using Kitaab.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kitaab.Models
{
    public class NewBookVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Cover Picture is required")]
        [Display(Name = "Cover Picture")]
        public string Cover { get; set; }
        [Display(Name = "Description")]
        public String Description { get; set; }
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Required(ErrorMessage ="Price is required")]
        [Display(Name = "Price")]
        public float Price { get; set; }
        public int AuthorId { get; set; }
    }
}
