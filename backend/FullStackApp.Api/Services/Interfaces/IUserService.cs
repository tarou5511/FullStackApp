using System;
using FullStackApp.Api.Models.Entities;

namespace FullStackApp.Api.Services.Interfaces;

public interface IUserService
{
    Task<User> RegisterAsync(string username, string email, string password);
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<bool> DeleteUserAsync(int id);
}
