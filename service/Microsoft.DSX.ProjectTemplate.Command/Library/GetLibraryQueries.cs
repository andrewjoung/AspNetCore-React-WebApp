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
    public class GetAllLibraryQuery: IRequest<IEnumerable<LibraryDto>> {}

    public class GetLibraryByIdQuery : IRequest<LibraryDto>
    {
        public int LibraryId { get; set; }
    }

    public class LibraryQueryHandler : QueryHandlerBase,
        IRequestHandler<GetAllLibraryQuery, IEnumerable<LibraryDto>>,
        IRequestHandler<GetLibraryByIdQuery, LibraryDto>
    {
        public LibraryQueryHandler(
            IMediator mediator,
            ProjectTemplateDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base (mediator, database, mapper, authorizationService)
        {

        }

        // Get all Libraries
        public async Task<IEnumerable<LibraryDto>> Handle(GetAllLibraryQuery request, CancellationToken cancellationToken)
        {
        
            return await Database.Libraries
                .Select(x => Mapper.Map<LibraryDto>(x))
                .ToListAsync();
            
        }

        // Get by ID
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
