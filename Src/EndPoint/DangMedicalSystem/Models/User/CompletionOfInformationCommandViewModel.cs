namespace DangMedicalSystem.Api.Models.User
{
    public class CompletionOfInformationCommandViewModel
    {
        public required string nationalityCode { get; set; }
        public required IFormFile nationalCardPhoto { get; set; }
        public required IFormFile birthCertificatePhoto { get; set; }
    }
}
