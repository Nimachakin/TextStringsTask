using System.Collections.Generic;

namespace MostGamesData.ViewModels
{
    public class ComparingTextsData
    {
        public List<AnalizedStringData> FirstTextData { get; set; }
        public List<AnalizedStringData> SecondTextData { get; set; }

        public ComparingTextsData()
        {
            FirstTextData = new List<AnalizedStringData>();
            SecondTextData = new List<AnalizedStringData>();
        }

        public List<AnalizedStringData> GetTextData(string[] textStrings, float[] strIndexes)
        {
            var textData = new List<AnalizedStringData>();
            AnalizedStringData strData = null;
            
            for(int i = 0; i < textStrings.Length; i++)
            {
                string str = textStrings[i];
                float index = strIndexes[i];
                strData = new AnalizedStringData() { TextString = str, PetrenkoIndex = index };
                textData.Add(strData);
            }

            return textData;
        }
    }
}