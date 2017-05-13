namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ValidationAttribute;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject //加上: IValidatableObject 去實作
    {
        public int 訂單數量
        {
            get
            {
                return this.OrderLine.Count;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Price > 100 && this.Stock > 5)
            {
                yield return new ValidationResult(" 價格與庫存數量不合理 ",
                                 new string[] { "Price", "Stock" });
            }

            if (this.OrderLine.Count > 5 && this.Stock == 0)
            {
                yield return new ValidationResult("Stock 與訂單數量不匹配 ",
                                 new string[] { "Stock" });
            }

            yield break;
        }
    }


    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        [DisplayName("商品名稱")]
        [Required]
        [商品名稱必須包含Will字串(ErrorMessage = " 商品名稱必須包含 Will 字串 ")]
        public string ProductName { get; set; }
        [DisplayName("商品價格")]
        [DisplayFormat(DataFormatString = "{0:0}")]
        public Nullable<decimal> Price { get; set; }

        [DisplayName("是否上架")]
        public Nullable<bool> Active { get; set; }
        [Required]
        [DisplayName("商品庫存")]

        public Nullable<decimal> Stock { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
