﻿using SchoolProject.Models.Classes;

namespace SchoolProject.Services.Abstract;

public interface ILessonService
{
    public Task<List<Lesson>> GetAllAsync();
    public Task<Lesson?> GetByIdAsync(int id);
    public Task AddAsync(Lesson lesson);
    public Task UpdateAsync(Lesson lesson);
    public Task DeleteAsync(Lesson lesson);
}