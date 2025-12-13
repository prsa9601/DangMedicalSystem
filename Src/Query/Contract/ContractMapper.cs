using Query.Contract.DTOs;

namespace Query.Contract
{
    public static class ContractMapper
    {
        public static ContractDto? Map(this Domain.Contract.ContractAgg contract)
        {
            return new ContractDto
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
