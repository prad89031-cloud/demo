using Core.Abstractions;
using Core.AccountCategories.AccountType;
using Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountCategories.Accounttype.Delete
{
    public class DeletebyIdAccountTypeCommandHandler : IRequestHandler<DeleteGLbyIdCommand, object>
    {
        private readonly IAccountTypeRepository _repository;

        public DeletebyIdAccountTypeCommandHandler(IAccountTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> Handle(DeleteGLbyIdCommand request, CancellationToken cancellationToken)
        {
            if (request.AccountType == null || request.AccountType.Id <= 0)
            {
                return new
                {
                    Data = (object)null,
                    Status = false,
                    Message = "Invalid AccountType Id"
                };
            }

            var response = await _repository.DeleteAsync(request.AccountType.Id) as ResponseModel;
            int rowsAffected = response != null && response.Data != null ? Convert.ToInt32(response.Data) : 0;


            return response;
        }
    }
}
