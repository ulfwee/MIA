using AutoMapper;
using MyWebApi.Entities;
using MyWebApi.DTOs;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;
using SortTest.Test;

namespace MyWebApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task CreateAsync(User user) => await _userRepository.CreateAsync(user);
        public async Task DeleteAsync(string id) => await _userRepository.DeleteAsync(id);
        public async Task<List<User>> GetAsync() => await _userRepository.GetAsync();
        public async Task<User?> GetByEmailAsync(string email) => await _userRepository.GetByEmailAsync(email);
        public async Task<User> GetAsync(string id) => await _userRepository.GetAsync(id);
        public async Task UpdateAsync(User user) => await _userRepository.UpdateAsync(user);
    }
}