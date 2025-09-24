using Core.AccountCategories.GLcodemaster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AccountCategories.GLcodemaster.GetAllAccountDetails
{
    public class GetAllAccountDetailsbyIdCommand : IRequest<object>
    {
        public GetAccountDetails GLCodeMaster { get; set; }
    }
}