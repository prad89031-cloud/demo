using Core.Abstractions;
using Core.AccountCategories.AccountCategory;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.AccountCategory.Delete
{
    public class DeleteAccountCategoryCommandHandler : IRequestHandler<DeleteAccountCategoryCommand, object>
    {
        private readonly IAccountCategoryRepository _repository;
        private readonly IUnitOfWorkDB3 _financeDb;

        public DeleteAccountCategoryCommandHandler(IAccountCategoryRepository repository, IUnitOfWorkDB3 financeDb)
        {
            _repository = repository;
            _financeDb = financeDb;
        }

        public async Task<object> Handle(DeleteAccountCategoryCommand request, CancellationToken cancellationToken)
        {
            AccountCategoryModel obj = new AccountCategoryModel();
            var result = await _repository.Delete(request.Category.Id);

            _financeDb.Commit();

            return result;
        }
    }
}
