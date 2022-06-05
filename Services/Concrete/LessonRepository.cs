using Microsoft.EntityFrameworkCore;
using SchoolProject.Models.Classes;
using SchoolProject.Models.Contexts;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class LessonRepository : ILessonService
    {
        private readonly SchoolContext _context;

        public LessonRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Lesson lesson)
        {
            _context.Add(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Lesson lesson)
        {
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Lesson>> GetAllAsync()
        {
            return await _context.Lessons.Where(l => l.Passive == false)
                .Include(l => l.Class)
                .Include(l => l.Teacher)
                .ToListAsync();
        }

        public async Task<Lesson?> GetByIdAsync(int id)
        {
            return await _context.Lessons
                .Where(l => l.Id == id)
                .Include(l => l.Class)
                .Include(l => l.Teacher)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Lesson lesson)
        {
            _context.Lessons.Update(lesson);

            await _context.SaveChangesAsync();
        }
    }
}
