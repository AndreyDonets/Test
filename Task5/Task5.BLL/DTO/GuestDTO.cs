using System;
using System.Collections.Generic;

namespace Task5.BLL.DTO
{
    public class GuestDTO : BaseModelDTO
    {
        public GuestDTO() => Id = Guid.NewGuid();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Passport { get; set; }
    }
}
