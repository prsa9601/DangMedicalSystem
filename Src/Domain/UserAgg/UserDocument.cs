using Common.Domain;
using Domain.UserAgg.Enum;

namespace Domain.UserAgg
{
    public class UserDocument : BaseEntity
    {
        public UserDocument(UserDocumentStatus status, string nationalityCode, string birthCertificatePhoto, string nationalCardPhoto)
        {
            Status = status;
            NationalityCode = nationalityCode;
            BirthCertificatePhoto = birthCertificatePhoto;
            NationalCardPhoto = nationalCardPhoto;
        }

        public void Confirmed()
        {
            Status = UserDocumentStatus.IsConfirmed;
        }
        public void Reject()
        {
            Status = UserDocumentStatus.WrongInformation;
        }

        public UserDocumentStatus Status { get; private set; } = UserDocumentStatus.NotConfirmed;
        public string NationalityCode { get; private set; }
        public string BirthCertificatePhoto { get; private set; }
        public string NationalCardPhoto { get; private set; }
        public Guid UserId { get; internal set; }

    }
}
