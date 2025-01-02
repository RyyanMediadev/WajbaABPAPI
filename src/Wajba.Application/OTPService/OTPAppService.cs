﻿global using Wajba.Dtos.OTPContract;
global using Wajba.Models.OTPDomain;
global using Volo.Abp.Domain.Entities;

namespace Wajba.OTPService
{
    [RemoteService(false)]
    public class OTPAppService : ApplicationService
    {
        private readonly IRepository<OTP, int> _repository;

        public OTPAppService(IRepository<OTP, int> repository)
        {
            _repository = repository;
        }
        public async Task<OTPDto> CreateAsync(CreateUpdateOTPDto input)
        {
            OTP otp = new OTP
            {
                DigitLimit = input.DigitLimit,
                ExpiryTimeInMinutes = input.ExpiryTimeInMinutes,
            };
            var insertedOTP = await _repository.InsertAsync(otp, true);
            return ObjectMapper.Map<OTP, OTPDto>(insertedOTP);
        }
        public async Task<OTPDto> UpdateAsync(UpdateOtpDto input)
        {



            var  otp = await _repository.FirstOrDefaultAsync();
            if (otp == null)
                throw new Exception("Not found");
            otp.DigitLimit = input.DigitLimit;
            otp.ExpiryTimeInMinutes = input.ExpiryTimeInMinutes;
            otp.LastModificationTime = DateTime.UtcNow;
            OTP updatedOTP = await _repository.UpdateAsync(otp, true);
            return ObjectMapper.Map<OTP, OTPDto>(updatedOTP);
        }
        public async Task<OTPDto> GetByIdAsync(int id)
        {
            OTP otp = await _repository.GetAsync(id);
            if (otp == null)
                throw new EntityNotFoundException(typeof(OTP), id);
            return ObjectMapper.Map<OTP, OTPDto>(otp);
        }
        public async Task<PagedResultDto<OTPDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
        {
            var otps = await _repository.GetListAsync();
            return new PagedResultDto<OTPDto>
            {
                TotalCount = otps.Count,
                Items = ObjectMapper.Map<List<OTP>, List<OTPDto>>(otps)
            };
        }
        public async Task DeleteAsync(int id)
        {
            OTP otp = await _repository.GetAsync(id);
            if (otp == null)
                throw new EntityNotFoundException(typeof(OTP), id);
            await _repository.DeleteAsync(otp);
        }
    }
}