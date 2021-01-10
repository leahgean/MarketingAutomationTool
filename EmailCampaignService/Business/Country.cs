using EmailCampaignService.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailCampaignService.Business
{
    public class Country
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();
        public string GetCountry(int countryid)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(connectionString);

           
            string strCountry = dc.Countries.FirstOrDefault(e=>e.CountryID==countryid).ToString();
            return strCountry;

        }
    }
}
