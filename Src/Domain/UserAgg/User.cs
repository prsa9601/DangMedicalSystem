using Common.Domain;

namespace Domain.UserAgg
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public string NationalityCode { get; set; }
        public string Imagename { get; set; }
    }
}
