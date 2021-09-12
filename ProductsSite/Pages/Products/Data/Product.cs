    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace ProductsSite
    {
        public class Product
        {
            public int Id { get; set; }
            public string? ProductType { get; set; }
            public string ProductName { get; set; }
            
            public DateTime? ProductDate { get; set; }
            //Переписал тип цены с decimal на string
            public int? Price { get; set; }
            
            [NotMapped]
            public string? PriceInput { get; set; }
        }
    }
