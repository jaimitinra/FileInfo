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
            var stringSplitted = fileContentStringify.ToLower().Split(new[] {' '});
            var dict = new Dictionary<string,int>();
            foreach (var piece in stringSplitted)
            {
                if (!dict.ContainsKey(piece))
                    dict.Add(piece, 1);
                else
                    dict[piece]++;
           }
            return new FileInfoResult
                {
                    Keys = dict
                };
        }
    }
}