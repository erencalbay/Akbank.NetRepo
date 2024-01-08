using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Genres
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
