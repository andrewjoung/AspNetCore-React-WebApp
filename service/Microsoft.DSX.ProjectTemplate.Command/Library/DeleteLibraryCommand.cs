﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.DSX.ProjectTemplate.Command.Group;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.Exceptions;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Command.Library
{
    // Delete Library Command class
    // Includes LibraryId property of type int
    public class DeleteLibraryCommand: IRequest<bool>
    {
        public int LibraryId { get; set; }
    }

    // The command handler class
    public class DeleteLibraryCommandHandler : CommandHandlerBase,
            IRequestHandler<DeleteLibraryCommand, bool>
    {
        public DeleteLibraryCommandHandler(
            IMediator mediator,
            ProjectTemplateDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        // Handle function for delete command
        // Throws bad request exceptions for invalid requests
        // Returns true when library is deleted
        public async Task<bool> Handle(DeleteLibraryCommand request, CancellationToken cancellationToken)
        {
            if(request.LibraryId <= 0)
            {
                throw new BadRequestException($"A valid {nameof(Data.Models.Group)} ID must be provided");
            }

            var library = await Database.Libraries.FindAsync(request.LibraryId);

            if(library == null)
            {
                throw new EntityNotFoundException($"{nameof(Data.Models.Library)} not found");
            }

            Database.Libraries.Remove(library);

            await Database.SaveChangesAsync();

            Debug.Assert(Database.Libraries.Find(library.Id) == null);

            return true;
        }
    }
}
