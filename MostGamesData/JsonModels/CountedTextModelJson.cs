namespace MostGamesData.JsonModels
{
    // String analysis data JSON model
    public class CountedTextModelJson
    {
        public string Text { get; set; }
        public int WordsCount { get; set; }
        public int VowelsCount { get; set; }
    }
}