using System;
using System.Collections;
using FullStackApp.Api.Data;
using FullStackApp.Api.Models.Entities;
using FullStackApp.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FullStackApp.Api.Repositories;

public class UserRepository :IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
