using System.Threading.Tasks;
using Task5.DAL.Entities;

namespace Task5.DAL.Interfaces
{
    public interface IGuestRepository : IRepository<Guest>
    {
        Task<Guest> GetByPassportAsync(string passport);

        Guest GetByPassport(string passport);
    }
}
