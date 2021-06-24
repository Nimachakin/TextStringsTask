using Microsoft.AspNetCore.Http;

namespace MostGamesData.Abstractions
{
    public interface IFileStringReader : ITextReader
    {
        IFormFile TextFile { get; }
    }
}