namespace SchoolProject.Services.Abstract
{
    public interface IHomeService
    {
        public Task<HashSet<int>> GetCounts();
    }
}
