using Kitaab.Data.Base;
using Kitaab.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitaab.Data.Services
{
    public class AuthorsService : EntityBaseRepository<Author>, IAuthorsService
    {
        public AuthorsService(AppDBContext context) : base(context) { }
     
    }
}
