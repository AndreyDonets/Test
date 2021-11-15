using System.Threading.Tasks;
using Task5.DAL.Entities;

namespace Task5.WebApi.Interfaces
{
    public interface IJwtGenerator
    {
        Task<string> CreateToken(User user);
    }
}