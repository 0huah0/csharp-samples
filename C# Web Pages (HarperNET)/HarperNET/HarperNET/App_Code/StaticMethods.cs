using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using System.Diagnostics;

public static class StaticMethods
{
    private static string conn = ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString;
    private static string eventSource = ConfigurationManager.AppSettings["EventSource"];

    public static void LogError(string page, string message, string stacktrace)
    {
        EventLog.WriteEntry(eventSource, string.Format("Error on: {0}\r\nMessage:{1}", new object[] { page, message }), EventLogEntryType.Error);
        LogWebEvent(string.Empty, page, string.Format("MESSAGE:{0}||STACKTRACE:{1}", new object[]{message, stacktrace}));
    }
    public static void LogWebEvent(string strUser, string strEvent, string strEventDesc)
    {
        SqlConnection myConnection = new SqlConnection(conn);
        SqlCommand myCommand = new SqlCommand("usp_LogWebEvent", myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter paParameter = myCommand.Parameters.Add("@LogUserName", SqlDbType.NVarChar);
        paParameter.Value = strUser;

        paParameter = myCommand.Parameters.Add("@LogEvent", SqlDbType.NVarChar);
        paParameter.Value = strEvent;

        paParameter = myCommand.Parameters.Add("@LogEventDesc", SqlDbType.NText);
        paParameter.Value = strEventDesc;

        myConnection.Open();
        myCommand.ExecuteNonQuery();
        myConnection.Close();

    }

    #region used by auctions
    public static string GetTimeDiff(DateTime datStartDate, DateTime datEndDate)
    {
        string strReturnValue;
        TimeSpan spnDiff;

        spnDiff = datEndDate.Subtract(datStartDate);
        strReturnValue = "";
        if (spnDiff.Days > 0)
        {
            strReturnValue += spnDiff.Days.ToString() + " Day ";
        }
        strReturnValue += spnDiff.Hours.ToString() + " Hour ";
        strReturnValue += spnDiff.Minutes.ToString() + " Min ";
        return strReturnValue;
    }

    public static void SetDropDownValue(System.Web.UI.WebControls.DropDownList ddlDropDown, string strValue)
    {
        System.Web.UI.WebControls.ListItem itmItem;
        if (ddlDropDown.SelectedItem != null)
        {
            ddlDropDown.SelectedItem.Selected = false;
        }
        itmItem = ddlDropDown.Items.FindByValue(strValue);
        if (itmItem != null)
        {
            ddlDropDown.SelectedIndex = -1;
            itmItem.Selected = true;
        }
    }
    public static string PadWithZerosLen2(int intValue)
    {
        if (intValue < 10)
        {
            return "0" + intValue.ToString();
        }
        else
        {
            return intValue.ToString();
        }
    }
    #endregion
}
