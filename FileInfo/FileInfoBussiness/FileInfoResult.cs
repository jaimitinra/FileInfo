using System.Collections.Generic;

namespace FileInfoBussiness
{
    public class FileInfoResult
    {
        public List<KeyValuePair<string, int>> Keys { get; set; }

        public FileInfoResult()
        {
            Keys = new List<KeyValuePair<string, int>>();
        }
    }
}