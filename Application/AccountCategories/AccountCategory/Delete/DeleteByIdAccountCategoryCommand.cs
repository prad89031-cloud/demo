using Core.AccountCategories.AccountCategory;
using MediatR;

namespace Application.AccountCategories.AccountCategory.Delete
{
    public class DeleteAccountCategoryCommand : IRequest<object>
    {
        public AccountCategoryModel Category { get; set; }
    }
}
