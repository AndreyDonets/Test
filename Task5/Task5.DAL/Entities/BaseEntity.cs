using System;
using System.ComponentModel.DataAnnotations;

namespace Task5.DAL.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}