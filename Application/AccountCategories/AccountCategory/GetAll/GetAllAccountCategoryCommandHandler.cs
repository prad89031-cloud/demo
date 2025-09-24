using Core.Abstractions;
using Core.AccountCategories.AccountCategory;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.AccountCategory.GetAll
{
    public class GetAllAccountCategoryCommandHandler : IRequestHandler<GetAllAccountCategoryCommand, object>
    {
        private readonly IAccountCategoryRepository _repository;

        public GetAllAccountCategoryCommandHandler(IAccountCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> Handle(GetAllAccountCategoryCommand request, CancellationToken cancellationToken)
        {
            AccountCategoryModel obj = new AccountCategoryModel();
            var result = await _repository.GetAll();


            return result;
        }
    }
}
