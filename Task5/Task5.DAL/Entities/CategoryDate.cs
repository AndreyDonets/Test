using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task5.DAL.Entities
{
    public class CategoryDate : BaseEntity
    {
        public CategoryDate() => Id = Guid.NewGuid();

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}