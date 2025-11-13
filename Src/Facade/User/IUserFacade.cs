using Application.Auth.Shared.Utilities;
using Application.User.Commands.ChangeActivityAccount;
using Application.User.Commands.ChangePassword;
using Application.User.Commands.CompletionOfInformation;
using Application.User.Commands.ConfirmedAccount;
using Application.User.Commands.Create;
using Application.User.Commands.Edit;
using Application.User.Commands.Remove;
using Application.User.Commands.SetImage;
using Common.Application;
using MediatR;
using Query.User.CheckOtpCodeForPhoneNumber;
using Query.User.DTOs;
using Query.User.DTOs.FilterDto;
using Query.User.DTOs.FilterDto.FilterExpression;
using Query.User.GetById;
using Query.User.GetFilter;
using Query.User.GetFilterForDocuments;
using Query.User.GetListByExpression;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Facade.User
{
    public interface IUserFacade
    {
        Task<OperationResult> CreateUser(CreateUserCommand command);
        Task<OperationResult<Guid>> CreateUserForAdmin(CreateUserForAdminCommand command);
        Task<OperationResult> EditUser(EditUserCommand command);
        Task<OperationResult<string>> RemoveUser(RemoveUserCommand command);
        Task<OperationResult> SetImageUser(SetImageUserCommand command);
        Task<OperationResult> CompletionOfInformation(CompletionOfInformationCommand command);
        Task<OperationResult> ConfirmedAccount(ConfirmedAccountUserCommand command);
        Task<OperationResult> ChangePassword(ChangePasswordCommand command);
        Task<OperationResult> ChangeActivityAccount(ChangeActivityUserAccountCommand command);


        Task<UserDto?> GetById(Guid userId);
        Task<UserDto?> CheckOtpCodeForPhoneNumber(string phoneNumber, string ipAddress);
        Task<UserFilterResult> GetByFilter(UserFilterParam param);
        Task<UserFilterForDocumentsResult> GetByFilterForDocuments(UserFilterForDocumentsParam param);
        Task<UserExpressionFilterResult> GetByExpressionFilter(UserExpressionFilterParam param);
    }
    internal class UserFacade : IUserFacade
    {
        private readonly IMediator _mediator;

        public UserFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> ChangeActivityAccount(ChangeActivityUserAccountCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> ChangePassword(ChangePasswordCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<UserDto?> CheckOtpCodeForPhoneNumber(string phoneNumber, string ipAddress)
        {
            return await _mediator.Send(new CheckOtpCodeForPhoneNumberQuery(phoneNumber.EnsureLeadingZero(), ipAddress));
        }

        public async Task<OperationResult> CompletionOfInformation(CompletionOfInformationCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> ConfirmedAccount(ConfirmedAccountUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> CreateUser(CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult<Guid>> CreateUserForAdmin(CreateUserForAdminCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditUser(EditUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<UserExpressionFilterResult> GetByExpressionFilter(UserExpressionFilterParam param)
        {
            return await _mediator.Send(new GetListByExpressionQuery(param));
        }

        public async Task<UserFilterResult> GetByFilter(UserFilterParam param)
        {
            return await _mediator.Send(new GetUsersByFilterQuery(param));
        }

        public async Task<UserFilterForDocumentsResult> GetByFilterForDocuments(UserFilterForDocumentsParam param)
        {
            return await _mediator.Send(new GetUsersByFilterForDocumentsQuery(param));
        }

        public async Task<UserDto?> GetById(Guid userId)
        {
            return await _mediator.Send(new GetUserByIdQuery(userId));
        }

        public async Task<OperationResult<string>> RemoveUser(RemoveUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SetImageUser(SetImageUserCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
