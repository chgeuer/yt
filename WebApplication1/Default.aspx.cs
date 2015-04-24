namespace WebApplication1
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Net.Http;
    
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                var data = new YoutubeMovieData(txtYouTubeURL.Text);
                if (data.Reason != null)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = data.Reason;
                    return;
                }

                lblTitle.Text = data.Title;

                data.Videos.ForEach(_ => ddlVideoFormats.Items.Add(new ListItem(text: _.Format,  value: _.Url)));

                btnProcess.Enabled = false;
                ddlVideoFormats.Visible = true;
                btnDownload.Visible = true;
                lblMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                var video = new YoutubeMovieData.Video(
                    format: ddlVideoFormats.SelectedItem.Text, 
                    url: ddlVideoFormats.SelectedValue);

                if (string.IsNullOrEmpty(video.Url))
                {
                    lblMessage.Text = "Unable to locate the Video.";
                    return;
                }

                var path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), 
                    "Downloads", video.Filename);

                var webClient = new WebClient();
                webClient.DownloadFileCompleted += (sender2, asyncCompletedEventArgs) =>
                {
                    lblMessage.Text = string.Format("Video downloaded to {0}", path);
                    lblMessage.ForeColor = Color.Green;
                };

                webClient.DownloadFileAsync(new Uri(video.Url), path);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }
    }

    public class YoutubeMovieData
    {
        public string Title { get; set; }
        public string Reason { get; set; }
        public List<Video> Videos { get; set; }

        public class Video
        {
            public string Url { get; set; }
            public string Format { get; set; }
            public string Filename { get; set; }
            public Video(string url, string format)
            {
                var parsed = parse(url);
                var title = decode(parsed["title"]);
                var type = decode(parsed["type"]);
                var extension = type.Split(';')[0].Split('/')[1];

                this.Url = url;
                this.Format = format;
                this.Filename = string.Format("{0} ({1}).{2}", title, format, extension);
            }
        }

        public YoutubeMovieData(string address)
        {
            var id = parse(new Uri(address).Query)["v"];
            var infoUrl = string.Format("http://www.youtube.com/get_video_info?video_id={0}", id);
            var infoText = Download(infoUrl);
            var infoParsed = parse(decode(infoText));

            this.Title = infoParsed["title"];
            this.Reason = infoParsed["reason"];
            this.Videos = infoParsed["url_encoded_fmt_stream_map"].Split(',').Select(videoUrl =>
            {
                var parsed = parse(decode(videoUrl));
                Func<string, string> param = key => decode(parsed[key]);

                var assetUrl = param("url") + "&signature=" + param("sig") + "&type=" + param("type") + "&title=" + this.Title;
                var format = parsed["quality"]+ " - " + param("type").Split(';')[0].Split('/')[1];

                return new Video(url: assetUrl, format: format);
            }).ToList();
        }

        static string decode(string str) { return HttpUtility.HtmlDecode(str); }
        static NameValueCollection parse(string str) { return HttpUtility.ParseQueryString(str); }

        static string Download(string videoInfoUrl)
        {
            var request = (HttpWebRequest)WebRequest.Create(videoInfoUrl);
            var response = (HttpWebResponse)(request.GetResponse());
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream, Encoding.UTF8);
            var videoInfoBody = reader.ReadToEnd();
            return videoInfoBody;
        }
    }
}