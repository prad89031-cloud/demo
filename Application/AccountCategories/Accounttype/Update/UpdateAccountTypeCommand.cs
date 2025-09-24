using Core.AccountCategories.AccountType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AccountCategories.Accounttype.Update
{
    public class UpdateAccountTypeCommand : IRequest<object>
    {
        public AccountTypemodel AccountType { get; set; }
    }
}
