using AppCore.Infrastructure.Extensions;
namespace AppCore.Core.Domain.Models
{
    public class QuestionItemModel
    {
        private string index = "A";
        public QuestionItemModel()
        {
            Items = new List<OptionItem>();
        }
        public List<OptionItem> Items { get; set; }
        
        public string Question { get; set; }

        public void AddNewOption(string question)
        {
            Items.Add(new OptionItem(index, question));
            index = index.GetNextIndex();
        }
    }
    public class OptionItem
    {
        public string Index { get; set; }
        public string Content {  get; set; }
        public OptionItem(string index, string content)
        {
            Index = index;
            Content = content;
        }
    }
}
