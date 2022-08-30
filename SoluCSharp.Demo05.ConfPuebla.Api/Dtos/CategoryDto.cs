using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoluCSharp.Demo05.ConfPuebla.Api.Dtos
{
    public record CategoryDto(int? Id, string Name, string Description, string Picture);
    
}
