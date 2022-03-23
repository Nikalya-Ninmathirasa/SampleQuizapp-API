using UserAuthentication.Models;
using UserAuthentication.Repositories;

namespace UserAuthentication.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly DataContext _dataContext;
        public QuestionService(DataContext dataContext) {

            _dataContext = dataContext;
        }
        public async Task <List <Question>> Create(Question question)
        { 

            _dataContext.Questions.Add(question);
          await _dataContext.SaveChangesAsync();

            return await _dataContext.Questions.ToListAsync();
        }



        public async Task<Question> Get(int quesID)
    {
        

         var question = _dataContext.Questions.FirstOrDefault(q => q.Id == quesID);

        if (question is null) return null;

        return question;
    }

       public async Task<List<Question>> ListAsync()
    {
        return await _dataContext.Questions.ToListAsync();
        }

       public async Task <Question> Update(Question newQuestion )
    {
            var oldQuestion = await _dataContext.Questions.FindAsync (newQuestion.Id);
            if (oldQuestion is null) return null;

      
        oldQuestion.QuesName = newQuestion.QuesName;
        oldQuestion.QuesType = newQuestion.QuesType;
        oldQuestion.Ques_Time = newQuestion.Ques_Time;

         await _dataContext.SaveChangesAsync ();
        return newQuestion;
    }

        public async Task<bool> Delete(int quesID)
    {

            var oldQuestion = await _dataContext.Questions.FindAsync(quesID);
            if (oldQuestion is null) return false;
             _dataContext.Questions.Remove(oldQuestion);
           await  _dataContext.SaveChangesAsync();
        return true;
    }
  
      }
}


