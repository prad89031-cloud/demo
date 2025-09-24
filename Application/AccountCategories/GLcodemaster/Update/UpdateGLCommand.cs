using Core.AccountCategories.GLcodemaster;
using MediatR;

namespace Application.AccountCategories.GLCodeMaster.Update
{
    public class UpdateGLCommand : IRequest<object>
    {
        public GLCodeMastermodels GLCodeMaster { get; set; }
    }
}
