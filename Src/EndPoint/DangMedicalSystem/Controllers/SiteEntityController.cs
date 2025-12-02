using Common.AspNetCore;
using Facade.SitEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Query.SiteEntity.DTOs;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteEntityController : ApiController
    {
        private readonly ISiteEntityFacade _facade;

        public SiteEntityController(ISiteEntityFacade facade)
        {
            _facade = facade;
        }

        [HttpGet("GetMainPage")]
        public async Task<ApiResult<MainPageDto?>> GetMainPage()
        {
            return QueryResult(await _facade.GetMainPage());
        }
    }
}
