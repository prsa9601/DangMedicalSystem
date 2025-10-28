using Common.Application;
using Common.Application.SecurityUtil;
using Domain.UserAgg.Interfaces.Builder;
using Domain.UserAgg.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.Create
{
    public class CreateUserForAdminCommand : IBaseCommand<Guid>
    {
        public required string phoneNumber { get; set; }
        public required string firstName { get; set; }
        public required string lastName { get; set; }
        public required string password { get; set; }
    }
    internal class CreateUserForAdminCommandHandler : IBaseCommandHandler<CreateUserForAdminCommand, Guid>
    {
        private readonly IUserRepository _repository;
        private readonly IUserBuilder _builder;

        public CreateUserForAdminCommandHandler(IUserBuilder builder, IUserRepository repository)
        {
            _builder = builder;
            _repository = repository;
        }

        public async Task<OperationResult<Guid>> Handle(CreateUserForAdminCommand request, CancellationToken cancellationToken)
        {
            string password = Sha256Hasher.Hash(request.password);
            var user = _builder.WithPhoneNumber(request.phoneNumber).WithFirstName(request.firstName).WithLastName(request.lastName)
                .WithPassword(password).Build();

            await _repository.AddAsync(user);
            await _repository.SaveChangeAsync();

            return OperationResult<Guid>.Success(user.Id);
        }
    }
}
