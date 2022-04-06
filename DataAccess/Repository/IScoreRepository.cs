using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IScoreRepository
    {
        public List<Score> GetScoresOfStudent(string studentID);
        public List<Score> GetScoresInClass(string classID);
        public Score Create(Score score);
        public Score Edit(Score score);
    }
}
