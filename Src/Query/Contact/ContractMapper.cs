using Query.Contact.DTOs;

namespace Query.Contact
{
    public static class ContractMapper
    {
        public static ContactDto? Map(this Domain.Contact.ContactAgg contract)
        {
            return new ContactDto
            {
                Id = contract.Id,
                CreationDate = contract.CreationDate,
                Description = contract.Description,
                Email = contract.Email,
                FullName = contract.FullName,
                PhoneNumber = contract.PhoneNumber,
                Status = contract.Status,
                Title = contract.Title,
            };
        }
    }
}
