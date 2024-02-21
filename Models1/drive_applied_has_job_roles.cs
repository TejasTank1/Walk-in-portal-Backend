using System.ComponentModel.DataAnnotations;

namespace Backend.Models1
{
    public class drive_applied_has_job_roles
    {
        [Key]
        public int Id { get; set; }

        public int Drive_id { get; set; }

        public int Slots_id { get; set; }

        public int Role_id { get; set; }
    }
}
