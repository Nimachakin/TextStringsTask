using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MostGamesData.JsonModels;

namespace MostGamesWeb.Controllers
{
    public class TextController : Controller
    {
        // Counts words and vowels amount in the string 'str' 
        // and returns the results as a JSON model.
        [HttpGet]
        public JsonResult AnalizeTheString(string str)
        {
            if(string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            
            // Remove excess whitespaces inside string of text
            while(str.Trim().Contains("  "))
            {
                str.Replace("  ", " ");
            }

            var viewModel = new CountedTextModelJson();
            viewModel.Text = str;
            viewModel.WordsCount = str.Split(' ').Length;
            viewModel.VowelsCount = CountStringVowels(str);

            return Json(viewModel);
        }

        // Counts all the instances of russian and european vowels 
        // in the string 'str' and returns its amount
        private int CountStringVowels(string str)
        {
            if(string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }

            HashSet<char> ruAndEuVowels = new HashSet<char> 
            { 
                'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я', 
                'a', 'e', 'i', 'o', 'u', 
                'ä', 'ö', 'ü' 
            };
            
            int result = str.ToLower().Where(w => ruAndEuVowels.Contains(w)).Count();             
            return result;
        } 
    }
}
