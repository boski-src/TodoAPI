using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Contexts.Todos.Commands;
using TodoAPI.Contexts.Todos.Services;
using TodoAPI.Types;

namespace TodoAPI.Contexts.Todos
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController : ControllerBase
    {
        private ITodoCommandService _commandService;
        private ITodoQueryService _queryService;

        public TodosController(ITodoCommandService commandService, ITodoQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Created), 200)]
        public async Task<IActionResult> Create(CreateTodoCommand command)
        {
            var id = Guid.NewGuid();
            await _commandService.Create(id, command.Title, command.Description, command.ExpiresAt);

            return Created("/", new Created(id));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Todo>), 200)]
        public async Task<IActionResult> Browse([FromQuery] string filter)
        {
            if (TodoFilter.IsValid(filter))
                return Ok(await _queryService.BrowseIncome(filter));

            return Ok(await _queryService.Browse());
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Todo), 200)]
        public async Task<IActionResult> Get(Guid id)
        {
            var todo = await _queryService.Get(id);
            if (todo is null)
                return NotFound();

            return Ok(todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTodoCommand command)
        {
            await _commandService.Update(id, command.Title, command.Description, command.ExpiresAt);
            return NoContent();
        }

        [HttpPatch("{id}/CompletePercentage")]
        public async Task<IActionResult> UpdatePercentage(Guid id, UpdateTodoPercentageCommand command)
        {
            await _commandService.UpdateCompletePercentage(id, command.Percentage);
            return NoContent();
        }

        [HttpPost("{id}/Done")]
        public async Task<IActionResult> MarkAsDone(Guid id)
        {
            await _commandService.MarkAsDone(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _commandService.Delete(id);
            return NoContent();
        }
    }
}