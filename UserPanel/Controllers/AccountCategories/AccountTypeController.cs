using Application.AccountCategories.Accounttype.Create;
using Application.AccountCategories.Accounttype.Delete;
using Application.AccountCategories.Accounttype.GetAll;
using Application.AccountCategories.Accounttype.Update;
using Core.AccountCategories.AccountType;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserPanel.Controllers.AccountrCategories
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetAllGLCommand();
            var result = await _mediator.Send(command);
            return Ok(result); 
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountTypemodel accountType)
        {
            if (accountType == null)
                return BadRequest(new { Status = false, Message = "AccountType cannot be null" });

            var command = new CreateGLCommand { AccountType = accountType };
            var result = await _mediator.Send(command);
            return Ok(result); 
        }

        // PUT:
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AccountTypemodel accountType)
        {
            if (accountType == null || accountType.Id == 0)
                return BadRequest(new { Status = false, Message = "Invalid AccountType data" });

            var command = new UpdateAccountTypeCommand { AccountType = accountType };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteGLbyIdCommand
            {
                AccountType = new AccountTypemodel { Id = id }
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
