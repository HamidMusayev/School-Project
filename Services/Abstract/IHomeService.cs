namespace SchoolProject.Services.Abstract
{
    public interface IHomeService
    {
        public Task<int[]> GetCounts();
    }
}
