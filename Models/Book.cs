using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kitaab.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public String Description { get; set; }
        public string Category { get; set; }
        public Author Author { get; set; }
    }
}
