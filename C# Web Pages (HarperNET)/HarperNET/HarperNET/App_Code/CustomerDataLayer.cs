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

    public class CustomerDataLayer
    {
        protected string m_strConnectionString;

        public CustomerDataLayer(string strConnectionString)
        {
            m_strConnectionString = strConnectionString;
        }

        public DataSet GetCustomerSubscription(int intCusID)
        {
            SqlConnection myConn = new SqlConnection(m_strConnectionString);
            SqlCommand myCom = new SqlCommand("usp_GetCustomerScription", myConn);
            SqlDataAdapter myAdapter = new SqlDataAdapter(myCom);
            SqlParameter myPara;
            DataSet dsSubscription = new DataSet();
            myCom.CommandType = CommandType.StoredProcedure;

            myPara = myCom.Parameters.Add("@cusID", SqlDbType.Int);
            myPara.Value = intCusID;

            myAdapter.Fill(dsSubscription);
            return dsSubscription;

        }
        
        public DataSet GetGiftLinkForCustID(int intCusID)
        {
            SqlConnection myConn = new SqlConnection(m_strConnectionString);
            SqlCommand myCom = new SqlCommand("JoinUs.usp_GetGiftLinkForCustID", myConn);
            SqlDataAdapter myAdapter = new SqlDataAdapter(myCom);
            SqlParameter myPara;
            DataSet dsGift = new DataSet();

            myCom.CommandType = CommandType.StoredProcedure;
            myPara = myCom.Parameters.Add("@cusCusID", SqlDbType.NVarChar);
            myPara.Value = intCusID;

            myAdapter.Fill(dsGift);
            return dsGift;
        
        }
        
        public DataSet GetCustomerIDForCustNum(string strCusNumber)
        {

            SqlConnection myConn = new SqlConnection(m_strConnectionString);
            SqlCommand myCom = new SqlCommand("usp_GetCustomerIDForCustNum", myConn);
            SqlDataAdapter myAdapter = new SqlDataAdapter(myCom);
            SqlParameter myPara;
            DataSet dsMember = new DataSet();

            myCom.CommandType = CommandType.StoredProcedure;
            myPara = myCom.Parameters.Add("@cusCustNum", SqlDbType.NVarChar);
            myPara.Value = strCusNumber;

            myAdapter.Fill(dsMember);
            return dsMember;
        }

        public DataSet CRGetReferralMessage(string strCusNumber)
        {
            SqlConnection myConn = new SqlConnection(m_strConnectionString);
            SqlCommand myCom = new SqlCommand("usp_CRGetReferralMessage", myConn);
            SqlDataAdapter myAdapter = new SqlDataAdapter(myCom);
            SqlParameter myPara;
            DataSet dsRef = new DataSet();

            myCom.CommandType = CommandType.StoredProcedure;
            myPara = myCom.Parameters.Add("@crfRefMemberNum", SqlDbType.NVarChar);
            myPara.Value = strCusNumber;

            myAdapter.Fill(dsRef);
            return dsRef;

        }

        public void CRInsertData(string strCusNumber, int intCount, string strMessage)
        {
            SqlConnection myConn = new SqlConnection(m_strConnectionString);
            SqlCommand myCom = new SqlCommand("usp_CRInsertData", myConn);
            SqlParameter myPara;
            SqlDataAdapter myAdapter = new SqlDataAdapter(myCom);

            myCom.CommandType = CommandType.StoredProcedure;


            myPara = myCom.Parameters.Add("@crfRefMemberNum", SqlDbType.NVarChar);
            myPara.Value = strCusNumber;

            myPara = myCom.Parameters.Add("@crfReferralCount", SqlDbType.Int);
            myPara.Value = intCount;

            myPara = myCom.Parameters.Add("@crfReferralMessage", SqlDbType.NVarChar);
            myPara.Value = strMessage;

            myConn.Open();
            myCom.ExecuteNonQuery();

            //int result = (int)myCom.Parameters["@Return Value"].Value;
            myConn.Close();
            //return result;


        }

        public DataSet GetUserNameForEmail(string strEmail)
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = new SqlConnection(m_strConnectionString);
            SqlCommand myCommand = new SqlCommand("usp_GetUserNameForEmail", myConnection);
            SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand);
            DataSet dasData;
            SqlParameter parParameter;

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            parParameter = myCommand.Parameters.Add("@cusEmail", SqlDbType.NVarChar);
            parParameter.Value = strEmail;

            // Execute the command
            dasData = new DataSet();
            apdAdapter.Fill(dasData);

            return dasData;
        }

        public DataSet GetRenewalRecords(string strUserName)
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = new SqlConnection(m_strConnectionString);
            SqlCommand myCommand = new SqlCommand("usp_GetRenewalRecords", myConnection);
            SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand);
            DataSet dasData;
            SqlParameter parParameter;

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            parParameter = myCommand.Parameters.Add("@cusUserName", SqlDbType.NVarChar);
            parParameter.Value = strUserName;

            // Execute the command
            dasData = new DataSet();
            apdAdapter.Fill(dasData);

            return dasData;
        }

        public DataSet GetRenewalLinkForUserName(string strUserName)
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = new SqlConnection(m_strConnectionString);
            SqlCommand myCommand = new SqlCommand("usp_GetRenewalLinkForUserName", myConnection);
            SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand);
            DataSet dasData;
            SqlParameter parParameter;

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            parParameter = myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar);
            parParameter.Value = strUserName;

            // Execute the command
            dasData = new DataSet();
            apdAdapter.Fill(dasData);

            return dasData;
        }

        public DataSet UpdateDisplayName(
            string strUserName,
            string strDisplayName,
            int isEmployee)
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = new SqlConnection(m_strConnectionString);
            SqlCommand myCommand = new SqlCommand("usp_UpdateDisplayName", myConnection);
            SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand);
            DataSet dasData;
            SqlParameter parParameter;

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            parParameter = myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar);
            parParameter.Value = strUserName;

            parParameter = myCommand.Parameters.Add("@DisplayName", SqlDbType.NVarChar);
            parParameter.Value = strDisplayName;

            parParameter = myCommand.Parameters.Add("@UpdateDisplayNameDate", SqlDbType.DateTime);
            parParameter.Value = DateTime.Now;

            parParameter = myCommand.Parameters.Add("@isEmployee", SqlDbType.Bit);
            parParameter.Value = isEmployee;
            
            // Execute the command
            dasData = new DataSet();
            apdAdapter.Fill(dasData);

            return dasData;
        }

        public DataSet CreateUpdateForumUserForUserName(string strUserName,string strExternalSource)
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = new SqlConnection(m_strConnectionString);
            SqlCommand myCommand = new SqlCommand("usp_CreateUpdateForumUserForUserName", myConnection);
            SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand);
            DataSet dasData;
            SqlParameter parParameter;

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            parParameter = myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar);
            parParameter.Value = strUserName;

            parParameter = myCommand.Parameters.Add("@ExternalSource", SqlDbType.NVarChar);
            parParameter.Value = strExternalSource;

            // Execute the command
            dasData = new DataSet();
            apdAdapter.Fill(dasData);

            return dasData;
        }

        public CustomerDataSet GetCustomerByUserName(string UserName)
        {
            SqlConnection myConnection = new SqlConnection(m_strConnectionString);
            SqlCommand myCommand = new SqlCommand("usp_GetCustomerByUserName", myConnection);
            SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand);
            CustomerDataSet dasData;
            SqlParameter parParameter;

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            parParameter = myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar);
            parParameter.Value = UserName;

            apdAdapter.TableMappings.Add(
                "Table", "tbl_Customer");

            apdAdapter.TableMappings.Add(
                "Table1", "tbl_NetMembership");

            // Execute the command
            dasData = new CustomerDataSet();
            apdAdapter.Fill(dasData);

            return dasData;
        }
        
        public DataSet InsertIntoBookingItemCritique(
            string UserName,
            string criHotelName,
            string criCity,
            string criEmail,
            string criDateVisited,
            string criReviewType,
            string criViewable,
            string criContact,
            string criComments)
        {
            SqlConnection myConnection = new SqlConnection(m_strConnectionString);
            SqlCommand myCommand = new SqlCommand("usp_InsertIntoBookingItemCritique", myConnection);
            SqlDataAdapter apdAdapter = new SqlDataAdapter(myCommand);
            DataSet dasData;
            SqlParameter parParameter;

            myCommand.CommandType = CommandType.StoredProcedure;

            parParameter = myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar);
            parParameter.Value = UserName;
            parParameter = myCommand.Parameters.Add("@criHotelName", SqlDbType.NVarChar);
            parParameter.Value = criHotelName;
            parParameter = myCommand.Parameters.Add("@criCity", SqlDbType.NVarChar);
            parParameter.Value = criCity;
            parParameter = myCommand.Parameters.Add("@criEmail", SqlDbType.NVarChar);
            parParameter.Value = criEmail;
            parParameter = myCommand.Parameters.Add("@criDateVisited", SqlDbType.NVarChar);
            parParameter.Value = criDateVisited;
            parParameter = myCommand.Parameters.Add("@criReviewType", SqlDbType.NVarChar);
            parParameter.Value = criReviewType;
            parParameter = myCommand.Parameters.Add("@criViewable", SqlDbType.NVarChar);
            parParameter.Value = criViewable;
            parParameter = myCommand.Parameters.Add("@criContact", SqlDbType.NVarChar);
            parParameter.Value = criContact;
            parParameter = myCommand.Parameters.Add("@criComments", SqlDbType.NText);
            parParameter.Value = criComments;

            dasData = new DataSet();
            apdAdapter.Fill(dasData);

            return dasData;
        }

		public bool DisplayNameAvailable(string displayName)
        {
          bool retVal = false;

          string sql = "select dbo.UsernameInUse('" + displayName + "')";
          using (SqlConnection conn = new SqlConnection(m_strConnectionString))
          {
	          using (SqlCommand cmd = new SqlCommand(sql, conn))
	          {
		          try
		          {
			          conn.Open();
			          retVal = !Convert.ToBoolean(cmd.ExecuteScalar());
		          }
		          catch { }
		          if (conn.State != ConnectionState.Closed)
			          conn.Close();
	          }
          }

          return retVal;
        }

    }

