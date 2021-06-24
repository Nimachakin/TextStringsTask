using System.Collections.Generic;
using System.Linq;
using MostGamesData.DatabaseModels;
using MostGamesData.JsonModels;
using MostGamesData.Abstractions;

namespace MostGamesData.Implementations
{
    // Implementation of ITextRepositoryService for SqlServer DBMS
    public class SqlServerTextRepository : ITextRepositoryService
    {
        private readonly TextContext context;

        public SqlServerTextRepository(TextContext db)
        {
            context = db;
        }
        
        // Gets data from database repository by id
        public TextModelJson Get(int? id)
        {
            if(id == null)
            {
                return null;
            }

            SimpleText simpleText = context.SimpleTexts
                .Where(t => t.Id == id)
                .FirstOrDefault();
            
            if(simpleText == null)
            {
                return null;
            }

            return new TextModelJson(simpleText.Text);         
        }

        // Gets all data from database repository
        public List<TextModelJson> GetAll()
        {
            List<TextModelJson> texts = (
                from str in context.SimpleTexts 
                select new TextModelJson(str.Text)
            ).ToList();

            return texts;
        }
    }
}