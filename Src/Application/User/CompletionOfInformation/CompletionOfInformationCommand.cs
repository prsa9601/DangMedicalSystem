using Application.Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.UserAgg.Enum;
using Domain.UserAgg.Interfaces.Builder;
using Domain.UserAgg.Interfaces.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.User.CompletionOfInformation
{
    public class CompletionOfInformationCommand : IBaseCommand
    {
        public required Guid userId { get; set; }
        public required string nationalityCode { get; set; }
        public required IFormFile nationalCardPhoto { get; set; }
        public required IFormFile birthCertificatePhoto { get; set; }
    }
    internal sealed class CompletionOfInformationCommandHandler : IBaseCommandHandler<CompletionOfInformationCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IFileService _fileService;
        private readonly IUserBuilder _builder;

        public CompletionOfInformationCommandHandler(IUserRepository repository, IUserBuilder builder, IFileService fileService)
        {
            _repository = repository;
            _builder = builder;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(CompletionOfInformationCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTracking(request.userId);
            if (user == null) return OperationResult.NotFound("خطای سمت سرور لطفا دقایقی دیگر دوباره تلاش کنید.");


            user.SetUserStatus(UserStatus.AwaitingConfirmation);

            //set birthCertificate
            string birthCertificationPhotoName = await _fileService.SaveFileAndGenerateName(
                request.birthCertificatePhoto, Directories.UserBirthCertificatePhotoPath);

            user.SetBirthCertificatePhoto(birthCertificationPhotoName);

            //Set NationalCard
            string nationalCardPhotoName = await _fileService.SaveFileAndGenerateName(
                request.nationalCardPhoto, Directories.UserNationalCardPhotoPath);
            
            user.SetNationalCardPhoto(nationalCardPhotoName);

            //Set NationalityCode
            user.SetNationalityCode(request.nationalityCode);

            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }

}
