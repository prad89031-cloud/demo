using Core.Abstractions;
using Core.AccountCategories.GLcodemaster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.GLCodeMaster.Update
{
    public class UpdateGLCommandHnadler : IRequest<object>
    {
        public object GLCodeMaster { get; set; } // using object as per your repository
    }

    public class UpdateGLCodeMasterCommandHandler : IRequestHandler<UpdateGLCommand, object>
    {
        private readonly IGLCodeMasterRepository _repository;
        private readonly IUnitOfWorkDB3 _financeDb;

        public UpdateGLCodeMasterCommandHandler(IGLCodeMasterRepository repository, IUnitOfWorkDB3 financeDb)
        {
            _repository = repository;
            _financeDb = financeDb;
        }

        public async Task<object> Handle(UpdateGLCommand command, CancellationToken cancellationToken)
        {
            var glCodeMaster = command.GLCodeMaster;

    
            var result = await _repository.UpdateAsync(glCodeMaster);

            _financeDb.Commit();

            return result;
        }
    }
}
