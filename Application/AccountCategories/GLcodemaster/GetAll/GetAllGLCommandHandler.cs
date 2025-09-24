using Core.Abstractions;
using Core.AccountCategories.GLcodemaster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.GLCodeMaster.GetAll
{
    public class GetAllGLCommandHandler : IRequest<object>
    {
      
    }

    public class GetAllGLCodeCommandHandler : IRequestHandler<GetAllGLCommand, object>
    {
        private readonly IGLCodeMasterRepository _repository;

        public GetAllGLCodeCommandHandler (IGLCodeMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> Handle(GetAllGLCommand command, CancellationToken cancellationToken)
        {

            var result = await _repository.GetAllAsync();

            return result;
        }
    }
}
