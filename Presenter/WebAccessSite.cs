using AppAvailableSite.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace AppAvailableSite.Presenter
{
    public class WebAccessSite
    {
        public List<Site> web_sites { get; set; }
        public WebAccessSite(List<Site> sites)
        {
            web_sites = sites;

            Thread myThread = new Thread(new ParameterizedThreadStart(InspectionOfSite));
            myThread.Start(web_sites);

            //TimerCallback tm = new TimerCallback(InspectionOfSite);
            //Timer timer = new Timer(tm, web_sites, 0, 10000);
        }


        public static void InspectionOfSite(object site)
        {
            
            foreach (var s in (List<Site>)site)
            {
                s.Access_status = Inspection(s.Url);
            }

            bool Inspection(string url)
            {
                Uri uri = new Uri(url);
                try
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    httpWebResponse.Close();

                }
                catch
                {
                    return false;
                }
                return true;
            }

        }
    }
}
