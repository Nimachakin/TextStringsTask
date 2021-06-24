using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MostGamesData.Implementations;
using MostGamesData.ViewModels;
using MostGamesData.Abstractions;

namespace MostGamesWeb.Controllers
{
    public class StringMatchingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompareFileTexts(IFormFile ruTextFile, IFormFile enTextFile)
        {
            if(ruTextFile == null || enTextFile == null)
            {
                return BadRequest("Необходимо выбрать два файловых текста!");
            }

            string[] ruTextStrings = await GetStringsFromText(new FileStringReader(ruTextFile));
            string[] enTextStrings = await GetStringsFromText(new FileStringReader(enTextFile));
            float[] ruStringsIndexes = GetStringsIndexes(ruTextStrings, new RuStringIndexEstimator());
            float[] enStringsIndexes = GetStringsIndexes(enTextStrings, new EnStringIndexEstimator());

            var textsData = new ComparingTextsData();
            textsData.FirstTextData = textsData.GetTextData(ruTextStrings, ruStringsIndexes);
            textsData.SecondTextData = textsData.GetTextData(enTextStrings, enStringsIndexes);
            Dictionary<string, string> stringsMatchResult = MatchStringsData(textsData);

            return PartialView("ComparingResultPartial", stringsMatchResult);
        }

        private async Task<string[]> GetStringsFromText(IFileStringReader analizingService)
        {
            string[] textStrings = await analizingService.GetStringsAsync();            
            return textStrings.Distinct().ToArray();
        }

        private float[] GetStringsIndexes(string[] textStrings, IStringIndexEstimator indexEstimator)
        {
            float[] stringsIndexes = new float[textStrings.Length];

            for(int i = 0; i < textStrings.Length; i++)
            {
                stringsIndexes[i] = indexEstimator.EstimateIndex(textStrings[i]);
            }

            return stringsIndexes;
        }

        private Dictionary<string, string> MatchStringsData(ComparingTextsData textsData)
        {
            var stringMatches = new Dictionary<string, string>();

            foreach(var strData in textsData.FirstTextData)
            {
                string[] matches = textsData.SecondTextData
                    .Where(data => data.PetrenkoIndex == strData.PetrenkoIndex)
                    .Select(data => data.TextString).ToArray();
                
                if(matches.Length > 0)
                {
                    string matchedStringsText = string.Join(" \n", matches);
                    stringMatches.Add(strData.TextString, matchedStringsText);
                }                
            }

            return stringMatches;
        }

        [HttpPost]
        public float CountStringIndex(string str)
        {
            if(!string.IsNullOrEmpty(str))
            {
                return new RuStringIndexEstimator().EstimateIndex(str);
            }
            else
            {
                return 0f;
            }
        }
    }
}