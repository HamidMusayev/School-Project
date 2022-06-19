using Microsoft.EntityFrameworkCore;
using SchoolProject.Models.Classes;
using SchoolProject.Models.Contexts;
using SchoolProject.Models.Enums;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete;

public class StudentRepository : IStudentService
{
    private readonly SchoolContext _context;

    public StudentRepository(SchoolContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .Where(u => u.Type == UserType.Tələbə && u.Passive == false)
            .Include(u => u.Class)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Class)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Update(user);
        await _context.SaveChangesAsync();
    }
}