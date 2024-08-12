using AppCore.Core.Domain.Entities;
using AppCore.Core.Domain.Models;
using System.Text.Json;

namespace AppCore.Core.Infrastructure.SeedWork
{
    public class SeedInitialQuestions
    {
        public static async Task SeedData(DataContext context,
            IUnitOfWork unitOfWork)
        {
            var _questions = new List<Question>
                {
                    new Question
                    {
                        Quiz = await unitOfWork.quizRepository.GetById(new Guid("fa1b265b-e660-4cac-b2ba-ab89b340512e")),
                        Content = CreateQuestion("Who are all ___ people?","this","those","them","that"),
                        Answer = "B"
                    },
                    new Question
                    {
                        Quiz = await unitOfWork.quizRepository.GetById(new Guid("11effe19-f57c-43cd-9dd7-e186696a6642")),
                        Content = CreateQuestion("Claude is ___.","Frenchman","a French","a Frenchman","French man"),
                        Answer = "B"
                    },
                    new Question
                    {
                        Quiz = await unitOfWork.quizRepository.GetById(new Guid("8fc48b1e-fa60-48a6-9d80-e7cd6d9fdf0a")),
                        Content = CreateQuestion("I ___ a car next year","buy","am buying","going to buy","bought"),
                        Answer = "B"
                    },

                    new Question
                    {
                        Quiz = await unitOfWork.quizRepository.GetById(new Guid("fa1b265b-e660-4cac-b2ba-ab89b340512e")),
                        Content = CreateQuestion("They are all ___ ready for the party?","getting","going","doing","putting"),
                        Answer = "A"
                    },
                    new Question
                    {
                        Quiz = await unitOfWork.quizRepository.GetById(new Guid("11effe19-f57c-43cd-9dd7-e186696a6642")),
                        Content = CreateQuestion("When do you go ___ bed?","to","to the","in","in the"),
                        Answer = "A"
                    },
                    new Question
                    {
                        Quiz = await unitOfWork.quizRepository.GetById(new Guid("8fc48b1e-fa60-48a6-9d80-e7cd6d9fdf0a")),
                        Content = CreateQuestion("London is famous for ___ red buses","it’s","its","it","it is"),
                        Answer = "B"
                    },

                    new Question
                    {
                        Quiz = await unitOfWork.quizRepository.GetById(new Guid("fa1b265b-e660-4cac-b2ba-ab89b340512e")),
                        Content = CreateQuestion("Is there ___ milk in the fridge?","a lot","many","much","some"),
                        Answer = "D"
                    },
                    new Question
                    {
                        Quiz = await unitOfWork.quizRepository.GetById(new Guid("11effe19-f57c-43cd-9dd7-e186696a6642")),
                        Content = CreateQuestion("There is a flower shop in front ___ my house.","of","to","off","in"),
                        Answer = "A"
                    },
                    new Question
                    {
                        Quiz = await unitOfWork.quizRepository.GetById(new Guid("8fc48b1e-fa60-48a6-9d80-e7cd6d9fdf0a")),
                        Content = CreateQuestion("Where are ___ children? – They go to school","the","you","a","an"),
                        Answer = "A"
                    },
                };
            if (!context.Questions.Any())
            {
                await unitOfWork.questionRepository.AddRangeAsync(_questions);
            }
            else
            {
                await UpdateData(context, unitOfWork, _questions);
            }
        }
        private static async Task UpdateData(DataContext context,
            IUnitOfWork unitOfWork,
            List<Question> _questions)
        {
            foreach (var item in _questions)
            {
                if (item.LastUpdatedDate >= DateTime.UtcNow)
                {
                    item.LastUpdatedDate = null;
                    await unitOfWork.questionRepository.AddOrUpdateAsync(item, x => x.Id == item.Id);
                }
            }
            await context.SaveChangesAsync();
        }
        public static string CreateQuestion(string Question, string Option1, string Option2, string Option3, string Option4)
        {
            QuestionItemModel QuestionItemModel = new QuestionItemModel();
            QuestionItemModel.Question = Question;
            QuestionItemModel.AddNewOption(Option1);
            QuestionItemModel.AddNewOption(Option2);
            QuestionItemModel.AddNewOption(Option3);
            QuestionItemModel.AddNewOption(Option4);
            return JsonSerializer.Serialize(QuestionItemModel);
        }
    }
}
