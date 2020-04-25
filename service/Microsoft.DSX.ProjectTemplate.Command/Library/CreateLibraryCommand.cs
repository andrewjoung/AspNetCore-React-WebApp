using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Events;
using Microsoft.DSX.ProjectTemplate.Data.Exceptions;
using Microsoft.DSX.ProjectTemplate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;


namespace Microsoft.DSX.ProjectTemplate.Command.Library
{
    public class CreateLibraryCommand : IRequest<LibraryDto>
    {
        public LibraryDto Library { get; set; }
    }

    public class CreateLibraryCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateLibraryCommand, LibraryDto>
    {
        public CreateLibraryCommandHandler(
        IMediator mediator,
        ProjectTemplateDbContext database,
        IMapper mapper,
        IAuthorizationService authorizationService)
        : base(mediator, database, mapper, authorizationService)
        {
        }

        public async Task<LibraryDto> Handle(CreateLibraryCommand request, CancellationToken cancellationToken)
        {
            if(request.Library == null)
            {
                throw new BadRequestException($"A valid {nameof(Data.Models.Library)} must be provided");
            }

            if(!request.Library.IsValid())
            {
                throw new BadRequestException(request.Library.GetValidationErrors());
            }

            var dto = request.Library;

            var model = new Data.Models.Library()
            {
                Name = dto.Name,
                Address = dto.Address
            };

            Database.Libraries.Add(model);

            await Database.SaveChangesAsync();

            await Mediator.Publish(new LibraryCreatedDomainEvent(model));

            return Mapper.Map<LibraryDto>(model);
        }

    }
}
