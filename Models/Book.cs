using Kitaab.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kitaab.Models
{
    public class Book:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Cover Picture")]
        public string Cover { get; set; }
        [Display(Name = "Description")]
        public String Description { get; set; }
        [Display(Name = "Category")]
        public string Category { get; set; }
        [Display(Name ="Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Price")]
        public float Price { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
