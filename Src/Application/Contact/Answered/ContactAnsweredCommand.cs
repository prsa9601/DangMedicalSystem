using Common.Application;
using Domain.Contact.Repository;

namespace Application.Contact.Answered
{
    public class ContactAnsweredCommand : IBaseCommand
    {
        public Guid id { get; set; }
    }
    internal sealed class ContactAnsweredCommandHandler : IBaseCommandHandler<ContactAnsweredCommand>
    {
        private readonly IContactRepository _repository;

        public ContactAnsweredCommandHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ContactAnsweredCommand request, CancellationToken cancellationToken)
        {
            var contract = await _repository.GetTracking(request.id);
            if (contract == null) return OperationResult.NotFound();

            contract.IsAnswered();
            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }
}
