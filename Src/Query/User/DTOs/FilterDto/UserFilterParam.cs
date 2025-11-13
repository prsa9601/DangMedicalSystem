using Common.Query.Filter;
using Domain.UserAgg.Enum;

namespace Query.User.DTOs.FilterDto
{
    public class UserFilterParam : BaseFilterParam
    {
        public bool? IsActive { get; set; }
        //public Guid? ProductId { get; set; }
        public string? PhoneNumber { get; set; }
        //public List<Guid>? UserIds { get; set; }

    }
    public class UserFilterForDocumentsParam : BaseFilterParam
    {
        public UserDocumentStatus? UserStatus { get; set; }
        public bool? IsActive { get; set; }
        public string? UserName { get; set; }
        //public Guid? ProductId { get; set; }
        public string? PhoneNumber { get; set; }
        //public List<Guid>? UserIds { get; set; }

    }
}
