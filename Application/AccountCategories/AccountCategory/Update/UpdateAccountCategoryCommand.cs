using Core.AccountCategories.AccountCategory;
using MediatR;

namespace Application.AccountCategories.AccountCategory.Update
{ 
    public class UpdateAccountCategoryCommand : IRequest<object>
{             
    public AccountCategoryModel Category { get; set; }
}
}
