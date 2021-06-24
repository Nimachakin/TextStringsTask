using System.Linq;
using MostGamesData.Abstractions;

namespace MostGamesData.Implementations
{
    public class RuStringIndexEstimator : IStringIndexEstimator
    {
        public float EstimateIndex(string str)
        {
            string cuttedString = string
                .Join<char>("", str.Where(c => char.IsLetterOrDigit(c)));            

            if(cuttedString.Length == 0)
            {
                return 0f;
            }

            float strIndex = 0.5f; 
            float indexIncrement = strIndex;            
            int stringLength = cuttedString.Length;             

            for(int i = 1; i < stringLength; i++)
            {
                indexIncrement += 1;
                strIndex += indexIncrement;
            }

            strIndex = strIndex * stringLength;
            return strIndex;  
        }
    }
}