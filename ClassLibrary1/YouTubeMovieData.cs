namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Policy;
    using System.Text;
    using System.Threading.Tasks;
    using NVK = System.Collections.Generic.Dictionary<string, string>;

    public static class MyExtensions
    {
        public static string HtmlDecode(this string str) { return SimpleOwin.Middlewares.Helpers.HttpUtility.HtmlDecode(str); }
        public static Dictionary<string, string> ParseQueryString(this string str) { return SimpleOwin.Middlewares.Helpers.HttpUtility.ParseQueryString(str).AsDictionary(); }

        public static Dictionary<string, string> AsDictionary(this NameValueCollection nvc)
        {
            var result = new Dictionary<string, string>();
            foreach (var key in nvc.Keys)
            {
                result[(string)key] = nvc[(string)key];
            }
            return result;
        }
    }


    public class YoutubeMovieData
    {
        
        #region properties
        public string account_playback_token { get; set; }
        public NVK adaptive_fmts { get; set; }
        public bool allow_embed { get; set; }
        public bool allow_ratings { get; set; }
        public string author { get; set; }
        public double avg_rating { get; set; }
        public string c { get; set; }
        public NVK caption_audio_tracks { get; set; }
        public NVK caption_tracks { get; set; }
        public NVK caption_translation_languages { get; set; }
        public string cc3_module { get; set; }
        public string cc_asr { get; set; }
        public string cc_font { get; set; }
        public string cc_fonts_url { get; set; }
        public string cc_module { get; set; }
        public string cl { get; set; }
        public string csi_page_type { get; set; }
        public string dashmpd { get; set; }
        public string default_audio_track_index { get; set; }
        public string enablecsi { get; set; }
        public string eventid { get; set; }
        public string fexp { get; set; }
        public string fmt_list { get; set; }
        public string has_cc { get; set; }
        public string hl { get; set; }
        public string idpj { get; set; }
        public string iurl { get; set; }
        public string iurlhq { get; set; }
        public string iurlmaxres { get; set; }
        public string iurlmq { get; set; }
        public string iurlsd { get; set; }
        public string keywords { get; set; }
        public string ldpj { get; set; }
        public string length_seconds { get; set; }
        public string loudness { get; set; }
        public string muted { get; set; }
        public string no_get_video_log { get; set; }
        public string of { get; set; }
        public string plid { get; set; }
        public string pltype { get; set; }
        public string probe_url { get; set; }
        public string ptk { get; set; }
        public string status { get; set; }
        public string storyboard_spec { get; set; }
        public string subtitles_xlb { get; set; }
        public string thumbnail_url { get; set; }
        public string timestamp { get; set; }
        public string title { get; set; }
        public string tmi { get; set; }
        public string token { get; set; }
        public string ttsurl { get; set; }
        public string url_encoded_fmt_stream_map { get; set; }
        public string use_cipher_signature { get; set; }
        public string video_id { get; set; }
        public string video_verticals { get; set; }
        public string view_count { get; set; }
        public string watermark { get; set; }
        
        #endregion
        public string reason { get; set; }

        public List<Video> Videos { get; set; }

        public Video GetVideoByITag(IEnumerable<int> itags)
        {
            var video = itags
                .Select(itag => this.Videos.FirstOrDefault(_ => _.itag == itag))
                .FirstOrDefault(_ => _ != null);

            return video;
        }

        public Video PreferredVideo
        {
            get
            {
                return this.GetVideoByITag(new[] {
                    22, // hd720 - mp4
                    18, // medium - mp4
                    43 // medium - webm
                });
            }
        }

        public class Video
        {
            public int itag { get; set; }
            public Uri Url { get; set; }
            public string fallback_host { get; set; }
            public string type { get; set; }
            public string quality { get; set; }
            public string Format { get; set; }
            public string Extension { get; set; }
            public string Filename { get; set; }


            public Video(string data, string title)
            {
                var parsed = data.HtmlDecode().ParseQueryString();
                Func<string, string> param = key => parsed[key].HtmlDecode();

                this.itag = int.Parse(param("itag"));
                this.Url = new Uri(param("url"));
                this.type = param("type");
                this.quality = param("quality");
                this.fallback_host = param("fallback_host");

                this.Format = this.quality + " - " + this.type.Split(';')[0].Split('/')[1];
                this.Extension = this.type.Split(';')[0].Split('/')[1];

                title = title.Replace(":", "-");

                this.Filename = string.Format("{0} ({1}).{2}", title, this.Format, this.Extension);
            }
        }

        public YoutubeMovieData(string address, Func<string,string> downloader)
        {
            var id = new Uri(address).Query.ParseQueryString()["v"];
            var infoText = downloader(id);
            var infoParsed = infoText.HtmlDecode().ParseQueryString();

            #region set everything
            this.account_playback_token = infoParsed["account_playback_token"];
            this.adaptive_fmts = infoParsed["adaptive_fmts"].HtmlDecode().ParseQueryString();
            this.allow_embed = 0 != int.Parse(infoParsed["allow_embed"]);
            this.allow_ratings = 0 != int.Parse(infoParsed["allow_ratings"]);
            this.author = infoParsed["author"];
            this.avg_rating = double.Parse(infoParsed["avg_rating"]);
            this.c = infoParsed["c"];
            this.caption_audio_tracks = infoParsed["caption_audio_tracks"].HtmlDecode().ParseQueryString();
            this.caption_tracks = infoParsed["caption_tracks"].HtmlDecode().ParseQueryString();
            this.caption_translation_languages = infoParsed["caption_translation_languages"].HtmlDecode().ParseQueryString();
            this.cc3_module = infoParsed["cc3_module"];
            this.cc_asr = infoParsed["cc_asr"];
            this.cc_font = infoParsed["cc_font"];
            this.cc_fonts_url = infoParsed["cc_fonts_url"];
            this.cc_module = infoParsed["cc_module"];
            this.cl = infoParsed["cl"];
            this.csi_page_type = infoParsed["csi_page_type"];
            this.dashmpd = infoParsed["dashmpd"];
            this.default_audio_track_index = infoParsed["default_audio_track_index"];
            this.enablecsi = infoParsed["enablecsi"];
            this.eventid = infoParsed["eventid"];
            this.fexp = infoParsed["fexp"];
            this.fmt_list = infoParsed["fmt_list"];
            this.has_cc = infoParsed["has_cc"];
            this.hl = infoParsed["hl"];
            this.idpj = infoParsed["idpj"];
            this.iurl = infoParsed["iurl"];
            this.iurlhq = infoParsed["iurlhq"];
            this.iurlmaxres = infoParsed["iurlmaxres"];
            this.iurlmq = infoParsed["iurlmq"];
            this.iurlsd = infoParsed["iurlsd"];
            this.keywords = infoParsed["keywords"];
            this.ldpj = infoParsed["ldpj"];
            this.length_seconds = infoParsed["length_seconds"];
            this.loudness = infoParsed["loudness"];
            this.muted = infoParsed["muted"];
            this.no_get_video_log = infoParsed["no_get_video_log"];
            this.of = infoParsed["of"];
            this.plid = infoParsed["plid"];
            this.pltype = infoParsed["pltype"];
            this.probe_url = infoParsed["probe_url"];
            this.ptk = infoParsed["ptk"];
            this.status = infoParsed["status"];
            this.storyboard_spec = infoParsed["storyboard_spec"];
            this.subtitles_xlb = infoParsed["subtitles_xlb"];
            this.thumbnail_url = infoParsed["thumbnail_url"];
            this.timestamp = infoParsed["timestamp"];
            this.title = infoParsed["title"];
            this.tmi = infoParsed["tmi"];
            this.token = infoParsed["token"];
            this.ttsurl = infoParsed["ttsurl"];
            this.url_encoded_fmt_stream_map = infoParsed["url_encoded_fmt_stream_map"];
            this.use_cipher_signature = infoParsed["use_cipher_signature"];
            this.video_id = infoParsed["video_id"];
            this.video_verticals = infoParsed["video_verticals"];
            this.view_count = infoParsed["view_count"];
            this.watermark = infoParsed["watermark"];
            #endregion

            this.reason = infoParsed.ContainsKey("reason") ? infoParsed["reason"] : null;
            this.Videos = infoParsed["url_encoded_fmt_stream_map"].Split(',').Select(data => new Video(data, this.title)).ToList();
        }


        public static string Download(string videoId)
        {
            var videoInfoUrl = string.Format("http://www.youtube.com/get_video_info?video_id={0}", videoId);

            var client = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(videoInfoUrl));
            requestMessage.Headers.Add("X-Forwarded-For", "187.72.88.32");
            var httpResponse = client.SendAsync(requestMessage).Result;
            var x = httpResponse.Content.ReadAsStringAsync().Result;


            var request = (HttpWebRequest)WebRequest.Create(videoInfoUrl);
            var response = (HttpWebResponse)(request.GetResponse());
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream, Encoding.UTF8);
            var videoInfoBody = reader.ReadToEnd();
            return videoInfoBody;
        }
    }
}
