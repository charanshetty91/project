using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public interface IPostCodeRepository
    {
        Task<Dictionary<string,string>> GetAllPostalCodeListById(LookupPostcodeRouteParameter lookupPostcodeRouteParameter);
        Task<PostCode> GetPostCodeById(string fullId);
    }
}