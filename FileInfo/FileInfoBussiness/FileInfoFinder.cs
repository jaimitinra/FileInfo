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
                if (string.IsNullOrWhiteSpace(piece))
                    continue;
                var gaplessPiece = piece.Trim();
                if (!dict.ContainsKey(gaplessPiece))
                    dict.Add(gaplessPiece, 1);
                else
                    dict[gaplessPiece]++;
           }
            return new FileInfoResult
                {
                    Keys = dict
                };
        }
    }
}