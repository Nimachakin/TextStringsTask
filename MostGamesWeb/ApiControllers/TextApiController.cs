using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MostGamesData.Abstractions;
using MostGamesData.JsonModels;

namespace MostGamesWeb.ApiControllers
{
    [ApiController]
    [Route("/api/textstrings")]
    public class TextStringsController : ControllerBase
    {
        private readonly ITextRepositoryService textDataRepo;

        public TextStringsController(ITextRepositoryService service)
        {
            textDataRepo = service;
        }

        [HttpGet]
        public List<TextModelJson> GetAll()
        {
            return textDataRepo.GetAll();
        }

        [HttpGet("{id}")]
        public TextModelJson Get(int? id)
        {
            return textDataRepo.Get(id);
        }
    }
}
