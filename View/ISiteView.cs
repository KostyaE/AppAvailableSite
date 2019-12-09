using AppAvailableSite.Model;
using System.Collections.Generic;

namespace AppAvailableSite.View
{
    public interface ISiteView
    {
        IList<Site> SiteList { get; set; }
        int SelectedSite { get; set; }
        string SiteName { get; set; }
        string UrlAddress { get; set; }
        int Interval { get; set; }
        bool Access_status { get; set; }
        string Status { get; set; }


        Presenter.SitePresenter Presenter { set; }
    }
}
