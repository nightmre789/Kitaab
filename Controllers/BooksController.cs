using Kitaab.Data;
using Kitaab.Data.Services;
using Kitaab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Filter(string searchString)
        {
            var allBooks = await _service.GetAllAsync(n => n.Author);
            if(!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower().Trim();
                var filteredResult = allBooks.Where(n => n.Title.ToLower().Trim().Contains(searchString) || n.Description.ToLower().Trim().Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }
            return View("Index", allBooks);
        }

        public async Task<IActionResult> Details(int id)
        {
            var bookDetail = await _service.GetBookByIdAsync(id);
            return View(bookDetail);
        }

        public async Task<IActionResult> Create()
        {
            var bookDropdownsData = await _service.GetNewBookDropdownsValues();
            ViewBag.AuthorId = new SelectList(bookDropdownsData.Authors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewBookVM book)
        {
            if (!ModelState.IsValid)
            {
                var bookDropdownsData = await _service.GetNewBookDropdownsValues(); 
                ViewBag.AuthorId = new SelectList(bookDropdownsData.Authors, "Id", "FullName");
                return View(book);
            }
            await _service.AddNewBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var bookDetails = await _service.GetBookByIdAsync(id);
            if (bookDetails == null) return View("NotFound");
            var response = new NewBookVM()
            {
                Id = bookDetails.Id,
                Title = bookDetails.Title,
                Description = bookDetails.Description,
                Price = bookDetails.Price,
                Quantity = bookDetails.Quantity,
                Cover = bookDetails.Cover,
                Category = bookDetails.Category,
                AuthorId = bookDetails.AuthorId
            };

            var bookDropdownsData = await _service.GetNewBookDropdownsValues();
            ViewBag.AuthorId = new SelectList(bookDropdownsData.Authors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewBookVM book)
        {
            if (id != book.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {

                var bookDropdownsData = await _service.GetNewBookDropdownsValues();
                ViewBag.AuthorId = new SelectList(bookDropdownsData.Authors, "Id", "FullName");
                return View(book);
            }
            await _service.UpdateBookAsync(book);
            return RedirectToAction(nameof(Index));
        }
    }
}
