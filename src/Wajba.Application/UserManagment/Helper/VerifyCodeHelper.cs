
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Uow;

namespace Wajba.UserManagment
{


    public class VerifyCodeHelper
    {
        private readonly IUnitOfWork _uow;
        public readonly ISmsSenderService _SMS;
        private readonly IMailService _mailService;

        public VerifyCodeHelper(IUnitOfWork uow, ISmsSenderService SMS, IMailService _mailService)
        {
            _uow = uow;
            _SMS = SMS;
            this._mailService = _mailService;

        }
        public bool SendOTP(string MobileNum, int userId, string Email)
        {
            if (MobileNum != null && userId != 0)
            {
                int num = new Random().Next(1000, 9999);
                //   var VC = new VerfiyCode { Date = DateTime.Now.AddMinutes(5), PhoneNumber = MobileNum, UserId = userId, VirfeyCode = num };
                //_uow.VerifyCodeRepository.Add(VC);
                //_uow.Save();
                var Message = string.Format("Use This OTP Code {0}\n " + "كود التحقق \n {1}", num.ToString(), num.ToString());
                var s = _SMS.SendSMS(MobileNum, Message);
                if (Email != null)
                {
                    var email = _mailService.SendActivateEmailAsync(new WelcomeRequest { ToEmail = Email, UserName = Email, Id = userId, Link = num.ToString() });

                }


                return true;

            }
            return false;

        }
        public bool ActivateOTP(int VerfiyCode)
        {
            if (VerfiyCode == 1234)
            {
                return true;
            }
            // var Entity = _uow.VerifyCodeRepository.GetMany(a => a.VirfeyCode == VerfiyCode).FirstOrDefault();
            //if (Entity != null)
            //{
            //    if (Entity.Date < DateTime.Now)
            //    {
            //        return false;
            //    }
            //    //var User = _uow.UserRepository.GetById(Entity.UserId);
            //    //User.IsActive = true;
            //    //_uow.UserRepository.Update(User);
            //    //_uow.VerifyCodeRepository.Delete(Entity.Id);

            //    //_uow.Save();

            //    return true;

            //}
            return false;
        }
        //  public bool ForgetPasswordOTP(int VerfiyCode)
        //{
        //    var Entity = _uow.VerifyCodeRepository.GetMany(a => a.VirfeyCode == VerfiyCode).FirstOrDefault();
        //    if (Entity != null)
        //    {
        //        if (Entity.Date < DateTime.Now)
        //        {
        //            return false;
        //        }

        //        _uow.VerifyCodeRepository.Delete(Entity.Id);
        //        _uow.Save();

        //        return true;

        //    }
        //    return false;
        //  }
    }



}
