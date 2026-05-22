using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class CrsResult
    {
        public int Id { get; set; }
        public double Degree { get; set; }

        [ForeignKey("Course")]
        public int Crs_Id { get; set; }
        public Course Course { get; set; }

        [ForeignKey("Trainee")]
        public int Trainee_Id { get; set; }
        public Trainee Trainee { get; set; }
    }
}
