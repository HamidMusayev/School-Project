using Microsoft.EntityFrameworkCore;
using SchoolProject.Models.Contexts;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class HomeRepository : IHomeService
    {
        private readonly SchoolContext _context;

        public HomeRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<HashSet<int>> GetCounts()
        {
            return new()
            {
                await _context.Users.CountAsync(u => u.Type == Models.Enums.UserType.Tələbə && u.Passive == false),
                await _context.Users.CountAsync(u => u.Type == Models.Enums.UserType.Müəllim && u.Passive == false),
                await _context.Classes.CountAsync(u => u.Passive == false),
                await _context.Exams.CountAsync(u => u.IsCanceled == false),
            };
        }
    }
}
