using Ecom.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecom.Core.DTO
{
    public record CategoryDTO
    (string Name, string Description);

    public record UpdateCategoryDTO
    (int Id, string Name, string Description);
}
