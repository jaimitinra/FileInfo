using System.Collections.Generic;

namespace FileInfoBussiness
{
    public class FileInfoFinder
    {
        public FileInfoRepository Repository { get; set; }

        public FileInfoResult Analize(string pattern)
        {
            return new FileInfoResult();
        }
    }
}