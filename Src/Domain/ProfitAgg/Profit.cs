using Common.Domain;
using Domain.ProfitAgg.Repository;
using Domain.ProfitAgg.Service;
using System.Reflection.Metadata.Ecma335;

namespace Domain.ProfitAgg
{
    public class Profit : BaseEntity
    {
        public Profit(Guid userId, Guid productId, ProfitStatus status,
            decimal price, Guid orderId, IProfitService service, DateTime forWhatTime, int forWhatPeriod)
        {
            if (service.CanCreate(userId, productId, orderId))
                throw new Exception("امکان انجام این عملیات وجود ندارد.");
            UserId = userId;
            ProductId = productId;
            Status = status;
            AmountPaid = price;
            OrderId = orderId;
            ForWhatTime = forWhatTime;
            ForWhatPeriod = forWhatPeriod;
        }
        public void SetImage(string imageName)
        {
            ImageName = imageName;
        }
        private Profit()
        {
            
        }

        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime ForWhatTime { get; set; }
        public int ForWhatPeriod { get; set; }
        public ProfitStatus Status { get; set; }
        public decimal AmountPaid { get; set; }
        public string ImageName { get; set; }
    }
    public enum ProfitStatus
    {
        None = 0,
        UnSuccessful = 1,
        Success = 2,
    }
}
