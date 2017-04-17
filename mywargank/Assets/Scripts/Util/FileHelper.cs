using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using Util;

namespace Util
{
    public static class FileHelper
    {
        private static bool _enableWrite = true;

        public static void Copy(string sourcePath,string destPath)
        {
            if (System.IO.File.Exists(sourcePath))
            {
                if (FileExist(destPath))
                {
                    File.Delete(destPath);
                }
                File.Copy(sourcePath,destPath);
            }
        }

        public static void DeleteFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static bool FileExist(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        public static void WriteFile(string content, string filePath)
        {
  
            if (FileExist(filePath))
            {
                File.Delete(filePath);
            }
            if (_enableWrite)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
                {
                    writer.WriteLine(content);
                }
            }
        }

        private static readonly object lockobj = new object();

        public static void WriteFileAppend(string content, string filePath)
        {
            if (_enableWrite)
            {
                lock (lockobj)
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
                    {
                        writer.WriteLine(content);
                    }
                }
            }
        }

        public static string ReadFile(string filePath)
        {
            if (FileExist(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            else
            {
                WGLogger.LogError(LogModule.Debug, "Can not find file " + filePath);
            }
            return "";
        }

        public static void WriteAllBytes(string path, byte[] bytes)
        {
            if (FileExist(path))
            {
                File.Delete(path);
            }
            if (_enableWrite)
            {
                File.WriteAllBytes(path, bytes);
            }
        }

        public static string GetPath(string fileName)
        {
            string path = "";
#if UNITY_EDITOR
            string prefix = Hash128.Parse(System.IO.Directory.GetCurrentDirectory()).ToString();
            path = Application.persistentDataPath + "/" + prefix + fileName + ".txt";
#else
             path = Application.persistentDataPath + "/" + fileName + ".txt";
#endif
            return path;
        }
    }
}