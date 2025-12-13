using Application.SiteSettingService;
using Application.SiteSettingService.CreateOrEditSiteSetting;
using Common.AspNetCore;
using Facade.SiteSetting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteSettingController : ApiController
    {
        private readonly ISiteSettingFacade _facade;

        public SiteSettingController(ISiteSettingFacade facade)
        {
            _facade = facade;
        }

        [HttpPost("CreateOrEditSiteSetting")]
        public async Task<ApiResult> CreateOrEditSiteSetting(CreateOrEditCommand command)
        {
            return CommandResult(await _facade.CreateOrEdit(command));
        }
      
        [HttpGet("GetSiteSetting")]
        public async Task<ApiResult<SiteSetting>> GetSiteSetting()
        {
            return CommandResult(await _facade.Get());
        }
      

    }
}
