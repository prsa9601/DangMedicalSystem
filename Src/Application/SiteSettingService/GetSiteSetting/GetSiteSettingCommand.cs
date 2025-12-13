using Common.Application;
using MediatR;

namespace Application.SiteSettingService.GetSiteSetting
{
    public class GetSiteSettingCommand : IBaseCommand<SiteSetting>
    {
    }
    internal class GetSiteSettingCommandHandler : IBaseCommandHandler<GetSiteSettingCommand, SiteSetting>
    {
        public async Task<OperationResult<SiteSetting>> Handle(GetSiteSettingCommand request, CancellationToken cancellationToken)
        {
            var siteSetting = SiteSetting.Instance;

            return OperationResult<SiteSetting>.Success(siteSetting);
        }
    }
}
