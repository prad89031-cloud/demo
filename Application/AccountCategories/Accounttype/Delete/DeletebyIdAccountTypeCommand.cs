using Core.AccountCategories.AccountType;
using MediatR;

namespace Application.AccountCategories.Accounttype.Delete
{
    public class DeleteGLbyIdCommand : IRequest<object>
    {
        public AccountTypemodel AccountType { get; set; }
    }
}
