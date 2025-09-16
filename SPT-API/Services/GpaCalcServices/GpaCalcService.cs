using System.Collections.Generic;
using SPT_API.Data;
using SPT_API.Models;

namespace SPT_API.Services.GpaCalcServices
{
    public class GpaCalcService : IGpaCalcService
    {
        private readonly SPT_APIDbContext _db;
        public GpaCalcService(SPT_APIDbContext db)
        {
            _db = db;
        }

        private static readonly Dictionary<char, double> gradePointValues = new Dictionary<char, double>
            {
                {'A',5.0d },
                {'B',4.0d },
                {'C',3.0d },
                {'D',2.0d },
                {'E',1.0d },
                {'F',0d }
            };

        public double CalculateGPA(string _cuuid)
        {
            var gradesAndUnits = _db.CourseTable.Where(c => c.cuuid == _cuuid)
                .GroupBy(c => c.Grade)
                .ToDictionary(g => g.Key,g => g.Select(x => x.CourseUnit).ToList());

            double totalWeightedPoints = 0;
            int totalUnits = 0;
            foreach (var grade in gradePointValues.Keys)
            {
                if (gradesAndUnits.TryGetValue(grade,out var gradeCourseUnits))
                {
                    var gradeUnits = gradeCourseUnits.Sum(c=>c??0);
                    totalWeightedPoints += gradeUnits * gradePointValues[grade];
                    totalUnits += (int)gradeUnits;
                }
            }
            double gpa = totalUnits > 0 ? totalWeightedPoints / totalUnits : 0;
            var student = _db.StudentTable.FirstOrDefault(c => c.uniqueUserId == _cuuid);
            student.gpa = gpa.ToString();
            _db.SaveChanges();
            return gpa;
        }
    }
}
