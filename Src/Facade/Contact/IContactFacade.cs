using Application.Contact.Answered;
using Application.Contact.Create;
using Common.Application;
using MediatR;
using Query.Contact.DTOs;
using Query.Contact.GetFilter;

namespace Facade.Contact
{
    public interface IContactFacade
    {
        Task<OperationResult> CreateContact(CreateContactCommand command);
        Task<OperationResult> AnsweredContact(ContactAnsweredCommand command);
        Task<ContactFilterResult> GetFilter(ContactFilterParam filterParam);
    }
    internal class ContactFacade : IContactFacade
    {
        private readonly IMediator _mediator;

        public ContactFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> AnsweredContact(ContactAnsweredCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> CreateContact(CreateContactCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<ContactFilterResult> GetFilter(ContactFilterParam filterParam)
        {
            return await _mediator.Send(new GetContactByFilter(filterParam));
        }
    }
}
