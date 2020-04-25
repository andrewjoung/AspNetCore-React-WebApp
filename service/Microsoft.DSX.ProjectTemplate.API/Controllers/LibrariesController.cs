using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DSX.ProjectTemplate.Command.Library;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using System.Collections;

namespace Microsoft.DSX.ProjectTemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LibrariesController: BaseController
    {
        public LibrariesController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryDto>>> GetAllLibrary()
        {
            return Ok(await Mediator.Send(new GetAllLibraryQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryDto>> GetLibrary(int id)
        {
            return Ok(await Mediator.Send(new GetLibraryByIdQuery() { LibraryId = id }));
        }

        [HttpPost]
        public async Task<ActionResult<LibraryDto>> CreateLibrary([FromBody] LibraryDto dto)
        {
            return Ok(await Mediator.Send(new CreateLibraryCommand() { Library = dto }));
        }


    }
}
