using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;


using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;

namespace DemoWebApp
{
    public partial class Music : System.Web.UI.Page
    {
        public const String urlRoot = "M:\\";

        DateTime selectedDT;
        String url;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetSelectedDate(DateTime.Now);
                DisplaySelectedDate();
                ShowSelectedDirByGenre();
            }
            else
            {
                selectedDT = (DateTime)ViewState["dt"];
            }
        }

        public void Selection_Change(Object sender, EventArgs e)
        {
            SetSelectedDate(Calendar1.SelectedDate);
            DisplaySelectedDate();
            ShowSelectedDirByGenre();
        }

        protected void Btn_DatePrevious_Click(object sender, EventArgs e)
        {
            SetSelectedDate(selectedDT.AddDays(-1));
            DisplaySelectedDate();
            ShowSelectedDirByGenre();
        }

        protected void Btn_DateNext_Click(object sender, EventArgs e)
        {
            SetSelectedDate(selectedDT.AddDays(1));
            DisplaySelectedDate();
            ShowSelectedDirByGenre();
        }

        public void SetSelectedDate(DateTime dt)
        {
            selectedDT = dt;
            ViewState["dt"] = dt;
            url = urlRoot + dt.ToString("yyyy") + "\\" + dt.ToString("MM") + "\\" + dt.ToString("dd");
        }

        public void DisplaySelectedDate()
        {
            Literal1.Text = selectedDT.ToString("yyyy") + "-" + selectedDT.ToString("MM") + "-" + selectedDT.ToString("dd");

            Calendar1.TodaysDate = selectedDT;
            Calendar1.SelectedDate = Calendar1.TodaysDate;
        }

        public void ShowSelectedDirByGenre()
        {
            try
            {
                DirectoryInfo dirInfoSource = new DirectoryInfo(url);

                //HtmlGenericControl main = new HtmlGenericControl();
                //main.Controls.Add(new LiteralControl("<div id='MyTarget'></div>"));
                //Panel3.Controls.Add(main);
                //Form.Controls.Add(main);

                

                // Loop subdirectory using recursion.
                foreach (DirectoryInfo dirSourceSubDir in dirInfoSource.GetDirectories())
                {
                    if (dirSourceSubDir.Name != ".metadata")
                    {
                        ShowSelectedDirByAlbum(dirSourceSubDir, Panel1);
                        ShowSelectedDirByTrack(dirSourceSubDir, Panel2);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Copy - Error {0}", ex.Message);
            }
        }

        public void ShowSelectedDirByAlbum(DirectoryInfo dirInfoSource, Panel p)
        {
            try
            {
                HtmlGenericControl main = new HtmlGenericControl();

                if (dirInfoSource.GetDirectories().Count() != 0)
                {
                    main.Controls.Add(new LiteralControl("<br><br><span style='font-size:20px'>" + dirInfoSource.Name + "</span><br><br>"));

                        // Loop subdirectory using recursion.
                        foreach (DirectoryInfo dirSourceSubDir in dirInfoSource.GetDirectories())
                        {
                            if (dirSourceSubDir.Name != ".metadata")
                            {
                                main.Controls.Add(new LiteralControl("<span style='font-size:20px'><img src='/images/" + dirSourceSubDir.Name + ".png' width='150px' hight='150px' /><br>" + dirSourceSubDir.Name + "</span><br><br>"));
                        
                                FileInfo[] tracks = dirSourceSubDir.GetFiles("*.mp3");

                                if (tracks.Count() != 0)
                                {
                                    main.Controls.Add(new LiteralControl("<p><span style='font-size:10px'>"));

                                    // Loop Files
                                    foreach (FileInfo track in tracks)
                                    {
                                        main.Controls.Add(new LiteralControl("<li>" + track.Name.Replace(".mp3", "")));
                                    }
                                    main.Controls.Add(new LiteralControl("</span><br><br>"));
                                }
                            }
                        }
                }

                p.Controls.Add(main);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Copy - Error {0}", ex.Message);
            }
        }

        public void ShowSelectedDirByTrack(DirectoryInfo dirInfoSource, Panel p)
        {
            try
            {
                HtmlGenericControl main = new HtmlGenericControl();
                
                FileInfo[] files = dirInfoSource.GetFiles("*.mp3");

                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                //String email = manager.GetEmail(User.Identity.GetUserId());
                //Boolean confirm = manager.IsEmailConfirmed(User.Identity.GetUserId());
                //main.Controls.Add(new LiteralControl(email));

                if (files.Count() != 0)
                {
                    main.Controls.Add(new LiteralControl("<br><br><span style='font-size:20px'>" + dirInfoSource.Name + "</span><br>"));
                    
                    // Loop Files
                    foreach (FileInfo file in files)
                    {
                        String title = file.Name.Replace(".mp3", "");
                        String path = dirInfoSource.FullName;
                        path = path.Replace(@"M:\", "");
                        path = path.Replace(@"\", @"/") + @"/";

                        //String urlEncode = Base64ForUrlEncode(url);
                        //urlEncode = "__1" + urlEncode.Remove(urlEncode.Length - 1);
                        //main.Controls.Add(new LiteralControl("<span style='text-align:left;font-size:12px'><a href='javascript:myPlayer.stop;javascript:myPlayer.setInfo(\"" + title + "\");javascript:myPlayer.play(\"" + urlEncode + "\");'><img src='/images/play_small.png'></a> "));


                        

                        if(Context.User.Identity.IsAuthenticated)
                        {
                            if(manager.IsEmailConfirmed(User.Identity.GetUserId()))
                            {
                                main.Controls.Add(new LiteralControl("<span style='text-align:left;font-size:12px'><a href='javascript:P(\"" + path + "\", \"" + title + "\");'><img src='/images/play_small.png'></a> "));
                                //main.Controls.Add(new LiteralControl("<a href='http:////ho-server-01.cloudapp.net/m/" + path + "/" + title + ".mp3'>Download</a>")); 
                            }
                        }
                        else
                        {
                            main.Controls.Add(new LiteralControl("<span style='text-align:left;font-size:12px'>" + title));
                        }
                        
                        main.Controls.Add(new LiteralControl(title + "</span><br>"));
                    }
                }

                p.Controls.Add(main);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Copy - Error {0}", ex.Message);
            }
        }

        ///<summary>
        /// Base 64 Encoding with URL and Filename Safe Alphabet using UTF-8 character set.
        ///</summary>
        ///<param name="str">The origianl string</param>
        ///<returns>The Base64 encoded string</returns>
        public static string Base64ForUrlEncode(string str)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(str);
            return HttpServerUtility.UrlTokenEncode(encbuff);
        }
        ///<summary>
        /// Decode Base64 encoded string with URL and Filename Safe Alphabet using UTF-8.
        ///</summary>
        ///<param name="str">Base64 code</param>
        ///<returns>The decoded string.</returns>
        public static string Base64ForUrlDecode(string str)
        {
            byte[] decbuff = HttpServerUtility.UrlTokenDecode(str);
            return Encoding.UTF8.GetString(decbuff);
        }

        



        

        //public void ShowSelectedDirByTrack(DirectoryInfo dirInfoSource)
        //{
        //    try
        //    {
        //        FileInfo[] files = dirInfoSource.GetFiles("*.mp3");

        //        if (files.Count() != 0)
        //        {
        //            GenerateHtmlGenre(dirInfoSource.Name);

        //            // Loop Files
        //            foreach (FileInfo file in files)
        //            {
        //                GenerateHtmlTrack(file.Name.Replace(".mp3", ""));
        //            }
        //        }

        //        // Loop subdirectory using recursion.
        //        foreach (DirectoryInfo dirSourceSubDir in dirInfoSource.GetDirectories())
        //        {
        //            if (dirSourceSubDir.Name != ".metadata")
        //            {
        //                ShowSelectedDirByGenre2(dirSourceSubDir);
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Copy - Error {0}", ex.Message);
        //    }
        //}

        

        //private void GenerateHtmlGenre(String genre, Panel p)
        //{
        //    HtmlGenericControl main = new HtmlGenericControl();

        //    //if (ulOpen)
        //    //{
        //    //    main.Controls.Add(new LiteralControl("</ul>"));
        //    //    ulOpen = false;
        //    //}

        //    main.Controls.Add(new LiteralControl("<span>"));
        //    main.Controls.Add(new LiteralControl(genre));
        //    main.Controls.Add(new LiteralControl("</span>"));
        //    main.Controls.Add(new LiteralControl("<br />"));
        //    //main.Controls.Add(new LiteralControl("<ul>"));
        //    p.Controls.Add(main);
        //    //ulOpen = true;
        //}

        //private void GenerateHtmlTrack(String track, Panel p)
        //{
        //    HtmlGenericControl main = new HtmlGenericControl();
        //    main.Controls.Add(new LiteralControl("<span>" + track + "</span><br>"));
        //    p.Controls.Add(main);
        //}

        //private void GenerateHtmlSmallAlbum(String track, Panel p)
        //{
        //    HtmlGenericControl main = new HtmlGenericControl();
        //    main.Controls.Add(new LiteralControl("<span><img src='/images/" + track + ".png' width='150px' hight='150px' /><br>" + track + "</span><br><br>"));
        //    p.Controls.Add(main);
        //}
    }
}