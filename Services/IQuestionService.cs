using UserAuthentication.Models;

namespace UserAuthentication.Services
{
    public interface IQuestionService
    {
        public  Task<List <Question>> Create(Question question);

        public Task<Question> Get(int id);

        public Task<List<Question>> ListAsync();

        public Task<Question> Update(Question question);

        public Task<bool> Delete(int id);

    }
}
