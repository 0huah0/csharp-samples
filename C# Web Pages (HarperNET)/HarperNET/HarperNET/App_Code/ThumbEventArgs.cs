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
    /// Summary description for ThumbEventArgs
    /// </summary>
    public class ThumbEventArgs
    {
        int m_intAucID;

        public int AucID
        {
            get
            {
                return m_intAucID;
            }
        }

        public ThumbEventArgs(
            int intAucID)
        {
            m_intAucID = intAucID;
        }
    }