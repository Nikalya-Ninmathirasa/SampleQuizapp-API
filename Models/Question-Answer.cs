namespace UserAuthentication.Models
{
    public class Question_Answer
    {
        public string QuesID { get; set;}
        public string AnName { get; set;}

        public string AnsType { get; set;}

        public TimeOnly AnsTime { get; set;}

        public string isRight { get; set;}
    }
}
