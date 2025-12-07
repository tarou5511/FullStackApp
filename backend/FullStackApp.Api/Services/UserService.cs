using System;
using FullStackApp.Api.Models.Entities;
using FullStackApp.Api.Repositories.Interfaces;
using FullStackApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FullStackApp.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> RegisterAsync(string username, string email, string password)
    {
        var ecistingUser = _userRepository.GetByEmailAsync(email);
        if (ecistingUser != null)
        {
            throw new Exception("Email is already registered.");
        }

        var user = new User
        {
            Username = username,
            Email = email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, password);

        await _userRepository.CreateAsync(user);
        return user;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users =  await _userRepository.GetAllAsync();
        return (IEnumerable<User>)users;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return false;
        }
        await _userRepository.DeleteAsync(id);
        return true;
    }

    
}
