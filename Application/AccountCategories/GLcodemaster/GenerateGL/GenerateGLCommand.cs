using Core.AccountCategories.GLcodemaster;
using MediatR;

namespace Application.AccountCategories.GLcodemaster
{
    public class GenerateGLCommand : IRequest<object>
    {
        public GLCodeMastermodels GLCodeMaster { get; set; }
    }
}
