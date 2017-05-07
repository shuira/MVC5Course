using System;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models.ViewModel
{
    /// <summary>
    /// 精簡版 Product
    /// </summary>
    public class ProductVM
    {
        [Required]
        [MinLength(5)]
        public string ProductName { get; set; }

        [Required]
        public Nullable<decimal> Price { get; set; }

        [Required]
        public Nullable<decimal> Stock { get; set; }
    }
}