using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAvailableSite.Model
{
    public class Site
    {

        public string Name { get; set; }
        public string Url { get; set; }
        public int Access_interval { get; set; }
        public bool Access_status { get; set; }

        public string Status
        {
            get
            {
                if (Access_status == true)
                    return "Доступен";
                else
                    return "Недоступен";
            }
            set {}
        }


    }
}
