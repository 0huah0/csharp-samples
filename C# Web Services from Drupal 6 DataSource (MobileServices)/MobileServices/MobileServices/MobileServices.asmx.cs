using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Configuration;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.IO;
using System.Net;

namespace MemberServices
{
    /// <summary>
    /// Summary description for MobileServices
    /// </summary>
    [WebService(Namespace = "http://andrewharper.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MobileServices : System.Web.Services.WebService
    {
        private int ResultsPerPage = 6;

        [WebMethod]
        public PageContent GetPageContent()
        {
            PageContent Response = new PageContent();
            return Response;
        }

        [WebMethod]
        public Detail GetDetail(string HotelId)
        {
            Detail Response = new Detail();
            int id = 0;
            if (int.TryParse(HotelId, out id))
            {
                Response = new Detail(id);
                Response.IsSuccess = true;
            }
            else
            {
                Response.IsSuccess = false;
                Response.ErrorMessage = "Hotel id must be a valid integer";
            }
            return Response;
        }
        
        [WebMethod]
        public Summary GetSummary(string HotelId)
        {
            Summary Response = new Summary();
            int id = 0;
            if (int.TryParse(HotelId, out id))
            {
                Response = new Summary(id);
                Response.IsSuccess = true;
            }
            else
            {
                Response.IsSuccess = false;
                Response.ErrorMessage = "Hotel id must be a valid integer";
            }
            return Response;
        }

        [WebMethod]
        public List<Summary> GetSummaries(string[] HotelIds)
        {
            List<Summary> Response = new List<Summary>();
            foreach (string hotelid in HotelIds)
            {
                Summary summary = new Summary();
                int id = 0;
                if (int.TryParse(hotelid, out id))
                {
                    summary = new Summary(id);
                    summary.IsSuccess = true;
                }
                else
                {
                    summary.IsSuccess = false;
                    summary.ErrorMessage = "Hotel id must be a valid integer";
                }
                Response.Add(summary);
            }            
            return Response;
        }

        [WebMethod]
        public SearchResponse SearchHotels(string Name, string City, string Province_Region, string Country)
        {
            SearchResponse Response = new SearchResponse();

            //get all hotel ids
            List<int> hotelids = new List<int>();
            MySqlConnection connection = new MySqlConnection(ConfigurationManager.AppSettings["drupal-data-connection"]);
            try{
                MySqlCommand command = connection.CreateCommand();
                MySqlDataReader Reader;
                string query = string.Empty;
                string union = " union ";
                query += string.IsNullOrEmpty(Name) ? string.Empty : QueryBuilder.SearchHotelName(Name, 0, false) + union;
                query += string.IsNullOrEmpty(City) ? string.Empty : QueryBuilder.SearchCity(City, 2, false) + union;
                query += string.IsNullOrEmpty(Province_Region) ? string.Empty : QueryBuilder.SearchProvince(Province_Region, 4, false) + union;
                query += string.IsNullOrEmpty(Country) ? string.Empty : QueryBuilder.SearchCountry(Country, 6, false) + union;
                query = query.Replace("\r\n", "");
                query = query.Substring(0, query.LastIndexOf(union));
                command.CommandText = query;
                connection.Open();
                Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    hotelids.Add(Reader.GetInt32("nid"));
                }
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            hotelids = hotelids.Distinct().ToList<int>();

            //get summaries
            List<Summary> summaries = new List<Summary>();
            int resultCount = 1;
            foreach (int hotelid in hotelids)
            {
                summaries.Add(new Summary(hotelid));
                if (resultCount >= ResultsPerPage) { break; }
                resultCount++;
            }
            
            

            Response.HotelIds = hotelids.ToArray();
            Response.Summaries = summaries.ToArray();
            Response.IsSuccess = true;
            return Response;
        }

        // modify this query
        [WebMethod]
        public SearchResponse SearchSpecialOffers(string Name, string City, string Province_Region, string Country)
        {
            SearchResponse Response = new SearchResponse();

            //get all hotel ids
            List<int> hotelids = new List<int>();
            MySqlConnection connection = new MySqlConnection(ConfigurationManager.AppSettings["drupal-data-connection"]);
            try
            {
                MySqlCommand command = connection.CreateCommand();
                MySqlDataReader Reader;
                string query = string.Empty;
                string union = " union ";
                query += string.IsNullOrEmpty(Name) ? string.Empty : QueryBuilder.SearchSpecialOffers(Name) + union;
                query += string.IsNullOrEmpty(City) ? string.Empty : QueryBuilder.SearchCity(City, 5, true) + union;
                query += string.IsNullOrEmpty(Province_Region) ? string.Empty : QueryBuilder.SearchProvince(Province_Region, 7, true) + union;
                query += string.IsNullOrEmpty(Country) ? string.Empty : QueryBuilder.SearchCountry(Country, 9, true) + union;
                query = query.Replace("\r\n", "");
                query = query.Substring(0, query.LastIndexOf(union));
                command.CommandText = query;
                connection.Open();
                Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    hotelids.Add(Reader.GetInt32("nid"));
                }
            }
            catch
            {
                Response.IsSuccess = false;
            }
            finally
            {
                connection.Close();
            }
            if (Response.IsSuccess)
            {
                hotelids = hotelids.Distinct().ToList<int>();

                //get summaries
                List<Summary> summaries = new List<Summary>();
                int resultCount = 1;
                foreach (int hotelid in hotelids)
                {
                    summaries.Add(new Summary(hotelid));
                    if (resultCount >= ResultsPerPage) { break; }
                    resultCount++;
                }

                Response.HotelIds = hotelids.ToArray();
                Response.Summaries = summaries.ToArray();
                Response.IsSuccess = true;
            }
            return Response;
        }

        [WebMethod]
        public ContactFormResponse SubmitContactForm(string FirstName, string LastName, string SpecialOfferName, string HotelName, string HotelLocation, string RoomCount,
            string CheckInDate, string CheckOutDate, string Phone, string Email, string IsMember, string Comments)
        {
            ContactForm FormObject = new ContactForm(FirstName, LastName, SpecialOfferName, HotelName, HotelLocation, RoomCount, CheckInDate, CheckOutDate, Phone, Email, IsMember, Comments);
            return FormObject.Submit();
        }
    }

    public class SearchResponse
    {
        public bool IsSuccess = true;
        public string ErrorMessage = "If error happens at the service level, IsSuccess will be false and ErrorMessage will contain the error message.  The error message is for debugging and testing purposes only, it is not to be shown on screen to the user.";

        public int[] HotelIds;
        public Summary[] Summaries;
    }
    
    public class Detail
    {
        public bool IsSuccess = true;
        public string ErrorMessage = string.Empty;

        public int HotelId = 0;
        public string Name = string.Empty;
        public int Rating = 0;
        public List<string> Photos = new List<string>();
        public string Address1 = string.Empty;
        public string Address2 = string.Empty;
        public string City = string.Empty;
        public string Province_State = string.Empty;
        public string PostalCode = string.Empty;
        public string Country = string.Empty;
        public string MemberBenefits = string.Empty;
        public string FromAndrewHarper = string.Empty;
        public string Rates = string.Empty;
        public List<MemberComment> MemberComments = new List<MemberComment>();
        
        public int SpecialOfferId = 0;
        public string SpecialOfferThumbnail = string.Empty;
        public string SpecialOfferName = string.Empty;
        public string SpecialOfferDescription = string.Empty;
        public string SpecialOfferBookingConditions = string.Empty;
        
        public Detail(){}
        public Detail(int hotelid)
        {
            this.HotelId = hotelid;
            this.IsSuccess = true;            

            #region detail information
            MySqlConnection connection = new MySqlConnection(ConfigurationManager.AppSettings["drupal-data-connection"]);
            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = QueryBuilder.GetQueryGetDetail(hotelid);
                command.CommandType = CommandType.Text;
                connection.Open();
                MySqlDataReader Reader = command.ExecuteReader(CommandBehavior.SingleRow);
                while (Reader.Read())
                {
                    string countryname = Reader.GetString("COUNTRY");
                    string regionname = Reader.GetString("PROVINCE_STATE");
                                        
                    this.Name = Reader.GetString("NAME");
                    this.Rating = 0;
                    switch (Reader.GetInt32("RATING"))
                    {
                        case 34:
                            this.Rating = 1;
                            break;
                        case 67:
                            this.Rating = 2;
                            break;
                        case 100:
                            this.Rating = 3;
                            break;
                    }
                    this.Address1 = Reader.GetString("ADDRESS1");
                    this.Address2 = Reader.GetString("ADDRESS2");
                    this.City = Reader.GetString("CITY");
                    this.Province_State = regionname; // Reader.GetString("PROVINCE_STATE");
                    this.PostalCode = Reader.GetString("POSTALCODE");
                    this.Country = countryname.ToLower() == "usa" ? "U.S." : System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(countryname.ToLower()); // Reader.GetString("COUNTRY");
                    this.MemberBenefits = Reader.GetString("MEMBERPRIVILEGES");
                    this.FromAndrewHarper = Reader.GetString("OVERVIEW");
                    this.Rates = Reader.GetString("RATES");
                    this.SpecialOfferId = Reader.GetInt32("SPECIALOFFERID");
                    this.SpecialOfferName = Reader.GetString("SPECIALOFFERNAME");
                    this.SpecialOfferDescription = Reader.GetString("SPECIALOFFERDESCRIPTION");
                    this.SpecialOfferBookingConditions = Reader.GetString("SPECIALOFFERBOOKINGCONDITIONS");
                }
            }
            catch(Exception e) {this.IsSuccess = false; throw e;}
            finally{ connection.Close();}
            #endregion

            #region hotel photos
            MySqlConnection connection2 = new MySqlConnection(ConfigurationManager.AppSettings["drupal-data-connection"]);
            try
            {
                MySqlCommand command2 = connection2.CreateCommand();
                command2.CommandText = QueryBuilder.GetQueryGetImage(this.HotelId, "hotels", "320x174", true);
                command2.CommandType = CommandType.Text;
                connection2.Open();
                MySqlDataReader Reader2 = command2.ExecuteReader();
                while (Reader2.Read())
                {
                    this.Photos.Add(string.Format("{0}/{1}", new object[]{ ConfigurationManager.AppSettings["image-root"], Reader2.GetString("filepath")}));
                }
            }
            catch { this.IsSuccess = false; }
            finally { connection2.Close(); }
            #endregion

            #region special offer thumbnail
            if (this.SpecialOfferId > 0)
            {
                MySqlConnection connection3 = new MySqlConnection(ConfigurationManager.AppSettings["drupal-data-connection"]);
                try
                {
                    MySqlCommand command3 = connection3.CreateCommand();
                    command3.CommandText = QueryBuilder.GetQueryGetImage(this.SpecialOfferId, "special_offers", "88x48", false);
                    command3.CommandType = CommandType.Text;
                    connection3.Open();
                    MySqlDataReader Reader3 = command3.ExecuteReader(CommandBehavior.SingleRow);
                    while (Reader3.Read())
                    {
                        this.SpecialOfferThumbnail = string.Format("{0}/{1}", new object[]{ ConfigurationManager.AppSettings["image-root"], Reader3.GetString("filepath")});
                    }
                }
                catch { this.IsSuccess = false; }
                finally { connection3.Close(); }
                if(string.IsNullOrEmpty(this.SpecialOfferThumbnail))
                {
                    MySqlConnection connection4 = new MySqlConnection(ConfigurationManager.AppSettings["drupal-data-connection"]);
                    try
                    {
                        MySqlCommand command4 = connection4.CreateCommand();
                        command4.CommandText = QueryBuilder.GetQueryGetImage(this.HotelId, "hotels", "88x48", false);
                        command4.CommandType = CommandType.Text;
                        connection4.Open();
                        MySqlDataReader Reader4 = command4.ExecuteReader(CommandBehavior.SingleRow);
                        while (Reader4.Read())
                        {
                            this.SpecialOfferThumbnail = string.Format("{0}/{1}", new object[]{ ConfigurationManager.AppSettings["image-root"], Reader4.GetString("filepath")});
                        }
                    }
                    catch (Exception e) { this.IsSuccess = false; throw e; }
                    finally { connection4.Close(); }
                }
            }
            #endregion

            #region member comments
            List<MemberComment> workingset = new List<MemberComment>();
            MySqlConnection connection5 = new MySqlConnection(ConfigurationManager.AppSettings["drupal-data-connection"]);
            try
            {
                MySqlCommand command5 = connection5.CreateCommand();
                command5.CommandText = QueryBuilder.GetQueryGetHotelComments(this.HotelId);
                command5.CommandType = CommandType.Text;
                connection5.Open();
                MySqlDataReader Reader5 = command5.ExecuteReader();
                while (Reader5.Read())
                {
                    workingset.Add(new MemberComment(Reader5.GetInt32("cid")));
                }
            }
            catch (Exception e) { this.IsSuccess = false; throw e; }
            finally { connection5.Close(); }
            try
            {
                List<MemberComment> parents = workingset.Where(x => x.ParentId == 0).ToList<MemberComment>();
                List<MemberComment> children = workingset.Where(x => x.ParentId != 0).OrderBy(x => x.CommentDate).ToList<MemberComment>();
                foreach (MemberComment parent in parents)
                {
                    this.MemberComments.Add(parent);
                    //System.Diagnostics.Debug.WriteLine(string.Format("Parents: {0}, {1}, {2}", parent.CommentId, parent.Thread, parent.CommentDate));
                    AddChildComments(parent.CommentId, children);         
                }

                //example of how to use parentid to determine the indention level
                /*
                int indentlevel = 0;
                Dictionary<int, int> commentindents = new Dictionary<int, int>();
                foreach (MemberComment comment in this.MemberComments)
                {
                    indentlevel = comment.ParentId == 0 ? 0 : ((int)commentindents.Where(x => x.Key == comment.ParentId).First().Value) + 1;
                    commentindents.Add(comment.CommentId, indentlevel);
                    string indent = new string('\t', indentlevel);
                    System.Diagnostics.Debug.WriteLine(string.Format("{0}{1}", indent, comment.CommentId));
                }
                */ 
            }
            catch{}
            #endregion            
        }

        private void AddChildComments(int parentid, List<MemberComment> children)
        {
            foreach (MemberComment child in children)
            {
                if (child.ParentId == parentid)
                {
                    this.MemberComments.Add(child);
                    //System.Diagnostics.Debug.WriteLine(string.Format("Children: {0}, {1}, {2}", child.CommentId, child.Thread, child.CommentDate));
                    AddChildComments(child.CommentId, children);
                }
            }
        }
    }
    
