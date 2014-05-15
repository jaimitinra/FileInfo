using System.Collections.Generic;
using System.Linq;

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
                if (string.IsNullOrWhiteSpace(gaplessPiece)) continue;
                if (IsNotRelevantKey(gaplessPiece)) continue;
                IncreaseOneKeyValuey(dict, gaplessPiece);
           }
            return new FileInfoResult { Keys = dict };
        }

        private bool IsNotRelevantKey(string gaplessPiece)
        {
            if (gaplessPiece.Length < 3 && FirstIsNotCapitalLetter(gaplessPiece[0]))
                return true;
            var notRelevantKeys = new List<string>
                {
                    "los","del"
                };
            return notRelevantKeys.Any(x => x == gaplessPiece);
        }

        private bool FirstIsNotCapitalLetter(char c)
        {
            var capitalLetters = new List<char>
                {
                    'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Ñ','O','P','Q','R','S','T','U','V','W','X','Y','Z'
                };
            return capitalLetters.All(x => x != c);
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