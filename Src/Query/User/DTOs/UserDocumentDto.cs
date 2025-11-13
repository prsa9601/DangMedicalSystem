using Common.Query;
using Domain.UserAgg.Enum;

namespace Query.User.DTOs
{
    public class UserDocumentDto : BaseDto
    {
        public UserDocumentStatus Status { get; set; } = UserDocumentStatus.NotConfirmed;
        public string NationalityCode { get; set; }
        public string BirthCertificatePhoto { get; set; }
        public string NationalCardPhoto { get; set; }
        public Guid UserId { get; set; }
    }
}
