using Core.Abstractions;
using Core.AccountCategories.AccountType;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.Accounttype.Create
{
    public class CreateAccountTypeCommandHandler : IRequestHandler<CreateGLCommand, object>
    {
        private readonly IAccountTypeRepository _repository;
        private readonly IUnitOfWorkDB3 _financeDb;

        public CreateAccountTypeCommandHandler(IAccountTypeRepository repository, IUnitOfWorkDB3 financeDb)
        {
            _repository = repository;
            _financeDb = financeDb;
        }

        public async Task<object> Handle(CreateGLCommand command, CancellationToken cancellationToken)
        {
 
            AccountTypemodel obj = new AccountTypemodel();
            var data = await _repository.InsertAsync(command.AccountType);
            _financeDb.Commit();
            return data;
        }
    }
}
