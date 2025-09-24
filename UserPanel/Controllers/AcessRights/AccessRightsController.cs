using Application.AccessRights.GetApprovalSettings;
using Application.AccessRights.GetMenusDetails;
using Application.Procurement.Purchase_Requitision.GetRequestorAutoComplete;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserPanel.Controllers.AcessRights
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessRightsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccessRightsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetMenusDetails")]
        public async Task<IActionResult> GetMenusDetails(int userid, int branchId, int orgid)
        {
            var result = await _mediator.Send(new GetMenusDetailsCommand() { userid = userid, orgid = orgid, branchId = branchId });
            return Ok(result);
        }

        [HttpGet("GetApprovalSettings")]
        public async Task<IActionResult> GetApprovalSettings(int userid, int branchId, Int32 orgid, Int32 screenid)
        {
            var result = await _mediator.Send(new GetApprovalSettingsCommand() { branchId = branchId, orgid = orgid, userid = userid,screenid=screenid });
            return Ok(result);
        }

    }
}
