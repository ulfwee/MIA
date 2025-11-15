using MyWebApi.Models;

namespace MyWebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(string id);
        Task CreateAsync(UserDto userDto);
        Task<bool> UpdateAsync(UserDto userDto);
        Task<bool> DeleteAsync(string id);
    }
}