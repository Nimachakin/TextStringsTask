using System.Linq;
using MostGamesData.Abstractions;

namespace MostGamesData.Implementations
{
    public class EnStringIndexEstimator : IStringIndexEstimator
    {
        public float EstimateIndex(string str)
        {
            string[] strParts = str.Split('|');

            if(strParts.Length < 2)
            {
                return 0f;
            }

            string mainPart = strParts[0];
            string comment = strParts[1];
            float indexResult = 0f;

            foreach(string stringPart in new string[] { mainPart, comment })
            {
                string cuttedString = string
                    .Join<char>("", stringPart.Where(c => char.IsLetterOrDigit(c)));            

                if(cuttedString.Length == 0)
                {
                    return 0f;
                }

                float stringPartIndex = 0.5f; 
                float indexIncrement = stringPartIndex;            
                int stringLength = cuttedString.Length;             

                for(int i = 1; i < stringLength; i++)
                {
                    indexIncrement += 1;
                    stringPartIndex += indexIncrement;
                }

                stringPartIndex = stringPartIndex * stringLength;
                indexResult += stringPartIndex;
            }

            return indexResult;  
        }
    }
}