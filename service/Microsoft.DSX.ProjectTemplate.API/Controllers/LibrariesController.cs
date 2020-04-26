using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DSX.ProjectTemplate.Command.Library;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using System.Collections;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.DSX.ProjectTemplate.API.Controllers
{
    // API Controller for Library 
    // Currently includes routes for GET, POST, and DELETE
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LibrariesController: BaseController
    {
        // Constructor for library controller
        // Passed in mediator object
        public LibrariesController(IMediator mediator) : base(mediator) { }

        // Get all Libraries
        // Returns status 200 if successful 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryDto>>> GetAllLibrary()
        {
            return Ok(await Mediator.Send(new GetAllLibraryQuery()));
        }

        // Get library by Id
        // Takes in an integer id used to query
        // Returns status 200 if successful
        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryDto>> GetLibrary(int id)
        {
            return Ok(await Mediator.Send(new GetLibraryByIdQuery() { LibraryId = id }));
        }

        // Create new library
        // Takes in a LibraryDto representing new library being created
        // Returns status 201 if successful
        [HttpPost]
        public async Task<ActionResult<LibraryDto>> CreateLibrary([FromBody] LibraryDto dto)
        {
            // Return CreateAtAction() instead of Ok() to recieve 201 status instead of 200
            return CreatedAtAction(nameof(CreateLibrary), await Mediator.Send(new CreateLibraryCommand() { Library = dto }));
        }

        // Delete a specific library
        // Takes in integer id of library that is being deleted
        // Returns a status of 200 if successful
        [HttpDelete("{id}")]
        public async Task<ActionResult<LibraryDto>> DeleteLibrary([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeleteLibraryCommand() { LibraryId = id }));
        }


    }
}
