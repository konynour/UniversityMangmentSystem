using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public string Address { get; set; }

        [ForeignKey("Department")]
        public int Dept_Id { get; set; }
        public Department Department { get; set; }

        [ForeignKey("Course")]
        public int Crs_Id { get; set; }
        public Course Course { get; set; }
    }
}
