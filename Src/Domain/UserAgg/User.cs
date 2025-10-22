﻿using Common.Domain;
using Domain.UserAgg.Enum;
using System.ComponentModel.DataAnnotations;

namespace Domain.UserAgg
{
    public class User : AggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Password { get; private set; }
        public string NationalityCode { get; private set; }
        public string ImageName { get; private set; }
        public string NationalCardPhoto { get; private set; }
        public string BirthCertificatePhoto { get; private set; }
        public UserStatus Status { get; private set; } = UserStatus.NotConfirmed;
        public bool IsActive { get; private set; } = true; //کاربر اکتیوه و میتونه کار کنه

        public UserBankAccount? BankAccount { get; private set; }
        public UserRole? UserRole { get; private set; }

        public List<UserOtp> UserOtps { get; private set; }
        public List<UserBlock> UserBlocks { get; private set; }
        public List<UserAttempt> UserAttempts { get; private set; }
        public List<UserSession> UserSessions { get; private set; }



        public User()
        {
            UserOtps = new();
            UserAttempts = new();
            UserBlocks = new();
            UserSessions = new();
        }

        //public void ChangeConcurrencyStamp()
        //{
        //    ConcurrencyStamp = Guid.NewGuid();
        //}

        public void SetUserOtp(string otpCode)
        {
            var userOtp = new UserOtp(otpCode, DateTime.Now.AddMinutes(3));
            userOtp.UserId = Id;
            UserOtps.Add(userOtp);
        }

        public void SetUserRole(Guid roleId)
        {
            var userRole = new UserRole(roleId);
            userRole.UserId = Id;
            UserRole = UserRole;
        }

        public void SetUserBlock(DateTime blockToDate, string description)
        {
            var userBlock = new UserBlock(blockToDate, description);
            userBlock.UserId = Id;
            UserBlocks.Add(userBlock);
        }

        public void SetUserSession(string jwtRefreshToken, string ipAddress, DateTime expireDate)
        {
            var userSession = new UserSession(jwtRefreshToken, ipAddress, expireDate);
            userSession.UserId = Id;
            UserSessions.Add(userSession);
        }

        public void SetUserAttempt(DateTime attemptDate, bool isSuccessful, string ipAddress, string userAgent,
            string failureReason, DateTime expireDate, AttemptType attemptType)
        {
            var userAttempt = new UserAttempt(attemptDate, isSuccessful,
                ipAddress, userAgent, failureReason, expireDate, attemptType);

            userAttempt.UserId = Id;
            UserAttempts.Add(userAttempt);
        }
        public void RemoveUserAttempt(DateTime expireDate, AttemptType attemptType)
        {
            UserAttempts.RemoveAll(i => i.ExpireDate > DateTime.Now && i.AttemptType == attemptType);
        }

        #region SetBuilder

        public void SetFirstName(string firstName)
        {
            FirstName = firstName;
        }

        public void SetImageName(string imageName)
        {
            ImageName = imageName;
        }

        public void SetLastName(string lastName)
        {
            LastName = lastName;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void SetNationalityCode(string nationalityCode)
        {
            NationalCodeGuard(nationalityCode);
            NationalityCode = nationalityCode;
        }

        public void SetNationalCardPhoto(string nationalCardPhoto)
        {
            NationalCardPhoto = nationalCardPhoto;
        }

        public void SetBirthCertificatePhoto(string birthCertificatePhoto)
        {
            BirthCertificatePhoto = birthCertificatePhoto;
        }

        public void SetUserStatus(UserStatus userStatus)
        {
            Status = userStatus;
        }

        public void SetAsActive()
        {
            IsActive = true;
        }

        public void SetAsDeActive()
        {
            IsActive = false;
        }

        #endregion

        private void NationalCodeGuard(string nationalCode)
        {
            if (!IranianNationalIdChecker.IsValid(nationalCode))
                throw new Exception("کد ملی شما صحیح نمیباشد.");
        }
    }
}