    public class Summary
    {
        public bool IsSuccess = true;
        public string ErrorMessage = string.Empty;

        public int HotelId = 0;
        public string HotelName = string.Empty;
        public string HotelCity = string.Empty;
        public string HotelProvince_State = string.Empty;
        public string HotelCountry = string.Empty;
        public int HotelRating = 0;
        public string HotelThumbnail = string.Empty;

        public int SpecialOfferId = 0;
        public string SpecialOfferThumbnail = string.Empty;
        public string SpecialOfferName = string.Empty;


        public Summary(){}
        public Summary(int hotelid)
        {
            this.IsSuccess = true;
            this.HotelId = hotelid;

            #region summary data
            MySqlConnection connection = new MySqlConnection(ConfigurationManager.AppSettings["drupal-data-connection"]);
            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = QueryBuilder.GetQueryGetSummary(hotelid);
                command.CommandType = CommandType.Text;
                connection.Open();
                MySqlDataReader Reader = command.ExecuteReader(CommandBehavior.SingleRow);
                while (Reader.Read())
                {
                    string countryname = Reader.GetString("HOTELCOUNTRY");
                    this.HotelName = Reader.GetString("HOTELNAME");
                    this.HotelCity = Reader.GetString("HOTELCITY");
                    this.HotelProvince_State = Reader.GetString("HOTELPROVINCE");
                    this.HotelCountry = countryname.ToLower() == "usa" ? "U.S." : System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(countryname.ToLower()); // Reader.GetString("COUNTRY");

                    this.HotelRating = 0;
                    switch (Reader.GetInt32("HOTELRATING"))
                    {
                        case 34:
                            this.HotelRating = 1;
                            break;
                        case 67:
                            this.HotelRating = 2;
                            break;
                        case 100:
                            this.HotelRating = 3;
                            break;
                    }
                    this.SpecialOfferId = Reader.GetInt32("SPECIALOFFERID");
                    this.SpecialOfferName = Reader.GetString("SPECIALOFFERNAME");
                }
            }
            catch (Exception e) { this.IsSuccess = false; throw e; }
            finally{ connection.Close();}
            #endregion

