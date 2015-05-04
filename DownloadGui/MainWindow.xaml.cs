namespace DownloadGui
{
    using Downloader;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Windows;


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var a = downloadUrl.Text;

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

            webClient.DownloadFile(video.Url, path);

            if (createAudioCheckbox.IsChecked.HasValue && createAudioCheckbox.IsChecked.Value)
            {

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
            }


            if (createAudioCheckbox.IsChecked.HasValue && createAudioCheckbox.IsChecked.Value)
            {
                Process.Start(
                    fileName: "explorer.exe",
                    arguments: string.Format("/select, \"{0}\"", Path.ChangeExtension(path, "mp3")));
            }
            else
            {
                Process.Start(
                    fileName: "explorer.exe",
                    arguments: string.Format("/select, \"{0}\"", path));
            }
        }
    }
}