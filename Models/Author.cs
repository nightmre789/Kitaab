using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kitaab.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
