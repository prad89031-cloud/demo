using Core.Abstractions;
using Core.AccountCategories.GLcodemaster;
using Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.GLCodeMaster.Delete
{
    public class DeleteGLByIdCommand : IRequest<object>
    {
        public int Id { get; set; }
    }

    public class DeleteGLCodeMasterCommandHandler : IRequestHandler<DeleteGLByIdCommand, object>
    {
        private readonly IGLCodeMasterRepository _repository;
        private readonly IUnitOfWorkDB3 _financeDb;

        public DeleteGLCodeMasterCommandHandler(IGLCodeMasterRepository repository, IUnitOfWorkDB3 financeDb)
        {
            _repository = repository;
            _financeDb = financeDb;
        }

        public async Task<object> Handle(DeleteGLByIdCommand command, CancellationToken cancellationToken)
        {
 
            var result = await _repository.DeleteAsync(command.Id);


            _financeDb.Commit();


            return result;
        }
    }
}
