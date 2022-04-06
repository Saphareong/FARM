using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ScoreRepository : IScoreRepository
    {
        public List<Score> GetScoresOfStudent(string studentID) => ScoreDAO.Instance.GetScoresByStudentID(studentID);
        public List<Score> GetScoresInClass(string classID) => ScoreDAO.Instance.GetScoresByClassID(classID);
        public Score Create(Score score) => ScoreDAO.Instance.CreateScore(score);
        public Score Edit(Score score) => ScoreDAO.Instance.EditScore(score);
    }
}
