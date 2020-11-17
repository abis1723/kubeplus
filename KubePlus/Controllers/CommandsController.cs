using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using KubePlus.Models;
using KubePlus.Data;
using Nest;
using System.Threading.Tasks;

namespace KubePlus.Controllers
{
    [ApiController]
    [Route("api/commands")]
    public class CommandsController : ControllerBase
    {
        
        private readonly IStudentRepo _repository;
        public CommandsController(IStudentRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {

            var commandItems = await _repository.GetStudents();
            return Ok(commandItems);

        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<Command> GetCommandById(int Id)
        {
            var commandItem = _repository.GetStudentById(Id);
            return Ok(commandItem);
        }

        [HttpPost]
        public ActionResult<Command> SaveSingleAsync(Command command)
        {
            var commandItem = _repository.SaveSingleAsync(command);
            return CreatedAtRoute(nameof(GetCommandById), new {Id = command.Id}, command);
        }
    }
}
