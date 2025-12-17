using Common.Application;
using Domain.Contact.Repository;

namespace Application.Contact.Create
{
    public class CreateContactCommand : IBaseCommand
    {

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Domain.Contact.ContactStatus Status { get; set; }
    }
    internal sealed class CreateContactCommandHandler : IBaseCommandHandler<CreateContactCommand>
    {
        private readonly IContactRepository _repository;

        public CreateContactCommandHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contract = new Domain.Contact.ContactAgg(request.PhoneNumber, request.Email, request.FullName, request.Title, request.Description);
            await _repository.AddAsync(contract);
            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }
}
