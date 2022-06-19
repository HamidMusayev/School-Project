using Microsoft.EntityFrameworkCore;
using SchoolProject.Models.Classes;
using SchoolProject.Models.Contexts;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete;

public class ClassRepository : IClassService
{
    private readonly SchoolContext _context;

    public ClassRepository(SchoolContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Class @class)
    {
        _context.Add(@class);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Class @class)
    {
        _context.Classes.Remove(@class);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Class>> GetAllAsync()
    {
        var classes = await _context.Classes.Where(c => c.Passive == false).ToListAsync();
        classes.ForEach(c => c.StudentCount = _context.Users.Count(u => u.ClassId == c.Id));
        return classes;
    }

    public async Task<Class?> GetByIdAsync(int id)
    {
        var @class = await _context.Classes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (@class != null) @class.StudentCount = await _context.Users.CountAsync(u => u.ClassId == @class.Id);
        return @class;
    }

    public async Task UpdateAsync(Class @class)
    {
        _context.Update(@class);
        await _context.SaveChangesAsync();
    }
}