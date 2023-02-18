using System.ComponentModel.DataAnnotations;

namespace notesMvc.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        public int? ParentId { get; set; }
        public Note Parent { get; set; }

        public List<Note> Children { get; set; }
    }
}
