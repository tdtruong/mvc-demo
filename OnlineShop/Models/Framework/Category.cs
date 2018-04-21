namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {

        public int ID { get; set; }

        [StringLength(50, ErrorMessage = "Please input upto 50 characters")]
        [Required]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Please input upto 50 characters")]
        public string Alias { get; set; }

        public int? ParentID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? Order { get; set; }

        public bool? Status { get; set; }
    }
}
