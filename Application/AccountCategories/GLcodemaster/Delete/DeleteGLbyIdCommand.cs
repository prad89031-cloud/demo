using Core.AccountCategories.GLcodemaster;
using MediatR;

namespace Application.AccountCategories.GLcodemaster.Delete
{
    public class DeleteGLbyIdCommand : IRequest<object>
    {
        public  GLCodeMastermodels Id { get; set; }
    }
}
