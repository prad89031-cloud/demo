using Core.Abstractions;
using Core.AccountCategories.AccountCategory;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.AccountCategory.Update
{
    public class UpdateAccountCategoryCommandHandler : IRequestHandler<UpdateAccountCategoryCommand, object>
    {
        private readonly IAccountCategoryRepository _repository;
        private readonly IUnitOfWorkDB3 _financeDb;

        public UpdateAccountCategoryCommandHandler(IAccountCategoryRepository repository, IUnitOfWorkDB3 financeDb)
        {
            _repository = repository;
            _financeDb = financeDb;
        }

        public async Task<object> Handle(UpdateAccountCategoryCommand request, CancellationToken cancellationToken)
        {
            var updatedCategory = await _repository.Update(request.Category);

            _financeDb.Commit();

            return updatedCategory;
        }

    }
}
