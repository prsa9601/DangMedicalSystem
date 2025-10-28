using Application.Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.UserAgg.Interfaces.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.User.Commands.SetImage
{
    public class SetImageUserCommand : IBaseCommand
    {
        public Guid userId { get; set; }
        public IFormFile userAccountImage { get; set; }
    }
    internal class SetImageUserCommandHandler : IBaseCommandHandler<SetImageUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IFileService _fileService;

        public SetImageUserCommandHandler(IUserRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(SetImageUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTracking(request.userId);
            if (user is null) return OperationResult.NotFound("کاربری یافت نشد.");

            string imageName = await _fileService.SaveFileAndGenerateName(request.userAccountImage, Directories.UserImageAccountPath);

            user.SetImageName(imageName);


            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
