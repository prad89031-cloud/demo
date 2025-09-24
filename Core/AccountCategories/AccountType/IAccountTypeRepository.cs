using System.Collections.Generic;
using System.Threading.Tasks;


namespace Core.AccountCategories.AccountType
{
    public interface IAccountTypeRepository
    {
        Task<object> GetAllAsync();
        Task<object> InsertAsync(object accountType);
        Task<object> UpdateAsync(object accountType);
        Task<object> DeleteAsync(int id);
    }
}
