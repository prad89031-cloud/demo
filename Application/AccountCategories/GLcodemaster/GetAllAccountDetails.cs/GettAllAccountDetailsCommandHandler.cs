using Core.Abstractions;
using Core.AccountCategories.GLcodemaster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.GLcodemaster.GetAllAccountDetails
{
    public class GetAllAccountDetailsCommandHandler : IRequestHandler<GetAllAccountDetailsCommand, object>
    {
        private readonly IGLCodeMasterRepository _repository;

        public GetAllAccountDetailsCommandHandler(IGLCodeMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> Handle(GetAllAccountDetailsCommand command, CancellationToken cancellationToken)
        {
            
            var result = await _repository.GetAllAccountTypeDetailsAsync();


            return result;
        }
    }
}
