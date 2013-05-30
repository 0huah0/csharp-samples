using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;

public class Emailer
{
    private const int READ_CHUNK_SIZE = 1024;
    public static string GetMessageFromWeb(string strURL)
    {
        System.Text.StringBuilder sbuBuilder;
        // Creates an HttpWebRequest with the specified URL. 
        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(strURL);
        // Sends the HttpWebRequest and waits for the response.            
        HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
        // Gets the stream associated with the response.
        Stream receiveStream = myHttpWebResponse.GetResponseStream();
        Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
        // Pipes the stream to a higher level stream reader with the required encoding format. 
        StreamReader readStream = new StreamReader(receiveStream, encode);
        sbuBuilder = new System.Text.StringBuilder();
        Char[] read = new Char[READ_CHUNK_SIZE];
        // Reads 256 characters at a time.    
        int count = readStream.Read(read, 0, READ_CHUNK_SIZE);
        while (count > 0)
        {
            sbuBuilder.Append(read);
            if (count < READ_CHUNK_SIZE)
            {
                sbuBuilder.Remove(sbuBuilder.Length - (READ_CHUNK_SIZE - count), READ_CHUNK_SIZE - count);
            }
            count = readStream.Read(read, 0, READ_CHUNK_SIZE);
        }
        myHttpWebResponse.Close();
        readStream.Close();
        return sbuBuilder.ToString();
    }

    public static string RenderToMessage(System.Web.UI.UserControl ctrControl)
    {
        System.Text.StringBuilder sbuBuilder;
        System.IO.StringWriter wriWriter;
        System.Web.UI.HtmlTextWriter htwWriter;

        sbuBuilder = new System.Text.StringBuilder();

        sbuBuilder.Append("<html>");
        sbuBuilder.Append("<body>");

        wriWriter = new System.IO.StringWriter(sbuBuilder);
        htwWriter = new System.Web.UI.HtmlTextWriter(wriWriter);
        ctrControl.RenderControl(htwWriter);

        htwWriter.Close();
        wriWriter.Close();

        sbuBuilder.Append("</body>");
        sbuBuilder.Append("</html>");

        return sbuBuilder.ToString();
    }

    public static void Send(
        string strFrom,
        string strTo,
        string strSubject,
        string strHTMLMessage,
        string strSMTPServer)
    {
        MailMessage mmsMessage;
        SmtpClient smcClient;

        if (strTo.Trim() == "")
        {
            return;
        }
        mmsMessage = new MailMessage(strFrom, strTo);
        mmsMessage.Subject = strSubject;
        mmsMessage.Body = strHTMLMessage;
        mmsMessage.IsBodyHtml = true;

        smcClient = new SmtpClient(strSMTPServer);
        smcClient.UseDefaultCredentials = true;
        smcClient.Send(mmsMessage);
    }
}


