using Domain.ProfitAgg;

namespace DangMedicalSystem.Api.Models.Profit
{
    public class CreateProfitViewModel
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public ProfitStatus Status { get; set; }
        public IFormFile Image { get; set; }
    }
}
