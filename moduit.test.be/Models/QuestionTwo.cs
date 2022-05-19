namespace moduit.test.be.Models
{
    public class QuestionTwo
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
