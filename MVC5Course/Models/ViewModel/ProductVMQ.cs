using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models.ViewModel
{
    /// <summary>
    /// 精簡版 Product
    /// </summary>
    public class ProductVMQ:IValidatableObject
    {
        [Required]
        [MinLength(5)]
        public string q { get; set; }

        [Required]
        public int Stock_S { get; set; }

        [Required]
        public int Stock_E { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           if(this.Stock_E < this.Stock_S )
            {
                yield return new ValidationResult(" 庫存資料篩選條件錯誤 ", new string[] { "Stock_S", "Stock_E" });
            }
            //throw new NotImplementedException();
        }
    }
}