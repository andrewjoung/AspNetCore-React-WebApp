using Microsoft.DSX.ProjectTemplate.Data.Models;

namespace Microsoft.DSX.ProjectTemplate.Data.DTOs
{
    // A Library data transfer object
    // Needs name of library and library address
    public class LibraryDto: AuditDto<int>
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                ModelState.AddModelError(nameof(Name), $"{nameof(Name)} cannot be null or empty.");
            }

            
            if(string.IsNullOrWhiteSpace(Address.ToString()))
            {
                ModelState.AddModelError(nameof(Address), $"{nameof(Address)} cannot be null or empty");
            }
            
            return ModelState.IsValid;
        }
    }

}
