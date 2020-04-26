using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Command.Library
{
    // Class for all libraries
    public class GetAllLibraryQuery: IRequest<IEnumerable<LibraryDto>> {}

    // CLass for specific library by id
    public class GetLibraryByIdQuery : IRequest<LibraryDto>
    {
        public int LibraryId { get; set; }
    }

    // The query handler class
    public class LibraryQueryHandler : QueryHandlerBase,
        IRequestHandler<GetAllLibraryQuery, IEnumerable<LibraryDto>>,
        IRequestHandler<GetLibraryByIdQuery, LibraryDto>
    {
        // Query handler constructor
        public LibraryQueryHandler(
            IMediator mediator,
            ProjectTemplateDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base (mediator, database, mapper, authorizationService)
        {

        }

        // Get all Libraries
        // Takes in the request and cancellation token
        // Returns a list of all Libraries 
        public async Task<IEnumerable<LibraryDto>> Handle(GetAllLibraryQuery request, CancellationToken cancellationToken)
        {
        
            return await Database.Libraries
                .Select(x => Mapper.Map<LibraryDto>(x))
                .ToListAsync();
            
        }

        // Get by ID
        // Takes in request and cancellation token
        // If it is not found, throws entity not found exception
        // Returns the Library
        public async Task<LibraryDto> Handle(GetLibraryByIdQuery request, CancellationToken cancellationToken)
        {
            if(request.LibraryId.ToString().Length == 0)
            {
                throw new BadRequestException($"A valid {nameof(Data.Models.Library)} Id must be provided");
            }

            var innerResult = await Database.Libraries
                .FindAsync(request.LibraryId);

            if(innerResult == null)
            {
                throw new EntityNotFoundException($"{nameof(Data.Models.Library)} with Id {request.LibraryId} cannot be found");
            }

            return Mapper.Map<LibraryDto>(innerResult);
        }
    }
}
