using Core.AccountCategories.GLcodemaster;
using MediatR;

namespace Application.AccountCategories.GLCodeMaster.Create
{
    public class CreateGLCommand : IRequest<object>
    {
        public GLCodeMastermodels GLCodeMaster { get; set; }
    }
}
