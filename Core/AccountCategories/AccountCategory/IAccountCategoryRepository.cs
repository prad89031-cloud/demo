using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.AccountCategories.AccountCategory
{
    public interface IAccountCategoryRepository
    {
        Task<object> GetAll();
        Task<object> Insert(AccountCategoryModel category);
        Task<object> Update(AccountCategoryModel category);

        Task<object> Delete(int id);

    }
}
