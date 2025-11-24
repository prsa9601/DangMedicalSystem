namespace DangMedicalSystem.Api.Models.User
{
    public class CompletionOfInformationCommandViewModel
    {
        public required string nationalityCode { get; set; }
        public required IFormFile nationalCardPhoto { get; set; }
        public required IFormFile birthCertificatePhoto { get; set; }
    }
    public class AddBankAccountCommandViewModel
    {
        public required string CardNumber { get; set; }
        public required string ShabaNumber { get; set; }
        public required string FullName { get; set; }
    }
}
