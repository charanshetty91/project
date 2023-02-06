using System.Threading.Tasks;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public interface IPostCodeRepository
    {
        Task<PostCodeList> GetAllPostalCodeListById(string partialId);
        Task<PostCodeData> GetPostCodeById(string fullId);
        string GetArea(double latitude);
    }
}