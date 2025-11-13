using Application.Auth.Shared.Abstractions.Interfaces;
using Application.Auth.Shared.Utilities;
using Common.Application;
using Common.Application.SecurityUtil;
using Domain.UserAgg.Events;
using Domain.UserAgg.Interfaces.Builder;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Auth.Commands.GenerateAndSendOtpCode
{
    public class GenerateAndSendOtpCodeCommand : IBaseCommand<string>
    {
        public string phoneNumber { get; set; }

        public GenerateAndSendOtpCodeCommand(string phoneNumber)
        {
            this.phoneNumber = phoneNumber.EnsureLeadingZero();
        }
    }

    public class GenerateAndSendOtpCodeCommandHandler : IBaseCommandHandler<GenerateAndSendOtpCodeCommand, string>
    {
        private readonly IUserRepository _repository;
        private readonly IUserBuilder _builder;
        private readonly IOtpService _otpService;

        public GenerateAndSendOtpCodeCommandHandler(IUserRepository repository, IOtpService otpService, IUserBuilder builder)
        {
            _repository = repository;
            _otpService = otpService;
            _builder = builder;
        }

        public async Task<OperationResult<string>> Handle(GenerateAndSendOtpCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByFilterAsync(i => i.PhoneNumber.Equals(request.phoneNumber));
            if (user != null)
            {
                string token = await _otpService.GenerateToken();
                user.SetUserOtp(token);

                var otpEvent = new AddOtpCodeEvent
                {
                    Id = Guid.NewGuid(),
                    Session = "AddOtpCode"
                };
                user.AddDomainEvent(otpEvent);

                await _repository.SaveChangeAsync();

                return OperationResult<string>.Success(token);
            }
            else
            {
                string password = Sha256Hasher.Hash("DefaultGuestPassword");
                var userEntity = _builder.WithPhoneNumber(request.phoneNumber).WithPassword(password).Build();
                
                
                await _repository.AddAsync(userEntity);

                string token = await _otpService.GenerateToken();
                userEntity.SetUserOtp(token);

                await _repository.SaveChangeAsync();

                return OperationResult<string>.Success(token);
            }
        }
    }
}
