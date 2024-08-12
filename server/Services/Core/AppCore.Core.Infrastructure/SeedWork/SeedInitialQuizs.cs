using AppCore.Core.Domain.Entities;

namespace AppCore.Core.Infrastructure.SeedWork
{
    public class SeedInitialQuizs
    {
        public static async Task SeedData(DataContext context,
            IUnitOfWork unitOfWork)
        {
            var _quizs = new List<Quiz>
                {
                    new Quiz
                    {
                        Id = new Guid("fa1b265b-e660-4cac-b2ba-ab89b340512e"),
                        Name = "Class A",
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow.AddDays(30),
                        LimitTimeInSecond = 600
                    },
                    new Quiz
                    {
                        Id = new Guid("11effe19-f57c-43cd-9dd7-e186696a6642"),
                        Name = "Class B",
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow.AddDays(30),
                        LimitTimeInSecond = 600
                    },
                    new Quiz
                    {
                        Id = new Guid("8fc48b1e-fa60-48a6-9d80-e7cd6d9fdf0a"),
                        Name = "Class C",
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow.AddDays(30),
                        LimitTimeInSecond = 600
                    },
                };
            if (!context.Quizs.Any())
            {
                await unitOfWork.quizRepository.AddRangeAsync(_quizs);
            }
            else
            {
                await UpdateData(context, unitOfWork, _quizs);
            }
        }
        private static async Task UpdateData(DataContext context,
            IUnitOfWork unitOfWork,
            List<Quiz> _quizs)
        {
            foreach (var item in _quizs)
            {
                if (item.LastUpdatedDate >= DateTime.UtcNow)
                {
                    item.LastUpdatedDate = null;
                    await unitOfWork.quizRepository.AddOrUpdateAsync(item, x => x.Id == item.Id);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
