using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json.Serialization;

namespace File.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        public async Task<string> FileUpload(IFormFile file)
        {
            var arry = Path.GetFileName(file.FileName).Split(".");
            var path = $"{_webHostEnvironment.ContentRootPath}/upload/{arry[0].Split("_")[0]}";
            if (!System.IO.Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //using (MemoryStream ms = new MemoryStream())
            //{

            using (FileStream fs = new FileStream($"{path}/{arry[0]}", FileMode.Create))
            {
                //await fs.WriteAsync(ms.GetBuffer());
                await file.CopyToAsync(fs);
            }
            //}


            return $"{path}/{arry[0] + "." + arry[1]}";
        }


        public class VerifyUploadData
        {
            public bool IsUpload { get; set; }
            public string hash { get; set; }
        }

        public class VerifyUploadData2
        {
            public string fileName { get; set; }
            public string hash { get; set; }
        }
        /// <summary>
        /// 验证上转文件是否存在
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="hash">hash值</param>
        /// <returns></returns>
        //[.]
        public async Task<VerifyUploadData> VerifyUpload(string fileName,string hash)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                return new VerifyUploadData { IsUpload = true, hash = hash };
            }
            var path = $"{_webHostEnvironment.ContentRootPath}/upload/{fileName}";
            if (!System.IO.Directory.Exists(path))
            {
                return new VerifyUploadData { IsUpload = true, hash = hash };
            }
            if (System.IO.File.Exists($"{path}/{hash}"))
            {
                return new VerifyUploadData { IsUpload = false, hash = hash };
            }
            return new VerifyUploadData { IsUpload = true, hash = hash };


        }



        [HttpGet]
        public async Task<string> MeUpload(string name)
        {
            var path = $"{_webHostEnvironment.ContentRootPath}/upload/{name}/";
            if (!System.IO.Directory.Exists(path))
            {
                return "有问题";
            }
            string[] arrFileNames = Directory.GetFiles(path);
            var path2 = $"{_webHostEnvironment.ContentRootPath}/upload/";
            if (!System.IO.Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream fs = new FileStream($"{path2}/{name}.mp4", FileMode.Create))
            {
                for (int i = 0; i < arrFileNames.Length; i++)
                {
                    using (FileStream fs2 = new FileStream($"{path}/{name}_{i}", FileMode.Open))
                    {

                        await fs2.CopyToAsync(fs);
                        //await fs.WriteAsync(ms.GetBuffer());
                    }
                }

                //foreach (var item in arrFileNames)
                //{
                //    using (FileStream fs2 = new FileStream(item, FileMode.Open))
                //    {
                        
                //        await fs2.CopyToAsync(fs);
                //        //await fs.WriteAsync(ms.GetBuffer());
                //    }
                //}
            }
            return "ok";
        }


            //查询改目录下所有的文件
            //DirectoryInfo root = new DirectoryInfo(path);
            //foreach (FileInfo f in root.GetFiles())
            //{
            //    string fullName = f.FullName;
            //}
            //MergeFiles(path, $"{ path}");
            //return "";

        }
        ///// <summary>
        ///// 合并文件流
        ///// </summary>
        //public void MegFiles2(string strInputDirectory)
        //{ 
        //    //获取该目录下的所有文件
        //    string[] arrFileNames = Directory.GetFiles(strInputDirectory);
        //    //打开文件流操作
        //    //using (FileStream fs=new FileStream("",)
        //    //{

        //    //}





        //    using (MemoryStream ms =new MemoryStream())
        //    {

        //    }
        //}

        ///// <summary>
        ///// 合并文件
        ///// </summary>
        ///// <param name="strDirectory"></param>
        ///// <param name="strMergeResultFile"></param>      
        //private void MergeFiles(string strInputDirectory, string strMergeResultFile)
        //{
        //    string[] arrFileNames = Directory.GetFiles(strInputDirectory);
        //    int iSumFile = arrFileNames.Length;
        //    //progressBar1.Maximum = iSumFile;
        //    FileStream AddStream = new FileStream(strMergeResultFile + "_temp", FileMode.OpenOrCreate);
        //    BinaryWriter AddWriter = new BinaryWriter(AddStream);

        //    long firstFileLength = 0;
        //    FileStream TempStream = null;
        //    BinaryReader TempReader = null;
        //    //文件长度列表
        //    string allFileLength = "";
        //    //文件长度和文件内容叠加
        //    {
        //        for (int i = 0; i < iSumFile; i++)
        //        {
        //            TempStream = new FileStream(arrFileNames[i], FileMode.Open);
        //            TempReader = new BinaryReader(TempStream);
        //            AddWriter.Write(TempReader.ReadBytes((int)TempStream.Length));
        //            if (firstFileLength == 0)
        //            {
        //                firstFileLength = TempReader.BaseStream.Length;
        //                allFileLength = firstFileLength.ToString().PadLeft(20, '0');
        //            }
        //            //暂时只处理2个文件的情况，下面是多文件
        //            //allFileLength += "," + TempReader.BaseStream.Length.ToString().PadLeft(20, '0');

        //            TempReader.Close();
        //            TempStream.Close();
        //        }
        //        allFileLength = allFileLength.Trim(',');

        //        //释放资源
        //        AddWriter.Flush();
        //        AddWriter.Close();
        //        AddStream.Close();
        //        TempStream = null;
        //        TempReader = null;
        //    }
        //    //创建含有头信息的文件- 也达到了加密的效果
        //    {
        //        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        //        byte[] BytesMessage = UTF8.GetBytes(allFileLength);
        //        AddStream = new FileStream(strMergeResultFile + "_r", FileMode.OpenOrCreate);
        //        AddWriter = new BinaryWriter(AddStream);
        //        //文件头
        //        AddWriter.Write(BytesMessage);

        //        //写入原来文件
        //        TempStream = new FileStream(strMergeResultFile, FileMode.Open);
        //        TempReader = new BinaryReader(TempStream);
        //        AddWriter.Write(TempReader.ReadBytes((int)TempStream.Length));
        //        long cc = TempReader.BaseStream.Length;

        //        TempReader.Close();
        //        AddWriter.Flush();
        //        AddWriter.Close();
        //        TempStream.Close();
        //        //释放
        //        TempReader = null;
        //        AddWriter = null;
        //        TempStream = null;
        //    }
        //    //删掉临时
        //    System.IO.File.Delete(strMergeResultFile + "_temp");
        //}

    
}
