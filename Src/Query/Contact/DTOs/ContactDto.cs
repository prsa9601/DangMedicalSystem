using Common.Query;
using Common.Query.Filter;
using Domain.Contact;

namespace Query.Contact.DTOs
{
    public class ContactDto : BaseDto
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ContactStatus Status { get; set; }
    }
    public class ContactFilterParam : BaseFilterParam
    {
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public ContactStatus? Status { get; set; }
    }
    public class ContactFilterResult : BaseFilter<ContactDto, ContactFilterParam>
    {
    }
}
