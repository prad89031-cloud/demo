using Application.AccountCategories.GLcodemaster;
using Application.AccountCategories.GLcodemaster.GetAllAccountDetails;
using Application.AccountCategories.GLCodeMaster.Create;
using Application.AccountCategories.GLCodeMaster.Delete;
using Application.AccountCategories.GLCodeMaster.Update;
using Application.AccountCategories.GLCodeMaster.GetAll;
using Core.AccountCategories.GLcodemaster;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UserPanel.Controllers.AccountCategories
{
    [ApiController]
    [Route("api/[controller]")]
    public class GLCodeMasterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GLCodeMasterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetAllGLCommand();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GLCodeMastermodels glCode)
        {
            if (glCode == null)
                return BadRequest(new { Data = (object)null, Status = false, Message = "GLCodeMaster cannot be null" });

            var command = new CreateGLCommand { GLCodeMaster = glCode };
            var result = await _mediator.Send(command);

            return Ok(result); ;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] GLCodeMastermodels glCode)
        {
            if (glCode == null || glCode.Id == 0)
                return BadRequest(new { Status = false, Message = "Invalid GLCodeMaster data" });

            var command = new UpdateGLCommand { GLCodeMaster = glCode };
            var result = await _mediator.Send(command);

            return Ok(result); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteGLByIdCommand { Id = id });

            if (result is ResponseModel response)
                return StatusCode(response.StatusCode, response);

            return Ok(result);
        }


        [HttpGet("GenerateSequence")]
        public async Task<IActionResult> GenerateSequence(int categoryId, int id)
        {
            var command = new GenerateGLCommand
            {
                GLCodeMaster = new GLCodeMastermodels
                {
                    CategoryId = categoryId,
                    Id = id
                }
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        
        [HttpGet("GetAccountTypeDetailsById/{id}")]
        public async Task<IActionResult> GetAccountTypeDetailsById(int id)
        {
            var command = new GetAllAccountDetailsbyIdCommand
            {
                GLCodeMaster = new GetAccountDetails { Id = id }
            };

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound(new { Data = (object)null, Status = false, Message = $"No record found for GLId {id}" });

            return Ok(result);
        }

        
        [HttpGet("GetAllAccountTypeDetails")]
        public async Task<IActionResult> GetAllAccountTypeDetails()
        {
            var command = new GetAllAccountDetailsCommand();
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
