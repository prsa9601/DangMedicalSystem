using AngleSharp.Css;
using Common.Application;
using Common.Application.SecurityUtil;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Auth.Commands.GetSession
{
    public class GetSessionCommand : IBaseCommand<bool>
    {
        public string RefreshToken { get; set; }
    }
    internal sealed class GetSessionCommandHandler : IBaseCommandHandler<GetSessionCommand, bool>
    {
        private readonly IUserRepository _repository;

        public GetSessionCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<bool>> Handle(GetSessionCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByFilterAsync(i => i.UserSessions.
            Any(x => x.JwtRefreshToken.Equals(Sha256Hasher.Hash(request.RefreshToken))));

            if (user == null) return OperationResult<bool>.Error(false);

            return OperationResult<bool>.Success(true);
        }
    }
}
