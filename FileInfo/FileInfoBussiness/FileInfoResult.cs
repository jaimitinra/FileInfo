using System.Collections.Generic;

namespace FileInfoBussiness
{
    public class FileInfoResult
    {
        public Dictionary<string, int> Keys { get; set; }

        public FileInfoResult()
        {
            Keys = new Dictionary<string, int>();
        }
    }
}