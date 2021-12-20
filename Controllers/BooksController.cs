using Kitaab.Data;
using Kitaab.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitaab.Controllers
{
    public class BooksController : Controller
    {

        private readonly IBooksService _service;

        public BooksController(IBooksService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allBooks = await _service.GetAllAsync(n => n.Author);
            return View(allBooks);
        }
    }
}
