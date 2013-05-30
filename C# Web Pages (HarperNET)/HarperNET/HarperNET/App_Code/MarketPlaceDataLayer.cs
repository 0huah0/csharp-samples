using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


    public class MarketPlaceDataLayer
    {
        private string m_strConnectionString;

        public MarketPlaceDataLayer(string strConnectionString)
        {
            m_strConnectionString = strConnectionString;
        }

        public DataSet AucGetOpeningPage(int intSeed)
        {
            using (DataSet dasData = new DataSet())
            {
                using (SqlConnection myConnection = new SqlConnection(m_strConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("usp_AucGetOpeningPage", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.Add("@Seed", SqlDbType.Int).Value = intSeed;
                        using (SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand))
                        {
                            apdAdapter.Fill(dasData);
                        }
                    }
                }
                return dasData;
            }
        }

        public DataSet AucGetAuctionThumbs(string StatusCode)
        {
            using (DataSet dasData = new DataSet())
            {
                using (SqlConnection myConnection = new SqlConnection(m_strConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("usp_AucGetAuctionThumbs", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.Add("@StatusCode", SqlDbType.NVarChar).Value = StatusCode;
                        using (SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand))
                        {
                            apdAdapter.Fill(dasData);
                        }
                    }
                }
                return dasData;
            }
        }

        public DataSet AucGetAuctionDetailForAuctionID(int aucID, bool LogRead)
        {
            using (DataSet dasData = new DataSet())
            {
                using (SqlConnection myConnection = new SqlConnection(m_strConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("usp_AucGetAuctionDetailForAuctionID", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.Add("@aucID", SqlDbType.Int).Value = aucID;
                        myCommand.Parameters.Add("@LogRead", SqlDbType.Bit).Value = LogRead;
                        using (SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand))
                        {
                            apdAdapter.Fill(dasData);
                        }
                    }
                }
                return dasData;
            }
        }

        public DataSet GetCustomerProfileByUserName(string UserName)
        {
            using (DataSet dasData = new DataSet())
            {
                using (SqlConnection myConnection = new SqlConnection(m_strConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("usp_GetCustomerProfileByUserName", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
                        using (SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand))
                        {
                            apdAdapter.Fill(dasData);
                        }
                    }
                }
                return dasData;
            }
        }

        public DataSet AucGetBidPageDropDowns()
        {
            using (DataSet dasData = new DataSet())
            {
                using (SqlConnection myConnection = new SqlConnection(m_strConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("usp_AucGetBidPageDropDowns", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand))
                        {
                            apdAdapter.Fill(dasData);
                        }
                    }
                }
                return dasData;
            }
        }

        public DataSet AucGetBillingInfo(string UserName)
        {
            using (DataSet dasData = new DataSet())
            {
                using (SqlConnection myConnection = new SqlConnection(m_strConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("usp_AucGetBillingInfo", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
                        using (SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand))
                        {
                            apdAdapter.Fill(dasData);
                            /*
                            string cc = dasData.Tables[1].Rows[0]["CardNum"].ToString();
                            try
                            {
                                dasData.Tables[1].Rows[0]["CardNum"] = HarperCRYPTO.Cryptography.Decrypt256FromHEX(cc);
                            }
                            catch (System.Security.Cryptography.CryptographicException)
                            {
                                //card num was not encrypted, do not need to decrypt it
                            }
                            */
                        }
                    }
                }
                return dasData;
            }
        }

        public DataSet AucHasCurrentBillingInfo(string UserName)
        {
            using (DataSet dasData = new DataSet())
            {
                using (SqlConnection myConnection = new SqlConnection(m_strConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("usp_AucHasCurrentBillingInfo", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
                        using (SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand))
                        {
                            apdAdapter.Fill(dasData);
                        }
                    }
                }
                return dasData;
            }
        }

        public DataSet AucUpdateBillingInfo(
             string cusUserName,
             string acuCCName,
             string acuPhone1,
             string acuPhoneExt1,
             string acuEmail,
             string acuCardType,
             string acuCardNum,
             int acuExpMo,
             int acuExpYr,
             string acuSecNum,
             string acuAddress1,
             string acuAddress2,
             string acuAddress3,
             string acuCity,
             string acuRegion,
             string acuCountry,
             string acuPostalCode)
        {
            using (DataSet dasData = new DataSet())
            {
                using (SqlConnection myConnection = new SqlConnection(m_strConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("usp_AucUpdateBillingInfo", myConnection))
                    {
                        #region Parameters
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.Add("@cusUserName", SqlDbType.NVarChar).Value = cusUserName;
                        myCommand.Parameters.Add("@acuCCName", SqlDbType.NVarChar).Value = acuCCName;
                        myCommand.Parameters.Add("@acuPhone1", SqlDbType.NVarChar).Value = acuPhone1;
                        myCommand.Parameters.Add("@acuPhoneExt1", SqlDbType.NVarChar).Value = acuPhoneExt1;
                        myCommand.Parameters.Add("@acuEmail", SqlDbType.NVarChar).Value = acuEmail;
                        myCommand.Parameters.Add("@acuCardType", SqlDbType.NVarChar).Value = acuCardType;
                        //myCommand.Parameters.Add("@acuCardNum", SqlDbType.NVarChar).Value = HarperCRYPTO.Cryptography.Encrypt256(acuCardNum);
                        myCommand.Parameters.Add("@acuCardNum", SqlDbType.NVarChar).Value = acuCardNum;
                        myCommand.Parameters.Add("@acuExpMo", SqlDbType.Int).Value = acuExpMo;
                        myCommand.Parameters.Add("@acuExpYr", SqlDbType.Int).Value = acuExpYr;
                        myCommand.Parameters.Add("@acuSecNum", SqlDbType.NVarChar).Value = acuSecNum;
                        myCommand.Parameters.Add("@acuAddress1", SqlDbType.NVarChar).Value = acuAddress1;
                        myCommand.Parameters.Add("@acuAddress2", SqlDbType.NVarChar).Value = acuAddress2;
                        myCommand.Parameters.Add("@acuAddress3", SqlDbType.NVarChar).Value = acuAddress3;
                        myCommand.Parameters.Add("@acuCity", SqlDbType.NVarChar).Value = acuCity;
                        myCommand.Parameters.Add("@acuRegion", SqlDbType.NVarChar).Value = acuRegion;
                        myCommand.Parameters.Add("@acuCountry", SqlDbType.NVarChar).Value = acuCountry;
                        myCommand.Parameters.Add("@acuPostalCode", SqlDbType.NVarChar).Value = acuPostalCode;
                        #endregion
                        using (SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand))
                        {
                            apdAdapter.Fill(dasData);
                        }
                    }
                }
                return dasData;
            }
        }

        public DataSet AucInsertNewBid(int aucID, double abdBid, string abdVoucher, string cusUserName)
        {
            using (DataSet dasData = new DataSet())
            {
                using (SqlConnection myConnection = new SqlConnection(m_strConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("usp_AucInsertNewBid", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.Add("@aucID", SqlDbType.Int).Value = aucID;
                        myCommand.Parameters.Add("@abdBid", SqlDbType.Money).Value = abdBid;
                        myCommand.Parameters.Add("@cusUserName", SqlDbType.NVarChar).Value = cusUserName;
                        myCommand.Parameters.Add("@abdVoucher", SqlDbType.NVarChar).Value = abdVoucher;
                        using (SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand))
                        {
                            apdAdapter.Fill(dasData);
                        }
                    }
                }
                return dasData;
            }
        }

        public DataSet AucInsertAutoBid(int aucID, double aabMaxBid, string aabVoucher, string cusUserName)
        {
            using (DataSet dasData = new DataSet())
            {
                using (SqlConnection myConnection = new SqlConnection(m_strConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("usp_AucInsertAutoBid", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.Add("@aucID", SqlDbType.Int).Value = aucID;
                        myCommand.Parameters.Add("@aabMaxBid", SqlDbType.Money).Value = aabMaxBid;
                        myCommand.Parameters.Add("@cusUserName", SqlDbType.NVarChar).Value = cusUserName;
                        myCommand.Parameters.Add("@aabVoucher", SqlDbType.NVarChar).Value = aabVoucher;
                        using (SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand))
                        {
                            apdAdapter.Fill(dasData);
                        }
                    }
                }
                return dasData;
            }
        }

        public DataSet AucValidateOneTimeBid(int aucID, double abdBid)
        {
            using (DataSet dasData = new DataSet())
            {
                using (SqlConnection myConnection = new SqlConnection(m_strConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("usp_AucValidateOneTimeBid", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.Add("@aucID", SqlDbType.Int).Value = aucID;
                        myCommand.Parameters.Add("@abdBid", SqlDbType.Money).Value = abdBid;
                        using (SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand))
                        {
                            apdAdapter.Fill(dasData);
                        }
                    }
                }
                return dasData;
            }
        }

        public DataSet AucValidateAutoBid(int aucID, double aabMaxBid)
        {
            using (DataSet dasData = new DataSet())
            {
                using (SqlConnection myConnection = new SqlConnection(m_strConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("usp_AucValidateAutoBid", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.Add("@aucID", SqlDbType.Int).Value = aucID;
                        myCommand.Parameters.Add("@aabMaxBid", SqlDbType.Money).Value = aabMaxBid;
                        using (SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand))
                        {
                            apdAdapter.Fill(dasData);
                        }
                    }
                }
                return dasData;
            }
        }

    }


