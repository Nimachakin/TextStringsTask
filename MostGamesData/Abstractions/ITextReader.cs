using System.Threading.Tasks;

namespace MostGamesData.Abstractions
{
    public interface ITextReader
    {
        Task<string[]> GetStringsAsync();
    }
}