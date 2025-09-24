using Core.AccountCategories.AccountCategory;
using MediatR;

namespace Application.AccountCategories.AccountCategory.Create
{
    public class CreateAccountCategoryCommand : IRequest<object>
    {
        public AccountCategoryModel Category { get; set; }
    }
}