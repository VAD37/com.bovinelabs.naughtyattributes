using UnityEngine;
using System.IO;

namespace NaughtyAttributes.Editor
{
    public static class IOUtility
    {
        public static string GetPersistentDataPath()
        {
            return Application.persistentDataPath + "/";
        }

        public static void WriteToFile(string filePath, string content)
        {
            var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            var streamWriter = new StreamWriter(fileStream, System.Text.Encoding.ASCII);
            streamWriter.WriteLine(content);
            fileStream.Dispose();
            streamWriter.Dispose();
        }

        public static string ReadFromFile(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var streamReader = new StreamReader(fileStream, System.Text.Encoding.ASCII);
            var content = streamReader.ReadToEnd();
            fileStream.Dispose();
            streamReader.Dispose();
            return content;
        }

        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static string GetPathRelativeToProjectFolder(string fullPath)
        {
            int indexOfAssetsWord = fullPath.IndexOf("\\Assets");
            string relativePath = fullPath.Substring(indexOfAssetsWord + 1);

            return relativePath;
        }
    }
}
