using UserAuthentication.Models;

namespace UserAuthentication.Repositories
{
    public class QuestionRepository
    {
        public static List<Question> Questions = new()
        {
            new() { Id = 1, QuesName = "What is ICT?", QuesType = "structure", Ques_Time = 10},
            new() {Id = 2, QuesName = " Select the input devices.", QuesType = "MCQ", Ques_Time = 10},
            new() { Id = 3, QuesName = " What is Hard Disk", QuesType = "structure", Ques_Time = 10 }
        };
    }
}
