using System.Collections.Generic;
using SPT_API.Data;

namespace SPT_API.Services.GpaCalcServices
{
    public class GpaCalcService : IGpaCalcService
    {
        private readonly SPT_APIDbContext _db;
        public GpaCalcService(SPT_APIDbContext db)
        {
            _db = db;
        }


        public void coursesAndGrades(string _cuuid)
        {
            Dictionary<char,double> gradePointValues = new Dictionary<char, double>
            {
                {'A',5.0d },
                {'B',4.0d }
            };
            var coursesAndGrades = _db.CourseTable.Where(c => c.cuuid == _cuuid)
                .GroupBy(c => c.Grade)
                .ToDictionary(g => g.Key,g => g.Select(x => x.CourseUnit).ToList() );

        }
    }
}
