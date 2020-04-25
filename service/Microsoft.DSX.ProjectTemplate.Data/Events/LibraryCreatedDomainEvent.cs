using MediatR;
using Microsoft.DSX.ProjectTemplate.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.DSX.ProjectTemplate.Data.Events
{
    public class LibraryCreatedDomainEvent : INotification
    {
        public LibraryCreatedDomainEvent(Library library)
        {
            Library = library;
        }

        public Library Library { get; }
    }
}
