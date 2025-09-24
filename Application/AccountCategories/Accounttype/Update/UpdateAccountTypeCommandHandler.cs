using Core.Abstractions;
using Core.AccountCategories.AccountType;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.Accounttype.Update
{
    public class UpdateAccountTypeCommandHandler : IRequestHandler<UpdateAccountTypeCommand, object>
    {
        private readonly IAccountTypeRepository _repository;
        private readonly IUnitOfWorkDB3 _financeDb;

        public UpdateAccountTypeCommandHandler(IAccountTypeRepository repository, IUnitOfWorkDB3 financeDb)
        {
            _repository = repository;
            _financeDb = financeDb;
        }

        public async Task<object> Handle(UpdateAccountTypeCommand request, CancellationToken cancellationToken)
        {
  
            AccountTypemodel obj = new AccountTypemodel();
            var result = await _repository.UpdateAsync(request.AccountType);

            _financeDb.Commit();

            return result;
        }
    }
}
