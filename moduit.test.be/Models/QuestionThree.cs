namespace moduit.test.be.Models
{
    public class QuestionThreeGet
    {
        public int id { get; set; }
        public int category { get; set; }
        public List<QuestionThreeGetItems> items { get; set; }
        public string createdAt { get; set; }
        public List<string> tags { get; set; }
    }
    public class QuestionThreeGetItems
    {
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
    }
    public class QuestionThreeResult
    {
        public int id { get; set; }
        public int category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
        public string createdAt { get; set; }
        public List<string> tags { get; set; }
    }
}
