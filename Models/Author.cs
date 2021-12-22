using Kitaab.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kitaab.Models
{
    public class Author : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength (50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 ad 50 chars")]
        public string FullName { get; set; }
        [Display(Name ="Books")]
        public ICollection<Book> Books { get; set; }
    }
}
