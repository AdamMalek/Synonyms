using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synonyms.Models;
using Synonyms.Models.ViewModels;
using Synonyms.Services;

namespace Synonyms.Controllers
{
    [Produces("application/json")]
    [Route("api/Synonyms")]
    public class SynonymsController : Controller
    {
        private readonly ISynonymService _synonymService;

        public SynonymsController(ISynonymService synonymService)
        {
            _synonymService = synonymService;
        }

        [HttpGet]
        public SynonymVM[] Get()
        {
            return _synonymService.GetAll()
                                  .Select(x => new SynonymVM { Term = x.Key, Synonyms = x.Value })
                                  .ToArray();
        }

        [HttpPost]
        public ApiResponse Post([FromBody]AddSynonymVM vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _synonymService.AddSynonym(vm.Term, vm.Synonyms);
                    return new ApiResponse
                    {
                        Message = "Success",
                        Success = true
                    };
                }
                catch (Exception e)
                {
                    Response.StatusCode = 500;
                    return new ApiResponse
                    {
                        Message = e.Message,
                        Success = false
                    };
                }
            }
            else
            {
                Response.StatusCode = 400;
                return new ApiResponse
                {
                    Message = "Invalid data",
                    Success = false
                };
            }
        }
    }
}