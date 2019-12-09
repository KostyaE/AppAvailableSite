using AppAvailableSite.Model;
using AppAvailableSite.View;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AppAvailableSite
{
    internal partial class SiteForm : Form, ISiteView
    {
        private bool _isSelectedMode = false;

        public SiteForm()
        {
            InitializeComponent();
        }

        public IList<Site> SiteList
        {
            get
            {
                return (IList<Site>)this.listView_Site.Items;
            }
            set
            {
                listView_Site.Items.Clear();
                IList<Site> sites = value;
                foreach (Site _site in sites)
                {
                    string[] row = new string[] { _site.Name, _site.Url, _site.Status };
                    ListViewItem lvl = new ListViewItem(row);
                    lvl.Tag = _site;
                    listView_Site.Items.Add(lvl);
                }
            }
        }

        public int SelectedSite
        {
            get
            {
                if (listView_Site.SelectedItems.Count != 0)
                {
                    return this.listView_Site.Items.IndexOf(listView_Site.SelectedItems[0]);
                }
                else
                    return 0;
                    
            }
            set { }
        }
        
        public string SiteName
        {
            get
            {
                if (!string.IsNullOrEmpty(textBox_Name.Text))
                    return textBox_Name.Text;
                else
                    return "Null";
            }
            set { this.textBox_Name.Text = value; }
        }

        public string UrlAddress
        {
            get
            {
                if (!string.IsNullOrEmpty(textBox_URL.Text))
                    return textBox_URL.Text;
                else
                    return "Null";
            }
            set { this.textBox_URL.Text = value; }
        }

        public int Interval
        {
            get
            {
                int num;
                bool isNum = int.TryParse(textBox_interval.Text, out num);
                if (isNum)
                    return num;
                else
                {
                    MessageBox.Show("Поле Интервал принимает целое число");
                    textBox_interval.Text = "Null";
                    return 0;
                }
            }
            set
            {
                this.textBox_interval.Text = Convert.ToString(value);
            }
        }

        public string Status { get; set; }
        public bool Access_status { get; set; }

        public Presenter.SitePresenter Presenter
        { private get; set; }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isSelectedMode = true;
            button_Add.Text = _isSelectedMode ? "Редактировать" : "Добавить";
            Presenter.UpdateSiteView(SelectedSite);
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (_isSelectedMode == true)
            {
                Presenter.SaveSite();
                textBox_Name.Clear();
                textBox_URL.Clear();
                textBox_interval.Clear();
                _isSelectedMode = false;
                button_Add.Text = _isSelectedMode ? "Редактировать" : "Добавить";

            }
            else
            {
                if ((SiteName == "Null"))
                    MessageBox.Show("Поле Name не заполнено");
                else if ((UrlAddress == "Null"))
                    MessageBox.Show("Поле URL не заполнено");
                else if ((Interval == 0))
                    MessageBox.Show("Поле Interval не заполнено");
                else
                {
                    Presenter.AddSite();
                    textBox_Name.Clear();
                    textBox_URL.Clear();
                    textBox_interval.Clear();
                }
            }
            
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            if (_isSelectedMode == true)
            {
                Presenter.DeleteSite();
                textBox_Name.Clear();
                textBox_URL.Clear();
                textBox_interval.Clear();
                _isSelectedMode = false;
                button_Add.Text = _isSelectedMode ? "Редактировать" : "Добавить";
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBox_Name.Clear();
            textBox_URL.Clear();
            textBox_interval.Clear();
            if (_isSelectedMode == true)
            {
                _isSelectedMode = false;
                button_Add.Text = _isSelectedMode ? "Редактировать" : "Добавить";

            }

        }
    }
}
