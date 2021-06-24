using System.Collections.Generic;
using MostGamesData.JsonModels;

namespace MostGamesData.Abstractions
{
    public interface ITextRepositoryService
    {
        List<TextModelJson> GetAll();
        TextModelJson Get(int? id);
    }
}