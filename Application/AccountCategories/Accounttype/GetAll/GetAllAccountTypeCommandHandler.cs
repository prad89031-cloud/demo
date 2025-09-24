using Core.Abstractions;
using Core.AccountCategories.AccountType;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.Accounttype.GetAll
{
    public class GetAllAccountTypeCommandHandler : IRequestHandler<GetAllGLCommand, object>
    {
        private readonly IAccountTypeRepository _repository;
        private readonly IUnitOfWorkDB3 _financeDb;

        public GetAllAccountTypeCommandHandler(IAccountTypeRepository repository)
        {
            _repository = repository;
            _financeDb = _financeDb;
        }

        public async Task<object> Handle(GetAllGLCommand request, CancellationToken cancellationToken)
        {
            // Fetch all account types
            AccountTypemodel obj = new AccountTypemodel();
            var result = await _repository.GetAllAsync();

            return result;
        }
    }
}
