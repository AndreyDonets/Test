using System;

namespace Task5.BLL.DTO
{
    public class CategoryDateDTO : BaseModelDTO
    {
        public CategoryDateDTO() => Id = Guid.NewGuid();

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}