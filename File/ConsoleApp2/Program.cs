using FFmpeg.NET;
using FFmpeg.NET.Events;
using System;

namespace ConsoleApp2
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var inputFile = new MediaFile(@"C:\Users\xzl\Desktop\终结者-黑暗命运.mp4");
            var ffmpeg = new Engine(@"C:\ffmpeg\bin\ffmpeg.exe");
            ffmpeg.Progress += OnProgress;
            ffmpeg.Data += OnData;
            ffmpeg.Error += OnError;
            ffmpeg.Complete += OnComplete;
            var outputFile = new MediaFile(@"C:\Users\xzl\Desktop\图片\1.jpg");
            // Saves the frame located on the 15th second of the video.
            var options = new ConversionOptions { Seek = TimeSpan.FromSeconds(190) };
            await ffmpeg.GetThumbnailAsync(inputFile, outputFile, options);
            //var metadata = await ffmpeg.GetMetaDataAsync(inputFile);
            //var m = Math.Ceiling(metadata.Duration.TotalSeconds / (60 * 1));
            //int szie = 1 * 60;//10分钟
            //ffmpeg.Progress += OnProgress;
            //ffmpeg.Data += OnData;
            //ffmpeg.Error += OnError;
            //ffmpeg.Complete += OnComplete;
            //for (int i = 0; i < m; i++)
            //{
            //    var outputFile = new MediaFile(@"C:\Users\xzl\Desktop\终结者黑暗命运\"+i+".mp4");
            //    var options = new ConversionOptions();
            //    options.CutMedia(TimeSpan.FromSeconds(i* szie), TimeSpan.FromSeconds(szie));
            //    await ffmpeg.ConvertAsync(inputFile, outputFile, options);
            //}
            Console.WriteLine();
        }
        private static void OnProgress(object sender, ConversionProgressEventArgs e)
        {
            Console.WriteLine("[{0} => {1}]", e.Input.FileInfo.Name, e.Output.FileInfo.Name);
            Console.WriteLine("Bitrate: {0}", e.Bitrate);
            Console.WriteLine("Fps: {0}", e.Fps);
            Console.WriteLine("Frame: {0}", e.Frame);
            Console.WriteLine("ProcessedDuration: {0}", e.ProcessedDuration);
            Console.WriteLine("Size: {0} kb", e.SizeKb);
            Console.WriteLine("TotalDuration: {0}\n", e.TotalDuration);
        }

        private static void OnData(object sender, ConversionDataEventArgs e)
        {
            Console.WriteLine("[{0} => {1}]: {2}", e.Input.FileInfo.Name, e.Output.FileInfo.Name, e.Data);
        }

        private static void OnComplete(object sender, ConversionCompleteEventArgs e)
        { 
            Console.WriteLine("Completed conversion from {0} to {1}", e.Input.FileInfo.FullName, e.Output.FileInfo.FullName);
        }

        private static void OnError(object sender, ConversionErrorEventArgs e)
        {
            Console.WriteLine("[{0} => {1}]: Error: {2}\n{3}", e.Input.FileInfo.Name, e.Output.FileInfo.Name, e.Exception.ExitCode, e.Exception.InnerException);
        }
    }
}
