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


        public List<(string, uint?, char?)> coursesAndGrades(string _cuuid)
        {
            var coursesandgrades = _db.CourseTable
            .Where(c => c.cuuid == _cuuid)
            .Select(c => new ValueTuple<string, uint?, char?>(c.CourseCode, c.CourseUnit, c.Grade))
            .ToList();

            return coursesandgrades;

        }
    }
}
