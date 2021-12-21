using Kitaab.Data.Base;
using Kitaab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitaab.Data.Services
{
    public interface IAuthorsService : Base.IEntityBaseRepository<Author>
    {
    }
}
