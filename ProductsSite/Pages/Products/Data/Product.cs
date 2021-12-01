    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Http;

    namespace ProductsSite
    {
        public class Product
        {
            [Key] public string Id { get; set; } = "";
            //Категория ИД
            public string? ProductType { get; set; }
            public string ProductName { get; set; } = "";
            
            public DateTime? ProductDate { get; set; }
            public int Price { get; set; }

            public string? PreviewName { get; set; }
// Наличие Товара
            [NotMapped] public bool OnPreview { get; set; }
            [NotMapped] public string? PriceInput { get; set; }
            
            [NotMapped] public string? ProductTypeNew { get; set; }
            
            [NotMapped] public string? ProductSearch { get; set; }
            
        }
    }
