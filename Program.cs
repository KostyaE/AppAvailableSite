using AppAvailableSite.Presenter;
using System;
using System.Windows.Forms;

namespace AppAvailableSite
{

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var repository = new Model.SiteXmlRepository(Application.StartupPath);
            var view = new SiteForm();
            WebAccessSite webAccessSite = new WebAccessSite(repository._sites.Value);
            var presenter = new Presenter.SitePresenter(view, repository, webAccessSite);

            Application.Run(view);
        }
    }
}
