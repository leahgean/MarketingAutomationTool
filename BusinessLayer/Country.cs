using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;

namespace BusinessLayer
{
    public class Country
    {
        public DataTable GetCountries()
        {
            DataAccessLayer.Controller.Country uCountry = new DataAccessLayer.Controller.Country();
            DataTable dtResult = uCountry.GetCountries();
            return dtResult;

        }

        public string GetCountry(int countryid)
        {
            DataAccessLayer.Controller.Country uCountry = new DataAccessLayer.Controller.Country();
            string strCountry = uCountry.GetCountry(countryid);
            return strCountry;

        }
    }
}
