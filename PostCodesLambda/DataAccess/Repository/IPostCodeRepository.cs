using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public interface IPostCodeRepository
    {
        Task<PostCodeList> GetAllPostalCodeListById(string partialId);
        Task<PostCodeData> GetPostCodeById(string Id);
        string GetArea(double latitude);
    }
}
