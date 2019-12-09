using AppAvailableSite.View;
using AppAvailableSite.Model;
using System.Linq;

namespace AppAvailableSite.Presenter
{
    public class SitePresenter
    {
        private readonly ISiteView _view;
        private readonly ISiteRepository _repository;
        private readonly WebAccessSite _webAccessSite;

        public SitePresenter(ISiteView view, ISiteRepository repository, WebAccessSite webAccessSite)
        {
            _view = view;
            view.Presenter = this;
            _repository = repository;
            _webAccessSite = webAccessSite;
            
            UpdateSiteListView();
        }

        public void UpdateSiteListView()
        {
            var siteObjects = from site in _webAccessSite.web_sites select site;
            int selectedSite = _view.SelectedSite >= 0 ? _view.SelectedSite : 0;
            
            _view.SiteList = siteObjects.ToList();
            _view.SelectedSite = selectedSite;
        }

        public void UpdateSiteView(int p)
        {
            Site site = _repository.GetSite(p);
            _view.SiteName = site.Name;
            _view.UrlAddress = site.Url;
            _view.Interval = site.Access_interval;
            _view.Status = site.Status;
        }

        public void SaveSite()
        {
            Site site = new Site { Name = _view.SiteName, Url = _view.UrlAddress, Access_interval = _view.Interval };
            _repository.SaveSite(_view.SelectedSite, site);
            UpdateSiteListView();
        }

        public void DeleteSite()
        {
            int selectedSite = _view.SelectedSite >= 0 ? _view.SelectedSite : 0;
            _repository.DeleteSite(selectedSite);
            UpdateSiteListView();

        }

        public void AddSite()
        {
            Site site = new Site { Name = _view.SiteName, Url = _view.UrlAddress, Access_interval = _view.Interval };
            _repository.AddSite(site);
            UpdateSiteListView();

        }
    }
}
