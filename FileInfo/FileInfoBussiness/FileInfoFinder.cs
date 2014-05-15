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
            var dict = new Dictionary<string,int>();
            foreach (var piece in SplittedFileContent(fileContentStringify))
            {
                var gaplessPiece = piece.Trim();
                if (string.IsNullOrWhiteSpace(piece))
                    continue;
                IncreaseOneKeyValuey(dict, gaplessPiece);
           }
            return new FileInfoResult { Keys = dict };
        }

        private void IncreaseOneKeyValuey(Dictionary<string, int> dict, string gaplessPiece)
        {
            if (!dict.ContainsKey(gaplessPiece))
                dict.Add(gaplessPiece, 1);
            else
                dict[gaplessPiece]++;
        }

        private IEnumerable<string> SplittedFileContent(string fileContentStringify)
        {
            return fileContentStringify.Split(new[] {' '});
        }
    }
}