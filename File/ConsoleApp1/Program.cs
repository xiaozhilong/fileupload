using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = new MediaFile { Filename = @"C:\Users\xzl\Desktop\终结者-黑暗命运.mp4" };

            using (var engine = new Engine(@"C:\ffmpeg\bin\ffmpeg.exe"))
            {
                engine.GetMetadata(inputFile);
            }

            Console.WriteLine(inputFile.Metadata.Duration);
            Console.ReadLine();
        }
    }
}
