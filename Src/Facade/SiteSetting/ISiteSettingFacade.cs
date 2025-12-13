using Application.SiteSettingService.CreateOrEditSiteSetting;
using Application.SiteSettingService.GetSiteSetting;
using Common.Application;
using MediatR;

namespace Facade.SiteSetting
{
    public interface ISiteSettingFacade
    {
        Task<OperationResult> CreateOrEdit(CreateOrEditCommand command);
        Task<OperationResult<Application.SiteSettingService.SiteSetting>> Get();
    }
    internal sealed class SiteSettingFacade : ISiteSettingFacade
    {
        private readonly IMediator _mediator;

        public SiteSettingFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> CreateOrEdit(CreateOrEditCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult<Application.SiteSettingService.SiteSetting>> Get()
        {
            return await _mediator.Send(new GetSiteSettingCommand { });
        }
    }
}
