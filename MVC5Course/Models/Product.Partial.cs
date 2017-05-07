namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
        public int 訂單數量
        {
             get
             {
                return this.OrderLine.Count;
                            }
                    }
        }


    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        [DisplayName("商品名稱")]
        [Required]
        [MaxLength(5)]
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
