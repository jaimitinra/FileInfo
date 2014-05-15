using System.Collections.Generic;

namespace FileInfoBussiness
{
    public class FileInfoFinder
    {
        public FileInfoRepository Repository { get; set; }

        public FileInfoResult Analize(string pattern)
        {
            var fileContentStringify = Repository.GetContent(pattern); 
            if(string.IsNullOrWhiteSpace(fileContentStringify))
                return new FileInfoResult();
            return new FileInfoResult
                {
                    Keys = new List<KeyValuePair<string, int>>
                        {
                            new KeyValuePair<string, int>(fileContentStringify.ToLower(),1)
                        }
                };
        }
    }
}