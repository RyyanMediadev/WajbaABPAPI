using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wajba.Dtos.UserAddressContract;
using Wajba.Models.AddressDomain;

namespace Wajba.UserAddressService
{
    public class UserAddressAppService : IUserAddressAppService
    {
        private readonly IRepository<UserAddress, int> _repository;

        public UserAddressAppService(IRepository<UserAddress, int> repository)
        {
            _repository = repository;
        }

        public async Task<UserAddressDto> CreateAsync(CreateUserAddressDto input)
        {
            // Manually map CreateUserAddressDto to UserAddress
            var userAddress = new UserAddress
            {
                Title = input.Title,
                Longitude = input.Longitude,
                Latitude = input.Latitude,
                CustomerId = input.CustomerId,
                BuildingName = input.BuildingName,
                Street = input.Street,
                ApartmentNumber = input.ApartmentNumber,
                Floor = input.Floor,
                AddressLabel = input.AddressLabel,
                AddressType = input.AddressType
            };

            await _repository.InsertAsync(userAddress);

            // Manually map UserAddress to UserAddressDto
            var userAddressDto = new UserAddressDto
            {
                Id = userAddress.Id,
                Title = userAddress.Title,
                Longitude = userAddress.Longitude,
                Latitude = userAddress.Latitude,
                CustomerId = userAddress.CustomerId,
                BuildingName = userAddress.BuildingName,
                Street = userAddress.Street,
                ApartmentNumber = userAddress.ApartmentNumber,
                Floor = userAddress.Floor,
                AddressLabel = userAddress.AddressLabel,
                AddressType = userAddress.AddressType
            };

            return userAddressDto;
        }
        public async Task<UserAddressDto> UpdateAsync(UpdateUserAddressDto input)
        {
            var userAddress = await _repository.GetAsync(input.Id);

            // Manually map UpdateUserAddressDto to UserAddress (only the fields that need to be updated)
            userAddress.Title = input.Title;
            userAddress.Longitude = input.Longitude;
            userAddress.Latitude = input.Latitude;
            userAddress.CustomerId = input.CustomerId;
            userAddress.BuildingName = input.BuildingName;
            userAddress.Street = input.Street;
            userAddress.ApartmentNumber = input.ApartmentNumber;
            userAddress.Floor = input.Floor;
            userAddress.AddressLabel = input.AddressLabel;
            userAddress.AddressType = input.AddressType;

            await _repository.UpdateAsync(userAddress);

            // Manually map UserAddress to UserAddressDto
            var userAddressDto = new UserAddressDto
            {
                Id = userAddress.Id,
                Title = userAddress.Title,
                Longitude = userAddress.Longitude,
                Latitude = userAddress.Latitude,
                CustomerId = userAddress.CustomerId,
                BuildingName = userAddress.BuildingName,
                Street = userAddress.Street,
                ApartmentNumber = userAddress.ApartmentNumber,
                Floor = userAddress.Floor,
                AddressLabel = userAddress.AddressLabel,
                AddressType = userAddress.AddressType
            };

            return userAddressDto;
        }

        public async Task DeleteAsync(int id)
        {
            var userAddress = await _repository.GetAsync(id);
            await _repository.DeleteAsync(userAddress);
        }

        public async Task<List<UserAddressDto>> GetAllByCustomerAsync(string customerId)
        {
            var userAddresses = await _repository.GetListAsync(x => x.CustomerId == customerId);

            // Manually map list of UserAddress to list of UserAddressDto
            var userAddressDtos = userAddresses.Select(userAddress => new UserAddressDto
            {
                Id = userAddress.Id,
                Title = userAddress.Title,
                Longitude = userAddress.Longitude,
                Latitude = userAddress.Latitude,
                CustomerId = userAddress.CustomerId,
                BuildingName = userAddress.BuildingName,
                Street = userAddress.Street,
                ApartmentNumber = userAddress.ApartmentNumber,
                Floor = userAddress.Floor,
                AddressLabel = userAddress.AddressLabel,
                AddressType = userAddress.AddressType
            }).ToList();

            return userAddressDtos;
        }

        public async Task<UserAddressDto> GetByIdAsync(int id)
        {
            var userAddress = await _repository.GetAsync(id);

            // Manually map UserAddress to UserAddressDto
            var userAddressDto = new UserAddressDto
            {
                Id = userAddress.Id,
                Title = userAddress.Title,
                Longitude = userAddress.Longitude,
                Latitude = userAddress.Latitude,
                CustomerId = userAddress.CustomerId,
                BuildingName = userAddress.BuildingName,
                Street = userAddress.Street,
                ApartmentNumber = userAddress.ApartmentNumber,
                Floor = userAddress.Floor,
                AddressLabel = userAddress.AddressLabel,
                AddressType = userAddress.AddressType
            };

            return userAddressDto;
        }
    }
}
