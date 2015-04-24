using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            // http://www.ecanarys.com/blog-entry/how-download-youtube-videos-using-net


            Func<string, string> HtmlDecode = WebUtility.HtmlDecode;
            Func<string, string, string> parameter = (encodedQueryString, key) =>
            {

                var parsed = Flurl.QueryParamCollection.Parse(HtmlDecode(encodedQueryString));
                if (!parsed.ContainsKey(key)) return string.Empty;
                return parsed[key] as string;
            };


            Uri videoUri = new Uri("http://www.youtube.com/watch?v=aqkkByP8RDM");
            string videoID = "aqkkByP8RDM"; // HttpUtility.ParseQueryString(videoUri.Query).Get("v");
            string videoInfoUrl = "http://www.youtube.com/get_video_info?video_id=" + videoID;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(videoInfoUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
            string videoInfo = HtmlDecode(reader.ReadToEnd());

            var videoParams = Flurl.QueryParamCollection.Parse(videoInfo);
            // var lblMessage = videoParams["reason"];
            var url_encoded_fmt_stream_map = (string)videoParams["url_encoded_fmt_stream_map"];

            string[] videoURLs = url_encoded_fmt_stream_map.Split(',');

            foreach (string vURL in videoURLs)
            {
                var sURL = parameter(vURL, "url") 
                    + "&signature=" + parameter(vURL, "sig")
                    + "&type=" + parameter(vURL, "type")
                    + "&title=" + parameter(vURL, "title");

                var videoFormat = parameter(vURL, "type")
                    + parameter(vURL, "quality")
                    + " - "
                    + parameter(vURL, "type").Split(';')[0].Split('/')[1];

                ;
            }
        }
    }
}
