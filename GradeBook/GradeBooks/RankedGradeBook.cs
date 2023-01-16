using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool weighted) : base(name, weighted)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException
                    ("Ranked-grading requires a minimum of 5 students to work");
            }
            else
            {
                //order the average scores from highest to lowest average
                var orderedAverage = Students.OrderByDescending(x => x.AverageGrade)
                    .Select(x => x.AverageGrade).ToList();

                //get the last average of student in the top X percentile
                int indexBase = (int)Math.Ceiling(0.2 * orderedAverage.Count);

                //compare the average grade of student to the last top X of comparison
                if (averageGrade >= orderedAverage[indexBase - 1])
                    return 'A';
                else if (averageGrade >= orderedAverage[(indexBase * 2)-1])
                    return 'B';
                else if (averageGrade >= orderedAverage[(indexBase * 3) - 1])
                    return 'C';
                else if (averageGrade >= orderedAverage[(indexBase * 4) - 1])
                    return 'D';
            }

            return 'F';
            
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students" +
                    "with grades in order to properly calculate a student's " +
                    "overall grade");
            }
            else
            {
                base.CalculateStatistics();
            }
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students" +
                    "with grades in order to properly calculate a student's " +
                    "overall grade");
            }
            else
            {
                base.CalculateStudentStatistics(name);
            }
        }
    }
}
