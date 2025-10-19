using Application.Auth.Shared.Abstractions.Interfaces;
using Common.Application;
using Domain.UserAgg;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Auth.Commands.GenerateAndSendOtpCode
{
    public class GenerateAndSendOtpCodeCommand : IBaseCommand<string>
    {
        public string phoneNumber { get; set; }
    }
    
    public class GenerateAndSendOtpCodeCommandHandler : IBaseCommandHandler<GenerateAndSendOtpCodeCommand, string>
    {
        private readonly IUserRepository _repository;
        private readonly IOtpService _otpService;

        public GenerateAndSendOtpCodeCommandHandler(IUserRepository repository, IOtpService otpService)
        {
            _repository = repository;
            _otpService = otpService;
        }

        public async Task<OperationResult<string>> Handle(GenerateAndSendOtpCodeCommand request, CancellationToken cancellationToken)
        {
            var user = new User();
            user.SetPhoneNumber(request.phoneNumber);
            await _repository.AddAsync(user);

            string token = await _otpService.GenerateToken();
            user.SetUserOtp(token);
            await _repository.SaveChangeAsync();

            return OperationResult<string>.Success(token);
        }
    }
}
