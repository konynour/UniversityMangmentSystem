using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public int Hours { get; set; }

        [ForeignKey("Department")]
        public int Dept_Id { get; set; }
        public Department Department { get; set; }

        public ICollection<Instructor> Instructors { get; set; }
        public ICollection<CrsResult> CrsResults { get; set; }
    }
}
