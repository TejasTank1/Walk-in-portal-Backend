using System.ComponentModel.DataAnnotations;

namespace Backend.Models1
{
    public class user_reg_has_all_job
    {
        [Key]
        public int Id { get; set; }

        public int Role_id { get; set; }
    }
}
