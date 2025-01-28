

using Wajba.Dtos.UserAddressContract;
using Wajba.Models.AddressDomain;
using Wajba.Models.CompanyDomain;

namespace Wajba.UserAddressService;

public class WajbaUserAddressAppService : ApplicationService
{
        private readonly IRepository<WajbaUserAddress, int> _repository;

        public WajbaUserAddressAppService(IRepository<WajbaUserAddress, int> repository)
        {
            _repository = (IRepository<WajbaUserAddress, int>?)repository;
        }

        public async Task<CreateUserAddressDto> CreateAsync(CreateUserAddressDto input)
        {
            // Manually map CreateUserAddressDto to UserAddress
            var userAddress = new WajbaUserAddress
            {
                Title = input.Title,
                Longitude = input.Longitude,
                Latitude = input.Latitude,
                WajbaUserId = input.WajbaUserId,
                BuildingName = input.BuildingName,
                Street = input.Street,
                ApartmentNumber = input.ApartmentNumber,
                Floor = input.Floor,
                AddressLabel = input.AddressLabel,
                AddressType = (EmployeeAddressType)input.AddressType
            };

         
            
        WajbaUserAddress WajbaUserAddress1 = await _repository.InsertAsync(userAddress, true);
        return ObjectMapper.Map<WajbaUserAddress, CreateUserAddressDto>(WajbaUserAddress1);
        //return userAddressDto;
        }
        public async Task<UserAddressDto> UpdateAsync(UpdateUserAddressDto input)
        {
        WajbaUserAddress userAddress = await _repository.GetAsync(input.Id);


        if (userAddress == null)
            throw new Exception("Not found");
       
   


        // Manually map UpdateUserAddressDto to UserAddress (only the fields that need to be updated)
        userAddress.Title = input.Title;
            userAddress.Longitude = input.Longitude;
            userAddress.Latitude = input.Latitude;
            userAddress.WajbaUserId = input.wajbaUserId;
            userAddress.BuildingName = input.BuildingName;
            userAddress.Street = input.Street;
            userAddress.ApartmentNumber = input.ApartmentNumber;
            userAddress.Floor = input.Floor;
            userAddress.AddressLabel = input.AddressLabel;
            userAddress.AddressType = (EmployeeAddressType)input.AddressType;

          WajbaUserAddress wus =  await _repository.UpdateAsync(userAddress);

         
            
        return ObjectMapper.Map<WajbaUserAddress, UserAddressDto>(wus);

        }

        public async Task DeleteAsync(int id)
        {
            var userAddress = await _repository.GetAsync(id);
            await _repository.DeleteAsync(userAddress);
        }

        public async Task<List<UserAddressDto>> GetAllByWajbaUserAsync(int WajbaUserId)
        {
            var userAddresses = await _repository.GetListAsync(x => x.WajbaUserId == WajbaUserId);

            // Manually map list of UserAddress to list of UserAddressDto
            var userAddressDtos = userAddresses.Select(userAddress => new UserAddressDto
            {
                Id = userAddress.Id,
                Title = userAddress.Title,
                Longitude = userAddress.Longitude,
                Latitude = userAddress.Latitude,
                WajbaUserId = userAddress.WajbaUserId,
                BuildingName = userAddress.BuildingName,
                Street = userAddress.Street,
                ApartmentNumber = userAddress.ApartmentNumber,
                Floor = userAddress.Floor,
                AddressLabel = userAddress.AddressLabel,
                AddressType = (int)userAddress.AddressType
            }).ToList();

            return userAddressDtos;
        }

        public async Task<UserAddressDto> GetByIdAsync(int id)
        {
        WajbaUserAddress WajbaUserAddress = await _repository.GetAsync(id);
        if (WajbaUserAddress == null)
            throw new Exception("Not found");
        return ObjectMapper.Map<WajbaUserAddress, UserAddressDto>(WajbaUserAddress);
    }
    }

