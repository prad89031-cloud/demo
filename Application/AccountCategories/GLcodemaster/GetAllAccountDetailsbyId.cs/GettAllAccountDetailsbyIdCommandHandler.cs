using Core.Abstractions;
using Core.AccountCategories.GLcodemaster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.GLcodemaster.GetAllAccountDetails
{
    public class GetAllAccountDetailsbyIdCommandHandler : IRequestHandler<GetAllAccountDetailsbyIdCommand, object>
    {
        private readonly IGLCodeMasterRepository _repository;

        public GetAllAccountDetailsbyIdCommandHandler(IGLCodeMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> Handle(GetAllAccountDetailsbyIdCommand command, CancellationToken cancellationToken)
        {
            
            int glId = command.GLCodeMaster?.Id ?? 0;

   
            var result = await _repository.GetAccountTypeDetailsByIdAsync(glId);


            return result;
        }
    }
}
