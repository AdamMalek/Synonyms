using System.ComponentModel.DataAnnotations;

namespace Synonyms.Models
{
    public class AddSynonymVM
    {
        [Required]
        public string Term { get; set; }
        [Required]
        public string Synonyms { get; set; }
    }
}
