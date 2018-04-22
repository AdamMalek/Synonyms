using System.ComponentModel.DataAnnotations;

namespace Synonyms.Models.ViewModels
{
    public class AddSynonymVM
    {
        [Required]
        public string Term { get; set; }
        [Required]
        public string Synonyms { get; set; }
    }
}
