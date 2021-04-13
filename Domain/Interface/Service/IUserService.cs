using Domain.Dto;
using System.Threading.Tasks;

namespace Domain.Interface.Service
{
    public interface IUserService
    {
        Task<UserDto> Save(UserDto userDto);
        Task<UserDto> GetByLogin(string login);
    }
}
