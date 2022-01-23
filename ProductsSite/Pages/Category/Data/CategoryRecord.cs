    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Http;

    namespace ProductsSite
    {
        [Table("Categories")]
        public class CategoryRecord
        {
            [Key][Column(TypeName ="nvarchar(150)")] public string Id { get; set; } = "";
            [Column(TypeName = "nvarchar(200)")]public string CategoryName { get; set; } = "";
            [NotMapped]
            public List<ProductRecord>? Products { get; set; }
        }
    }
