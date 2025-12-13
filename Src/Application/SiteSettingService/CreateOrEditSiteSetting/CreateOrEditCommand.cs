using Common.Application;

namespace Application.SiteSettingService.CreateOrEditSiteSetting
{
    public class CreateOrEditCommand : IBaseCommand
    {
        public required string ComparyName { get; set; }
    }
    internal class CreateOrEditCommandHandler : IBaseCommandHandler<CreateOrEditCommand>
    {
        public async Task<OperationResult> Handle(CreateOrEditCommand request, CancellationToken cancellationToken)
        {
            var siteSetting = SiteSetting.Instance;
            siteSetting.SetCompanyName(request.ComparyName);

            return OperationResult.Success();
        }
    }
}
