using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.UserAddressContract
{
    public interface IUserAddressAppService
    {
        Task<UserAddressDto> CreateAsync(CreateUserAddressDto input);
        Task<UserAddressDto> UpdateAsync(UpdateUserAddressDto input);
        Task DeleteAsync(int id);
        Task<List<UserAddressDto>> GetAllByCustomerAsync(string customerId);
        Task<UserAddressDto> GetByIdAsync(int id);
    }
}
