using Common.Query;
using Common.Query.Filter;
using Domain.Contract;

namespace Query.Contract.DTOs
{
    public class ContractDto : BaseDto
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ContractStatus Status { get; set; }
    }
    public class ContractFilterParam : BaseFilterParam
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public ContractStatus Status { get; set; }
    }
    public class ContractFilterResult : BaseFilter<ContractDto, ContractFilterParam>
    {
    }
}
