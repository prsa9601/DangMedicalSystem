using Common.Domain;
using Microsoft.Extensions.Logging;

namespace Application.SiteSettingService
{
    public class SiteSetting : AggregateRoot
    {
        public string CompanyName { get; set; }


        private static SiteSetting _instance;
        private SiteSetting() { }

       
        public static SiteSetting Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SiteSetting();
                }
                return _instance;
            }
        }

        public void SetCompanyName(string companyName)
        {
            CompanyName = companyName;
        }
    }
}
