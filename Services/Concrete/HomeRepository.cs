using Microsoft.EntityFrameworkCore;
using SchoolProject.Models.Contexts;
using SchoolProject.Models.Enums;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete;

public class HomeRepository : IHomeService
{
    private readonly SchoolContext _context;

    public HomeRepository(SchoolContext context)
    {
        _context = context;
    }

    public async Task<int[]> GetCounts()
    {
        var counts = new int[4];

        counts[0] = await _context.Users.CountAsync(u =>
            u.Type == UserType.Tələbə && u.Passive == false);
        counts[1] = await _context.Users.CountAsync(u =>
            u.Type == UserType.Müəllim && u.Passive == false);
        counts[2] = await _context.Classes.CountAsync(u => u.Passive == false);
        counts[3] = await _context.Exams.CountAsync(u => u.IsCanceled == false);

        return counts;
    }
}