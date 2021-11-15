using System;
using System.Collections.Generic;

namespace Task5.BLL.DTO
{
    public class RoomDTO : BaseModelDTO
    {
        public RoomDTO() => Id = Guid.NewGuid();

        public int Number { get; set; }
        public Guid CategoryId { get; set; }

        public override bool Equals(object obj) => obj is RoomDTO ? this.Id == ((RoomDTO)obj).Id : false;
        public override int GetHashCode() => Id.GetHashCode();
    }
}
