using System;
using System.IO;

namespace ClassLibrary
{
    public class FileProcess
    {
        public bool FileExists(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException("FileName");
            }

            return File.Exists(fileName);
        }
    }
}
