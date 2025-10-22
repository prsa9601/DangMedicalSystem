using Common.Application;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Sessions.Commands.AddSession
{
    public class AddSessionsCommand : IBaseCommand
    {
    }
    internal sealed class AddSessionsCommandHandler : IBaseCommandHandler<AddSessionsCommand>
    {
        private readonly IUserRepository _repository;

        public AddSessionsCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(AddSessionsCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByFilterAsync(i => i.PhoneNumber.Equals("00000000000"));
            user.SetUserSession($"fewa{DateTime.Now}", $"few{DateTime.Now}", DateTime.Now);
             _repository.SaveChange();
            return OperationResult.Success();
        }
    }
}
