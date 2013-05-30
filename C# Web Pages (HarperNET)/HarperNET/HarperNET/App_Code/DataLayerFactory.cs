using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

    /// <summary>
    /// Summary description for DataLayerFactory
    /// </summary>
    public class DataLayerFactory
    {
        public DataLayerFactory()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static CustomerDataLayer GetCustomer()
        {
            return new CustomerDataLayer(
                System.Configuration.ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ToString());
        }

        public static MarketPlaceDataLayer GetMarketPlace()
        {
            return new MarketPlaceDataLayer(System.Configuration.ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ToString());
        }

        //public static Member.TravelCenter.AH_TravelCenter_MainDL GetTravelCenter()
        //{
        //    return new Member.TravelCenter.AH_TravelCenter_MainDL(
        //        System.Configuration.ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ToString());
        //}

        //public static Member.RateRequest.DataLayer GetRateRequest()
        //{
        //    return new Member.RateRequest.DataLayer(
        //        System.Configuration.ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ToString());
        //}
    }

