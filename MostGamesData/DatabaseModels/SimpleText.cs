namespace MostGamesData.DatabaseModels
{
    // Data model for table "SimpleTexts" in "TextsCatalog" database
    public class SimpleText
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public SimpleText(string text)
        {
            Text = text;
        }

        public SimpleText() { }
    }
}