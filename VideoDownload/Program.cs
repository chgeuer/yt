namespace Downloader
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            var addresses = new[] {
                //"https://www.youtube.com/watch?v=VQAAkO5B5Hg+",
                //"https://www.youtube.com/watch?v=Q5POuMHxW-0",
                //"https://www.youtube.com/watch?v=pts6F00GFuU",
                // "https://www.youtube.com/watch?v=hSoQ_Cyse2A"
                "https://www.youtube.com/watch?v=NZ5U3mJU-DE"
            };

            addresses.ToList().ForEach(D);
        }

        static void D(string a)
        {
            Func<string, string> cachedDownload = id =>
            {
                string content;
                var cacheFile = new FileInfo(string.Format("{0}.txt", id)).FullName;
                if (File.Exists(cacheFile))
                {
                    Console.WriteLine("Read cache file {0}", cacheFile);
                    content = File.ReadAllText(cacheFile);
                }
                else
                {
                    Console.WriteLine("Wrote cache file {0}", cacheFile);

                    content = YoutubeMovieData.Download(id);
                    File.WriteAllText(cacheFile, content);
                }

                return content;
            };

            var data = new YoutubeMovieData(a, 
                YoutubeMovieData.Download);
                // cachedDownload);

            var video = data.PreferredVideo;

            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads", video.Filename);

            var webClient = new WebClient();
            webClient.DownloadFileCompleted += (sender2, asyncCompletedEventArgs) =>
            {
                Console.WriteLine("Video downloaded to {0}", path);
            };

            Console.WriteLine("Download {0}", video.Url.AbsoluteUri);

            webClient.DownloadFile(video.Url, path);


            var process = new Process
            {
                StartInfo = new ProcessStartInfo(
                fileName: @"ffmpeg.exe",  
                arguments: string.Format("-i \"{0}\" -vn \"{1}\"", path, Path.ChangeExtension(path, "mp3")))
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Normal,
                    WorkingDirectory = "."
                }
            };
            var sb = new StringBuilder();
            process.OutputDataReceived += (s, _a) => sb.AppendLine("stdout " + _a.Data);
            process.ErrorDataReceived += (s, _a) => sb.AppendLine("stderr " + _a.Data);

            process.Start();
            if (process.StartInfo.RedirectStandardOutput) process.BeginOutputReadLine();
            if (process.StartInfo.RedirectStandardError) process.BeginErrorReadLine();
            process.WaitForExit();


            Console.WriteLine(sb.ToString());
        }
    }
}