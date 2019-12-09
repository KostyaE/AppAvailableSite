using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAvailableSite.Model
{
    public interface ISiteRepository
    {
        IEnumerable<Site> GetAllSite();
        Site GetSite(int id);
        void SaveSite(int id, Site site);
        void DeleteSite(int id);
        void AddSite(Site site);
    }
}
