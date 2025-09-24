using Core.Abstractions;
using Core.AccountCategories.GLcodemaster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.GLCodeMaster.Create
{
    public class CreateGLCodeMasterCommandHandler : IRequestHandler<CreateGLCommand, object>
    {
        private readonly IGLCodeMasterRepository _repository;
        private readonly IUnitOfWorkDB3 _financeDb;

        public CreateGLCodeMasterCommandHandler(IGLCodeMasterRepository repository, IUnitOfWorkDB3 financeDb)
        {
            _repository = repository;
            _financeDb = financeDb;
        }

        public async Task<object> Handle(CreateGLCommand command, CancellationToken cancellationToken)
        {
           
            var glCodeMaster = command.GLCodeMaster;

            var result = await _repository.CreateAsync(glCodeMaster);

            _financeDb.Commit();

            return result;
        }
    }
}