            #region hotel thumbnail
            MySqlConnection connection2 = new MySqlConnection(ConfigurationManager.AppSettings["drupal-data-connection"]);
            try
            {
                MySqlCommand command2 = connection2.CreateCommand();
                command2.CommandText = QueryBuilder.GetQueryGetImage(this.HotelId, "hotels", "88x48", false);
                command2.CommandType = CommandType.Text;
                connection2.Open();
                MySqlDataReader Reader2 = command2.ExecuteReader(CommandBehavior.SingleRow);
                while (Reader2.Read())
                {
                    this.HotelThumbnail = string.Format("{0}/{1}", new object[] { ConfigurationManager.AppSettings["image-root"], Reader2.GetString("filepath") });
                }
            }
            catch (Exception e) { this.IsSuccess = false; throw e; }
            finally{ connection2.Close(); }
            #endregion

            #region special offer thumbnail
            MySqlConnection connection3 = new MySqlConnection(ConfigurationManager.AppSettings["drupal-data-connection"]);
            try
            {
                MySqlCommand command3 = connection3.CreateCommand();
                command3.CommandText = QueryBuilder.GetQueryGetImage(this.SpecialOfferId, "special_offers", "88x48", false);
                command3.CommandType = CommandType.Text;
                connection3.Open();
                MySqlDataReader Reader3 = command3.ExecuteReader(CommandBehavior.SingleRow);
                while (Reader3.Read())
                {
                    this.SpecialOfferThumbnail = string.Format("{0}/{1}", new object[] { ConfigurationManager.AppSettings["image-root"], Reader3.GetString("filepath") });
                }
            }
            catch (Exception e) { this.IsSuccess = false; throw e; }
            finally{connection3.Close();}
            if(string.IsNullOrEmpty(this.SpecialOfferThumbnail)){ this.SpecialOfferThumbnail = this.HotelThumbnail; }
            #endregion

        }
    }

    public class ContactForm
    {
        public string FirstName;
        public string LastName;
        public string HotelName;
        public string HotelLocation;
        public string RoomCount;
        public string CheckInDate;
        public string CheckOutDate;
        public string Phone;
        public string Email;
        public string IsMember;
        public string SpecialOfferName;
        public string Comments;

        public ContactForm(string FirstName, string LastName, string SpecialOfferName, string HotelName, string HotelLocation, string RoomCount,
            string CheckInDate, string CheckOutDate, string Phone, string Email, string IsMember, string Comments)
        {
            this.FirstName = Coalesce(FirstName);
            this.LastName = Coalesce(LastName);
            this.SpecialOfferName = Coalesce(SpecialOfferName);
            this.HotelName = Coalesce(HotelName);
            this.HotelLocation = Coalesce(HotelLocation);
            this.RoomCount = Coalesce(RoomCount);
            this.CheckInDate = Coalesce(CheckInDate);
            this.CheckOutDate = Coalesce(CheckOutDate);
            this.Phone = Coalesce(Phone);
            this.Email = Coalesce(Email);
            switch (Coalesce(IsMember).ToLower())
            {
                case "yes":
                case "true":
                case "y":
                case "1":
                    this.IsMember = "1";
                    break;
                default:
                    this.IsMember = "0";
                    break;
            }
            this.Comments = Coalesce(Comments);
        }

        public ContactFormResponse Submit()
        {
            ContactFormResponse Response = new ContactFormResponse();
            string urlparam = System.Web.HttpUtility.UrlEncode(string.Format(@"{0}||{1}||{2}||{3}||{4}||{5}||{6}||{7}||{8}||{9}||{10}||{11}",                                
                    this.FirstName, this.LastName, this.HotelName, this.HotelLocation, this.CheckInDate, this.CheckOutDate, this.Phone, 
                    this.Email, this.Comments, this.RoomCount, this.SpecialOfferName ,this.IsMember
                ).Replace("/", "-"));
            string submiturl = string.Format(@"{0}/{1}", ConfigurationManager.AppSettings["mobilesite-submitcontactform-url"], urlparam);

            System.Net.WebResponse response = null;
            Stream data = null;
            StreamReader reader = null;
            try
            {
                WebClient client = new WebClient();
                data = client.OpenRead(submiturl);
                reader = new StreamReader(data);
                string responsebody = reader.ReadToEnd().Replace("\r\n", "");
                if (responsebody == "SUCCESS")
                {
                    Response.IsSuccess = true;
                    Response.ErrorMessage = string.Empty;
                }
                else
                {
                    Response.IsSuccess = false;
                    Response.ErrorMessage = responsebody;
                }
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.ErrorMessage = ex.Message;
            }
            finally
            {
                if (data != null) { data.Close(); }
                if (reader != null) { reader.Close(); }
                if (response != null) { response.Close(); }
            }

            return Response;
        }

        public string Coalesce(string input)
        {
            return string.IsNullOrEmpty(input) ? string.Empty : input;
        }
    }
    public class ContactFormResponse
    {
        public bool IsSuccess = true;
        public string ErrorMessage = "If error happens at the service level, IsSuccess will be false and ErrorMessage will contain the error message.  The error message is for debugging and testing purposes only, it is not to be shown on screen to the user.";

    }

    public class PageContent
    {
        public string HomePagePhoto = string.Empty;
        public string HotelSearchPageCopy = string.Empty;
        public string PrivacyPolicy = string.Empty;
        //These values all hard-coded per dev meeting 10/4
        public string HomePageCopy = string.Empty;
        public string SpecialOfferSearchPageCopy = string.Empty;
        //public string ContactFormPageText = "Morbi ac eros in est ultricies facilisis. Vestibulum nec ornare sapien. Nunc euismod leo at mi egestas tristique. Integer eu libero neque. Fusce id ligula ut nunc pharetra pharetra rutrum vitae metus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Integer ut urna sit amet lorem porttitor hendrerit.";
        
        public PageContent()
        {
            MySqlConnection connection = new MySqlConnection(ConfigurationManager.AppSettings["drupal-data-connection"]);
            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = QueryBuilder.GetQueryGetHomePageContent();
                command.CommandType = CommandType.Text;
                connection.Open();
                MySqlDataReader Reader = command.ExecuteReader(CommandBehavior.SingleRow);
                while (Reader.Read())
                {
                    this.HomePageCopy = Reader.GetString("HOMEPAGECOPY");
                    this.HomePagePhoto = string.Format("{0}/{1}", new object[]{ ConfigurationManager.AppSettings["image-root"], Reader.GetString("HOMEPAGEPHOTO")});
                    this.HotelSearchPageCopy = Reader.GetString("HOTELSEARCHPAGECOPY");
                    this.PrivacyPolicy = Reader.GetString("PRIVACYPOLICY");
                    this.SpecialOfferSearchPageCopy = Reader.GetString("SPECIALOFFERSEARCHPAGECOPY");
                }
            }
            catch (Exception e) { throw e; }
            finally { connection.Close(); }
        }
    }

    public class MemberComment
    {
        public int CommentId = 0;
        public string MemberName = string.Empty;
        public DateTime CommentDate = DateTime.MinValue;
        public string Comment = string.Empty;
        public int ParentId = 0;
        public int HotelId = 0;
        //public string Thread = string.Empty;

        public MemberComment() { }
        public MemberComment(int cid) 
        {
            this.CommentId = cid;
            MySqlConnection connection = new MySqlConnection(ConfigurationManager.AppSettings["drupal-data-connection"]);
            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = QueryBuilder.GetQueryGetComment(this.CommentId);
                command.CommandType = CommandType.Text;
                connection.Open();
                MySqlDataReader Reader = command.ExecuteReader(CommandBehavior.SingleRow);
                while (Reader.Read())
                {
                    this.MemberName = Reader.IsDBNull(8) ? "" : QueryBuilder.GetUserScreenName(Reader.GetString("data"));
                    this.CommentDate = Reader.IsDBNull(5) ? DateTime.MinValue : QueryBuilder.ConvertFromUnixTimestamp(Reader.GetDouble("timestamp"));
                    this.Comment = Reader.GetString("comment");
                    //this.Thread = Reader.GetString("thread");
                    this.HotelId = Reader.GetInt32("nid");
                    this.ParentId = Reader.IsDBNull(7) ? 0 : QueryBuilder.GetParentIdFromThread(this.HotelId, Reader.GetString("thread"));//this.Thread);
                }
            }
            catch (Exception e) { throw e; }
            finally { connection.Close(); }
        }
    }

    public static class QueryBuilder
    {
        public static int GetParentIdFromThread(int nid, string commentthread)
        {
            int parentid = 0;
            //System.Diagnostics.Debug.WriteLine(commentthread);
            List<string> blocks = commentthread.Split('.').ToList<string>();
            if (blocks.Count > 1)
            {
                string thisthread = blocks[blocks.Count-1];
                string parentthread = commentthread.Replace("." + thisthread, "/");
                MySqlConnection connection = new MySqlConnection(ConfigurationManager.AppSettings["drupal-data-connection"]);
                try
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = QueryBuilder.GetQueryGetCommentByThread(nid, parentthread);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    MySqlDataReader Reader = command.ExecuteReader(CommandBehavior.SingleRow);
                    while (Reader.Read())
                    {
                        parentid = Reader.GetInt32("cid");
                    }
                }
                catch { }
                finally { connection.Close(); }
            }
            return parentid;
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public static string GetUserScreenName(string userdata)
        {
            string name = string.Empty;
            try
            {
                string screennamestartstring = "\"ScreenName\"";
                string usernamestartstring = "\"UserName\"";
                List<string> blocks = userdata.Split(';').ToList<string>();
                int nextblock = 0;
                int nameblock = 0;

                foreach (string block in blocks)
                {
                    nextblock++;
                    if (block.Contains(screennamestartstring))
                    {
                        nameblock = nextblock;
                        break;
                    }
                }
                if (nameblock <= 0)
                {
                    foreach (string block in blocks)
                    {
                        nextblock++;
                        if (block.Contains(usernamestartstring))
                        {
                            nameblock = nextblock;
                            break;
                        }
                    }
                }
                if (nameblock > 0)
                {
                    if (blocks[nameblock].Split('"').Count() == 3)
                    {
                        name = blocks[nameblock].Split('"')[1];
                    }
                }
            }
            catch { }
            return name;
        }

        public static string GetQueryGetDetail(int hotelid)
        {
            string query = @"
                            SELECT 
                            DISTINCT
                                 hotel.nid                                                          HOTELID
                                 , hotel.vid                                                        REVISION
                                 , hotel.title                                                      NAME
                                 , COALESCE(member_privileges.field_member_privileges_value, '')    MEMBERPRIVILEGES
                                 , COALESCE(from_andrew.field_from_andrew_harper_value, '')         OVERVIEW
                                 , COALESCE(rates.field_rates_value, '')                            RATES 
                                 , COALESCE(address.street, '')                                     ADDRESS1
                                 , COALESCE(address.additional, '')                                 ADDRESS2
                                 , COALESCE(address.city, '')                                       CITY
                                 , COALESCE(address.province, '')                                   PROVINCE_STATE
                                 , COALESCE(address.postal_code, '')                                POSTALCODE
                                 , COALESCE(isocountry.cntname, '')                                    COUNTRY
                                 , COALESCE(hoteldata.field_alliance_hotel_value , 'No')            ISALLIANCE
                                 , COALESCE(rating.field_ratings_rating, -1)                        RATING
                                 , COALESCE(specialoffers.nid, -1)                   SPECIALOFFERID
                                 , COALESCE(offernode.title, '')                     SPECIALOFFERNAME
                                 , COALESCE(SPECIALOFFERS.field_booking_condition_value, '') SPECIALOFFERBOOKINGCONDITIONS
                                 , COALESCE(specofferrevision.body, '') SPECIALOFFERDESCRIPTION

                            FROM node                                             HOTEL 
                                LEFT OUTER JOIN content_field_location_geo        ADDRESSLINK             on hotel.nid = addresslink.nid                          and hotel.vid = addresslink.vid
                                LEFT OUTER JOIN location                          ADDRESS                 on addresslink.field_location_geo_lid = address.lid
                                LEFT OUTER JOIN content_field_ratings             RATING                  on hotel.nid = rating.nid                               and hotel.vid = rating.vid
                                LEFT OUTER JOIN content_field_member_privileges   MEMBER_PRIVILEGES       on hotel.nid = member_privileges.nid                    and hotel.vid = member_privileges.vid
                                LEFT OUTER JOIN content_field_from_andrew_harper  FROM_ANDREW             on hotel.nid = from_andrew.nid                          and hotel.vid = from_andrew.vid
                                LEFT OUTER JOIN content_field_rates               RATES                   on hotel.nid = rates.nid                                and hotel.vid = rates.vid
                                LEFT OUTER JOIN content_type_hotels               HOTELDATA               on hotel.nid = hoteldata.nid                            and hotel.vid = hoteldata.vid    
                                LEFT OUTER JOIN content_field_hotels            OFFERSLINK              on hotel.nid = offerslink.field_hotels_nid 
                                LEFT OUTER JOIN content_type_special_offers     SPECIALOFFERS           on offerslink.nid = specialoffers.nid                   and offerslink.vid = specialoffers.vid
                                LEFT OUTER JOIN node                            OFFERNODE               on SPECIALOFFERS.nid = offernode.nid and SPECIALOFFERS.vid  = offernode.vid
                                left outer join node_revisions                  specofferrevision       on offernode.nid = specofferrevision.nid and offernode.vid = specofferrevision.vid
                                join nucleus.tbl_country isocountry on address.country = isocountry.cntisocode

                            WHERE HOTEL.nid = '{0}'
                            ORDER BY OFFERSLINK.vid desc, SPECIALOFFERNAME, SPECIALOFFERBOOKINGCONDITIONS, SPECIALOFFERDESCRIPTION ASC
                            LIMIT 1;";
            return string.Format(query, hotelid).Replace("\r\n", "  ");
        }

        public static string GetQueryGetSummary(int hotelid)
        {
            string query = @"
                            SELECT 
                            DISTINCT
                                 hotel.nid                                      HOTELID
                                 , hotel.title                                  HOTELNAME
                                 , COALESCE(address.city, '')                   HOTELCITY
                                 , COALESCE(isocountry.cntname, '')             HOTELCOUNTRY
                                 , COALESCE(address.province, '')               HOTELPROVINCE 
                                 , COALESCE(rating.field_ratings_rating, -1)    HOTELRATING
                                 , offernode.nid
                                 , hotel.vid
                                 , COALESCE(specialoffers.nid, -1)              SPECIALOFFERID 
                                 , COALESCE(offernode.title, '')                SPECIALOFFERNAME


                            FROM node                                           HOTEL 
                                LEFT OUTER JOIN content_field_location_geo      ADDRESSLINK             on hotel.nid = addresslink.nid                          and hotel.vid = addresslink.vid
                                LEFT OUTER JOIN location                        ADDRESS                 on addresslink.field_location_geo_lid = address.lid
                                LEFT OUTER JOIN content_field_ratings           RATING                  on hotel.nid = rating.nid                               and hotel.vid = rating.vid
                                
                                LEFT OUTER JOIN content_field_hotels            OFFERSLINK              on hotel.nid = offerslink.field_hotels_nid 
                                LEFT OUTER JOIN content_type_special_offers     SPECIALOFFERS           on offerslink.nid = specialoffers.nid                   and offerslink.vid = specialoffers.vid
                                LEFT OUTER JOIN node                            OFFERNODE               on SPECIALOFFERS.nid = offernode.nid and SPECIALOFFERS.vid  = offernode.vid
                                join nucleus.tbl_country isocountry on address.country = isocountry.cntisocode

                            WHERE HOTEL.nid = '{0}'
                            ORDER BY OFFERSLINK.vid DESC
                            LIMIT 1;";
            return string.Format(query, hotelid).Replace("\r\n", "  ");
        }
        
        public static string GetQueryGetImage(int id, string contenttype, string size, bool getall)
        {
            string query = @"
                            select 
                            coalesce(photofile.filepath, '') filepath from node content
                            left outer join content_field_image_{2} photo on content.nid = photo.nid and content.vid = photo.vid
                            left outer join files photofile on photo.field_image_{2}_fid = photofile.fid 
                            where content.nid = {0} 
                            and content.type = '{1}'
                            and photo.field_image_{2}_fid is not null
                            order by photofile.fid asc";
            if (getall)
            {
                query += ";";
            }
            else
            {
                query += " LIMIT 1;";
            }
            return string.Format(query, new object[]{id, contenttype, size}).Replace("\r\n", "  ");
        }

        public static string GetQueryGetHomePageContent()
        {
            return @"select 
                    content.vid
                    , content.nid
                    , content.field_mobile_hotel_search_copy_value HOTELSEARCHPAGECOPY
                    , content.field_mobile_privacy_policy_value PRIVACYPOLICY
                    , content.field_mobile_home_page_copy_value HOMEPAGECOPY
                    , content.field_mobile_so_search_copy_value SPECIALOFFERSEARCHPAGECOPY
                    , files.filepath HOMEPAGEPHOTO
                    from content_type_mobile_site_content content
                    join node on content.nid = node.nid and content.vid = node.vid
                    join content_field_image_320x175 images on images.nid = content.nid and images.vid = content.vid
                    join files on images.field_image_320x175_fid = files.fid
                    where node.status = 1
                    limit 1;
                    ";
        }

        public static string GetQueryGetHotelComments(int hotelid)
        {
            return string.Format(@"select comments.cid
                    from comments
                    where comments.nid = {0}
                    order by length(thread), timestamp desc; #order by thread length so parents show first
                    ", hotelid);
        }

        public static string GetQueryGetCommentByThread(int hotelid, string thread)
        {
            return string.Format(@"select comments.cid
                    from comments
                    where comments.nid = {0}
                    and thread = '{1}'
                    order by timestamp desc;
                    ", new object[]{hotelid, thread});
        }

        public static string GetQueryGetComment(int cid)
        {
            return string.Format(@"select comments.cid
                    ,comments.nid
                    ,comments.uid
                    ,comments.subject
                    ,comments.comment
                    ,comments.timestamp
                    ,comments.status
                    ,comments.thread
                    ,users.data
                    from comments
                    join users on comments.uid = users.uid
                    where comments.cid = {0};", cid);
        }

        public static string SearchCity(string searchterm, int groupnumber, bool searchspecialoffer)
        {
            string specialofferjoin = searchspecialoffer
                ? " join content_field_hotels specialofferlink on specialofferlink.field_hotels_nid = hotel.nid join node specialoffer on specialofferlink.nid = specialoffer.nid "
                : string.Empty;
            string specialofferwhere = searchspecialoffer
                ? " and specialoffer.status = 1 "
                : string.Empty;
            return string.Format(@"
                    (select distinct {1} searchgroup, hotel.nid from node HOTEL 
                    left outer join content_field_location_geo ADDRESSLINK on hotel.nid = addresslink.nid and hotel.vid = addresslink.vid
                    left outer join location ADDRESS on addresslink.field_location_geo_lid = address.lid
                    {3}
                    where HOTEL.type = 'hotels' and HOTEL.status = 1 
                    and ADDRESS.city = '{0}'
                    {4})

                    union 

                    (select distinct {2} searchgroup, hotel.nid from node HOTEL 
                    left outer join content_field_location_geo ADDRESSLINK on hotel.nid = addresslink.nid and hotel.vid = addresslink.vid
                    left outer join location ADDRESS on addresslink.field_location_geo_lid = address.lid
                    {3}
                    where HOTEL.type = 'hotels' and HOTEL.status = 1 
                    and ADDRESS.city like CONCAT('%', '{0}', '%')
                    {4})", searchterm, groupnumber, groupnumber + 1, specialofferjoin, specialofferwhere);
        }

        public static string SearchProvince(string searchterm, int groupnumber, bool searchspecialoffer)
        {
            string specialofferjoin = searchspecialoffer
                ? " join content_field_hotels specialofferlink on specialofferlink.field_hotels_nid = hotel.nid join node specialoffer on specialofferlink.nid = specialoffer.nid "
                : string.Empty;
            string specialofferwhere = searchspecialoffer
                ? " and specialoffer.status = 1 "
                : string.Empty;
            return string.Format(@"                        
                (
                    select distinct {1} searchgroup, hotel.nid from node HOTEL                     
                    left outer join content_field_location_geo ADDRESSLINK on hotel.nid = addresslink.nid and hotel.vid = addresslink.vid                    
                    left outer join location ADDRESS on addresslink.field_location_geo_lid = address.lid  
                    {3}
                    where HOTEL.type = 'hotels' and HOTEL.status = 1 
                    {4}                
                    and 
                    (
                        ADDRESS.province = '{0}'  
                        or ADDRESS.province in    
                        (
                            SELECT province.code
                            FROM `nucleus`.`tbl_country` country
                            left outer join `nucleus`.`tbl_province` province on country.cntisocode = province.country
                            where country.cntcode = '{0}'
                            or country.cntname = '{0}'
                            or country.sfgcountryname = '{0}'
                            or country.cntisocode = '{0}'
                            or province.code = '{0}'
                            or province.province = '{0}'
                        )
                        or ADDRESS.province in    
                        (
                            SELECT province.province
                            FROM `nucleus`.`tbl_country` country
                            left outer join `nucleus`.`tbl_province` province on country.cntisocode = province.country
                            where country.cntcode = '{0}'
                            or country.cntname = '{0}'
                            or country.sfgcountryname = '{0}'
                            or country.cntisocode = '{0}'
                            or province.code = '{0}'
                            or province.province = '{0}'
                        )
                    )
                )
                union
                (
                    select distinct {2} searchgroup, hotel.nid from node HOTEL        
                    left outer join content_field_location_geo ADDRESSLINK on hotel.nid = addresslink.nid 
                    and hotel.vid = addresslink.vid                    
                    left outer join location ADDRESS on addresslink.field_location_geo_lid = address.lid  
                    {3}
                    where HOTEL.type = 'hotels' and HOTEL.status = 1 
                    {4}
                    and 
                    (
                        ADDRESS.province like CONCAT('%', '{0}', '%') 
                        or ADDRESS.province like CONCAT('%', (SELECT province.code
                            FROM `nucleus`.`tbl_country` country
                            left outer join `nucleus`.`tbl_province` province on country.cntisocode = province.country
                            where country.cntcode = '{0}'
                            or country.cntname = '{0}'
                            or country.sfgcountryname = '{0}'
                            or country.cntisocode = '{0}'
                            or province.code = '{0}'
                            or province.province = '{0}'
                            limit 1), '%')     
                        or ADDRESS.province like CONCAT('%', (SELECT province.province
                            FROM `nucleus`.`tbl_country` country
                            left outer join `nucleus`.`tbl_province` province on country.cntisocode = province.country
                            where country.cntcode = '{0}'
                            or country.cntname = '{0}'
                            or country.sfgcountryname = '{0}'
                            or country.cntisocode = '{0}'
                            or province.code = '{0}'
                            or province.province = '{0}'
                            limit 1), '%')
                    )   
                )", searchterm, groupnumber, groupnumber + 1, specialofferjoin, specialofferwhere);
        }

        public static string SearchCountry(string searchterm, int groupnumber, bool searchspecialoffer)
        {
            switch(searchterm)
            {
                case "us":
                case "usa":
                case "u.s.":
                case "u.s.a.":
                case "united states":
                case "united states of america":
                case "america":
                    searchterm = "us";
                    break;
            }
            string specialofferjoin = searchspecialoffer
                ? " join content_field_hotels specialofferlink on specialofferlink.field_hotels_nid = hotel.nid join node specialoffer on specialofferlink.nid = specialoffer.nid "
                : string.Empty;
            string specialofferwhere = searchspecialoffer
                ? " and specialoffer.status = 1 "
                : string.Empty;
            return string.Format(@"
                    (
                        select distinct {1} searchgroup, hotel.nid from node HOTEL 
                        left outer join content_field_location_geo ADDRESSLINK on hotel.nid = addresslink.nid and hotel.vid = addresslink.vid
                        left outer join location ADDRESS on addresslink.field_location_geo_lid = address.lid
                        {3}
                        where HOTEL.type = 'hotels' and HOTEL.status = 1 
                        {4}
                        and 
                        (
                            ADDRESS.country = '{0}'
                            or ADDRESS.country in    
                            (
                                SELECT country.cntcode
                                FROM `nucleus`.`tbl_country` country
                                where country.cntcode = '{0}'
                                or country.cntname = '{0}'
                                or country.sfgcountryname = '{0}'
                                or country.cntisocode = '{0}'
                            )
                            or ADDRESS.country in    
                            (
                                SELECT country.cntname
                                FROM `nucleus`.`tbl_country` country
                                where country.cntcode = '{0}'
                                or country.cntname = '{0}'
                                or country.sfgcountryname = '{0}'
                                or country.cntisocode = '{0}'
                            )
                            or ADDRESS.country in    
                            (
                                SELECT country.cntisocode
                                FROM `nucleus`.`tbl_country` country
                                where country.cntcode = '{0}'
                                or country.cntname = '{0}'
                                or country.sfgcountryname = '{0}'
                                or country.cntisocode = '{0}'
                            )
                            or ADDRESS.country in    
                            (
                                SELECT country.sfgcountryname
                                FROM `nucleus`.`tbl_country` country
                                where country.cntcode = '{0}'
                                or country.cntname = '{0}'
                                or country.sfgcountryname = '{0}'
                                or country.cntisocode = '{0}'
                            )   
                        )
                    )

                    union 

                    (
                        select distinct {2} searchgroup, hotel.nid from node HOTEL 
                        left outer join content_field_location_geo ADDRESSLINK on hotel.nid = addresslink.nid and hotel.vid = addresslink.vid
                        left outer join location ADDRESS on addresslink.field_location_geo_lid = address.lid
                        {3}
                        where HOTEL.type = 'hotels' and HOTEL.status = 1 
                        {4}   
                        and 
                        (
                            ADDRESS.country like CONCAT('%', '{0}', '%')                 
                            or ADDRESS.country like CONCAT('%',     
                            (
                                SELECT country.cntcode
                                FROM `nucleus`.`tbl_country` country
                                where country.cntcode = '{0}'
                                or country.cntname = '{0}'
                                or country.sfgcountryname = '{0}'
                                or country.cntisocode = '{0}'
                                limit 1
                            ), '%')                  
                            or ADDRESS.country like CONCAT('%',     
                            (
                                SELECT country.cntname
                                FROM `nucleus`.`tbl_country` country
                                where country.cntcode = '{0}'
                                or country.cntname = '{0}'
                                or country.sfgcountryname = '{0}'
                                or country.cntisocode = '{0}'
                                limit 1
                            ), '%')                 
                            or ADDRESS.country like CONCAT('%',     
                            (
                                SELECT country.cntisocode
                                FROM `nucleus`.`tbl_country` country
                                where country.cntcode = '{0}'
                                or country.cntname = '{0}'
                                or country.sfgcountryname = '{0}'
                                or country.cntisocode = '{0}'
                                limit 1
                            ), '%')                 
                            or ADDRESS.country like CONCAT('%',     
                            (
                                SELECT country.sfgcountryname
                                FROM `nucleus`.`tbl_country` country
                                where country.cntcode = '{0}'
                                or country.cntname = '{0}'
                                or country.sfgcountryname = '{0}'
                                or country.cntisocode = '{0}'
                                limit 1
                            ), '%') 
                        )
                    )", searchterm, groupnumber, groupnumber + 1, specialofferjoin, specialofferwhere);
        }

        public static string SearchHotelName(string searchterm, int groupnumber, bool searchspecialoffer)
        {
            string specialoffer = searchspecialoffer ? " join content_field_hotels specialofferlink on specialofferlink.field_hotels_nid = hotel.nid " : string.Empty;
            return string.Format(@"
                    (select distinct {1} searchgroup, hotel.nid from node HOTEL 
                    where HOTEL.type = 'hotels' and HOTEL.status = 1 
                    and HOTEL.title = '{0}')

                    union 

                    (select distinct {2} searchgroup, hotel.nid from node HOTEL 
                    where HOTEL.type = 'hotels' and HOTEL.status = 1 
                    and HOTEL.title like CONCAT('%', '{0}', '%'))", searchterm, groupnumber, groupnumber + 1, specialoffer);
        }

        public static string SearchSpecialOffers(string searchterm)
        {
            return string.Format(@"(select distinct 1 searchgroup, hotel.field_hotels_nid nid from content_type_special_offers offer
                                    join node node on offer.nid = node.nid
                                    join node_revisions revisions on offer.vid = revisions.vid
                                    join content_field_hotels hotel on offer.nid = hotel.nid
                                    join node hotelnode on hotelnode.nid = hotel.field_hotels_nid and hotelnode.type = 'hotels'
                                    where node.type = 'special_offers'
                                    and node.status = 1
                                    and hotel.field_hotels_nid is not null
                                    and node.title = '{0}')

                                    union 

                                    (select distinct 2 searchgroup, hotel.field_hotels_nid nid from content_type_special_offers offer
                                    join node node on offer.nid = node.nid
                                    join node_revisions revisions on offer.vid = revisions.vid
                                    join content_field_hotels hotel on offer.nid = hotel.nid
                                    join node hotelnode on hotelnode.nid = hotel.field_hotels_nid and hotelnode.type = 'hotels'
                                    where node.type = 'special_offers' 
                                    and node.status = 1
                                    and hotel.field_hotels_nid is not null
                                    and node.title like concat('%', '{0}', '%'))

                                    union 

                                    (select distinct 3 searchgroup, hotel.field_hotels_nid nid from content_type_special_offers offer
                                    join node node on offer.nid = node.nid
                                    join node_revisions revisions on offer.vid = revisions.vid
                                    join content_field_hotels hotel on offer.nid = hotel.nid
                                    join node hotelnode on hotelnode.nid = hotel.field_hotels_nid and hotelnode.type = 'hotels'
                                    where node.type = 'special_offers'
                                    and node.status = 1
                                    and hotel.field_hotels_nid is not null
                                    and revisions.body = '{0}')

                                    union 

                                    (select distinct 4 searchgroup, hotel.field_hotels_nid nid from content_type_special_offers offer
                                    join node node on offer.nid = node.nid
                                    join node_revisions revisions on offer.vid = revisions.vid
                                    join content_field_hotels hotel on offer.nid = hotel.nid
                                    join node hotelnode on hotelnode.nid = hotel.field_hotels_nid and hotelnode.type = 'hotels'
                                    where node.type = 'special_offers' 
                                    and node.status = 1
                                    and hotel.field_hotels_nid is not null
                                    and revisions.body like concat('%', '{0}', '%'))", searchterm);
        }
    }
}
