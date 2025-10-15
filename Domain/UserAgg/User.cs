using System.ComponentModel;

namespace Domain.UserAgg
{
    public class User
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalityCode { get; set; }
        public string Imagename { get; set; }
    }
}
