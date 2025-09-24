using Core.Abstractions;
using Core.AccountCategories.GLcodemaster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.GLcodemaster
{
    public class GenerateGLCommandHandler : IRequestHandler<GenerateGLCommand, object>
    {
        private readonly IGLCodeMasterRepository _repository;

        public GenerateGLCommandHandler(IGLCodeMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> Handle(GenerateGLCommand command, CancellationToken cancellationToken)
        {
            var glModel = command.GLCodeMaster;

            if (glModel == null)
            {
                return new
                {
                    Data = (string)null,
                    Status = false,
                    Message = "GLCodeMaster details not provided"
                };
            }

            return await _repository.GenerateGLSequenceIdAsync(glModel.CategoryId, glModel.Id);
        }
    }
}
