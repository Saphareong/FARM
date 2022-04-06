using BusinessObj.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ScoreDAO
    {
        private static ScoreDAO instance = null;
        private static readonly object instanceLock = new object();
        private FAMContext db = new FAMContext();

        private double lowerScoreRange = 0;
        private double upperScoreRange = 10;

        public ScoreDAO() { }
        public static ScoreDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ScoreDAO();
                    }
                    return instance;
                }
            }
        }

        // get score by its ID used for check if exist
        public Score GetScoreByID(int scoreID)
        {
            Score score = db.Scores.FirstOrDefault(s => s.ScoreID == scoreID);

            return score;
        }

        // get all scores of a student
        public List<Score> GetScoresByStudentID(string studentID)
        {
            List<Score> scores = new List<Score>();

            scores = db.Scores.Where(s => s.AccountId == studentID).ToList();

            return scores;
        }

        // get all scores from a class
        public List<Score> GetScoresByClassID(string classID)
        {
            List<Score> scores = new List<Score>();

            scores = db.Scores.Where(s => s.ClassID == classID).Include(c => c.User).ToList();

            return scores;
        }

        // create default record for score when a student enrolls a new subject
        public Score CreateScore(Score score)
        {
            Score check = GetScoreByID(score.ScoreID);
            if (check != null)
            {
                db.Scores.Add(score);
                db.SaveChanges();
                return score;
            }
            else
            {
                throw new Exception("Score already exists!");
            }
        }

        public Score EditScore(Score score)
        {
            Score existScoreRecord = GetScoreByID(score.ScoreID);
            if (existScoreRecord != null)
            {
                if (!CheckValidScore(score))
                    throw new Exception("Score update is not valid!");

                existScoreRecord.Quiz1 = score.Quiz1;
                existScoreRecord.Quiz2 = score.Quiz2;
                existScoreRecord.Lab1 = score.Lab1;
                existScoreRecord.Lab2 = score.Lab2;
                existScoreRecord.Lab3 = score.Lab3;
                existScoreRecord.Assignment = score.Assignment;
                existScoreRecord.PE = score.PE;
                existScoreRecord.FE = score.FE;

                db.SaveChanges();
                return score;
            }
            else
            {
                throw new Exception("Score record not exist!");
            }
        }

        private bool CheckValidScore(Score score)
        {
            if (score == null) return false;

            if (score.Quiz1 < lowerScoreRange || score.Quiz1 > upperScoreRange) return false;
            if (score.Quiz2 < lowerScoreRange || score.Quiz2 > upperScoreRange) return false;
            if (score.Lab1 < lowerScoreRange || score.Lab1 > upperScoreRange) return false;
            if (score.Lab2 < lowerScoreRange || score.Lab2 > upperScoreRange) return false;
            if (score.Lab3 < lowerScoreRange || score.Lab3 > upperScoreRange) return false;
            if (score.Assignment < lowerScoreRange || score.Assignment > upperScoreRange) return false;
            if (score.FE < lowerScoreRange || score.FE > upperScoreRange) return false;
            if (score.Total < lowerScoreRange || score.Total > upperScoreRange) return false;

            return true;
        }
    }
}
