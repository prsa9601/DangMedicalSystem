using Application.Auth.Shared.Abstractions.Interfaces;
using Domain.UserAgg.Interfaces.Repository;
using System;

namespace Infrastructure.Services
{
    public class OtpService : IOtpService
    {
        private readonly Random _random;
        private readonly IUserRepository _repository;

        public OtpService(Random random, IUserRepository repository)
        {
            _random = random;
            _repository = repository;
        }

        public async Task<bool> CheckDateTimeForLogin(string phoneNumber)
        {
            var user = await _repository.GetUserByFilter(i => i.PhoneNumber.Equals(phoneNumber));
            if (user == null)
                return false;
            var otp = user.UserOtps.FirstOrDefault(i => i.UserId.Equals(user.Id) 
                && i.ExpireDate.AddMinutes(10) >= DateTime.Now);
            return true;
        }

        public async Task<string> GenerateToken()
        {
            return await Task.Run(() => _random.Next(100000, 999999).ToString());
        }
    }
}
