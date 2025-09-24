using Core.AccountCategories.AccountType;
using MediatR;

namespace Application.AccountCategories.Accounttype.Create
{
    public class CreateGLCommand : IRequest<object>
    {
        public AccountTypemodel AccountType { get; set; }
    }
}
