using AppAvailableSite.Presenter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace AppAvailableSite.Model
{
    class SiteXmlRepository : ISiteRepository
    {
        private readonly string _xmlFilePath;
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(List<Site>));
        public readonly Lazy<List<Site>> _sites;

        public SiteXmlRepository(string fullPath)
        {
            _xmlFilePath = fullPath + @"\sites.xml";

            if (!File.Exists(_xmlFilePath))
                CreateSiteXmlStub();

            _sites = new Lazy<List<Site>>(() =>
            {
                using (var reader = new StreamReader(_xmlFilePath))
                {
                    List<Site> sites = new List<Site>();
                    sites = (List<Site>)_serializer.Deserialize(reader);

                    WebAccessSite.InspectionOfSite(sites);

                    return sites;
                }
            });

        }

        private void CreateSiteXmlStub()
        {
            var stubSiteList = new List<Site> {
                new Site {Name = "GitHub", Url = "https://github.com", Access_interval = 2, Access_status = false},
                new Site {Name = "YouTube", Url = "https://www.youtube.com", Access_interval = 1, Access_status = false},
                new Site {Name = "Google", Url = "https://www.google.ru", Access_interval = 1, Access_status = false},
                new Site {Name = "Microsoft", Url = "https://www.microsoft.com", Access_interval = 2, Access_status = false},
                new Site {Name = "Apple", Url = "https://www.apple.com", Access_interval = 1, Access_status = false},
                new Site {Name = "VK", Url = "https://vk.com", Access_interval = 3, Access_status = false}
            };
            SaveSiteList(stubSiteList);
        }

        private void SaveSiteList(List<Site> site)
        {
            WebAccessSite.InspectionOfSite(site);

            using (var writer = new StreamWriter(_xmlFilePath, false))
            {
                
                _serializer.Serialize(writer, site);
            }
        }

        public IEnumerable<Site> GetAllSite()
        {
            return _sites.Value;
        }

        public Site GetSite(int id)
        {
            return _sites.Value[id];
        }

        public void SaveSite(int id, Site site)
        {
            _sites.Value[id] = site;
            SaveSiteList(_sites.Value);
        }

        public void DeleteSite(int id)
        {
            _sites.Value.RemoveAt(id);
            SaveSiteList(_sites.Value);
        }

        public void AddSite(Site site)
        {
            _sites.Value.Add(site);
            SaveSiteList(_sites.Value);

        }
    }
}
