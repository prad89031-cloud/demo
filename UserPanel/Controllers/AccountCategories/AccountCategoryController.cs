using Application.AccountCategories.AccountCategory.Create;
using Application.AccountCategories.AccountCategory.Update;
using Application.AccountCategories.AccountCategory.Delete;
using Application.AccountCategories.AccountCategory.GetAll;
using Core.AccountCategories.AccountCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UserPanel.Controllers.AccountCategories
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountCategoryModel category)
        {
            if (category == null)
                return BadRequest(new { status = false, message = "Invalid payload." });

            var result = await _mediator.Send(new CreateAccountCategoryCommand { Category = category });
            return Ok(result);
        }

        // UPDATE
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AccountCategoryModel category)
        {
            if (category == null || category.Id <= 0)
                return BadRequest(new { status = false, message = "Invalid payload or missing Id." });

            var result = await _mediator.Send(new UpdateAccountCategoryCommand { Category = category });
            return Ok(result);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { status = false, message = "Invalid ID." });

            var result = await _mediator.Send(new DeleteAccountCategoryCommand
            {
                Category = new AccountCategoryModel { Id = id }
            });

            return Ok(result);
        }

        // GET ALL
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllAccountCategoryCommand());
            return Ok(result);
        }

    }
}
