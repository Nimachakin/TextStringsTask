using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MostGamesData.DatabaseModels
{
    // Database context for "TextsCatalog" database
    public class TextContext : DbContext
    {
        public DbSet<SimpleText> SimpleTexts { get; set; }

        public TextContext(DbContextOptions<TextContext> options)
            :base(options) 
        {
            Database.EnsureCreated();
        }

        public async Task InitializeAsync()
        {
            if(!SimpleTexts.Any())
            {
                var textsData = new List<SimpleText>() {
                    new SimpleText("Буря мглою небо кроет."), 
                    new SimpleText(string.Concat("O for a Muse of fire, that would ascend ", 
                        "the brightest heaven of invention, a kingdom for a stage, ", 
                        "princes to act and monacrchs to behold the swelling scene!")), 
                    new SimpleText(string.Concat("Der Holle Rache kocht in meinem Herzen, ",   
                        "Tod und Verzweiflung flammet um mich her!")) 
                };

                await SimpleTexts.AddRangeAsync(textsData);
                await SaveChangesAsync();
            }            
        }
    }
}