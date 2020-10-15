using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Util.Extensions
{
    public static class FileExtensions
    {
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="orignFile">原始文件</param>
        /// <param name="newFile">新文件路径</param>
        public static void FileCoppy(string orignFile, string newFile)
        {
            if (orignFile.IsNullOrEmpty())
            {
                throw new ArgumentException(orignFile);
            }
            if (newFile.IsNullOrEmpty())
            {
                throw new ArgumentException(newFile);
            }
            System.IO.File.Copy(orignFile, newFile, true);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">路径</param>
        public static void FileDel(string path)
        {
            if (path.IsNullOrEmpty())
            {
                throw new ArgumentException(path);
            }
            System.IO.File.Delete(path);
        }
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="orignFile">原始路径</param>
        /// <param name="newFile">新路径</param>
        public static void FileMove(string orignFile, string newFile)
        {
            if (orignFile.IsNullOrEmpty())
            {
                throw new ArgumentException(orignFile);
            }
            if (newFile.IsNullOrEmpty())
            {
                throw new ArgumentException(newFile);
            }
            System.IO.File.Move(orignFile, newFile);
        }
        //创建路径
        public static void CreatePath(string FilePath)
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
        }
        //创建文件
        public static void CreateFile(string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                FileStream fs = File.Create(FilePath);
                fs.Close();
            }
        }
    }
}
