using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MostGamesData.Abstractions;

namespace MostGamesData.Implementations
{
    public class FileStringReader : IFileStringReader
    {
        public IFormFile TextFile { get; }

        public FileStringReader(IFormFile textFile)
        {
            TextFile = textFile;
        }

        public async Task<string[]> GetStringsAsync()
        {
            var textStrings = new List<string>();

            using(var reader = new StreamReader(TextFile.OpenReadStream()))
            {
                string textString = await reader.ReadLineAsync();

                while(!string.IsNullOrEmpty(textString))
                {
                    textStrings.Add(textString);
                    textString = reader.ReadLine();
                }
            }

            return textStrings.ToArray();
        }
    }
}