using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

    public delegate void EventHandlerWithHighBid(
        object sender, EventArgsWithHighBid e);

    public delegate void EventHandlerWithCancel(
        object sender, EventArgsWithCancel e);

    public class EventArgsWithHighBid : EventArgs
    {
        protected double m_dblHighBid = 0;

        public double HighBid
        {
            get
            {
                return m_dblHighBid;
            }
            set
            {
                m_dblHighBid = value;
            }
        }
    }

    public class EventArgsWithCancel : EventArgs
    {
        protected bool m_bolCancel = false;
        protected string m_strReason = "";

        public bool Cancel
        {
            get
            {
                return m_bolCancel;
            }
            set
            {
                m_bolCancel = value;
            }
        }

    }
