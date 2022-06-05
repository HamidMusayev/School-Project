using Microsoft.EntityFrameworkCore;
using SchoolProject.Models.Classes;
using SchoolProject.Models.Contexts;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class ExamRepository : IExamService
    {
        private readonly SchoolContext _context;

        public ExamRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Exam exam)
        {
            _context.Add(exam);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Exam exam)
        {
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Exam>> GetAllAsync()
        {
            return await _context.Exams
                .Include(e => e.Lesson)
                .Include(e => e.Lesson!.Class)
                .Include(e => e.Lesson!.Teacher).ToListAsync();
        }

        public async Task<Exam?> GetByIdAsync(int id)
        {
            return await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(Exam exam)
        {
            _context.Exams.Update(exam);

            await _context.SaveChangesAsync();
        }
    }
}
