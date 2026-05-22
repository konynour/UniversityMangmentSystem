using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public double Grade { get; set; }
        
        [ForeignKey("Department")]
        public int Dept_Id { get; set; }
        public Department Department { get; set; }

        public ICollection<CrsResult> CrsResults { get; set; }
    }
}
