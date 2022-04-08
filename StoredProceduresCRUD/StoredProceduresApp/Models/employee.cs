using System.ComponentModel.DataAnnotations;

namespace StoredProceduresApp.Models
{
    public class employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public int Salary { get; set; }
        public string? Gender { get; set; }
        public string? Response { get; set; }
    }
}
