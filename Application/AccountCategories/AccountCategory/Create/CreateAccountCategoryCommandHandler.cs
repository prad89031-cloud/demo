using Core.Abstractions;
using Core.AccountCategories.AccountCategory;
using MediatR;

namespace Application.AccountCategories.AccountCategory.Create
{
    public class CreateAccountCategoryCommandHandler : IRequestHandler<CreateAccountCategoryCommand, object>
    {
        private readonly IAccountCategoryRepository _repository;
        private readonly IUnitOfWorkDB3 _financeDb;

        public CreateAccountCategoryCommandHandler(IAccountCategoryRepository repository, IUnitOfWorkDB3 financeDb)
        {
            _repository = repository;
            _financeDb = financeDb;
        }

        public async Task<object> Handle(CreateAccountCategoryCommand command, CancellationToken cancellationToken)
        {
      
            AccountCategoryModel obj = new AccountCategoryModel();
            var data = await _repository.Insert(command.Category);

            _financeDb.Commit();

            return data;
        }
    }

}