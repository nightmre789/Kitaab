using Kitaab.Data.Base;
using Kitaab.Data.ViewModels;
using Kitaab.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitaab.Data.Services
{
    public class BooksService : EntityBaseRepository<Book>, IBooksService
    {
        private readonly AppDBContext _context;
        public BooksService(AppDBContext context) : base(context)
        { _context = context; }

        public async Task AddNewBookAsync(NewBookVM data)
        {
            var newBook = new Book()
            {
                Title = data.Title,
                Description = data.Description,
                Cover = data.Cover,
                Price = data.Price,
                Quantity = data.Quantity,
                AuthorId = data.AuthorId,
                Category = data.Category
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var bookDetails = _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(n => n.Id == id);
            return await bookDetails;
        }

        public async Task<NewBookDropdownsVM> GetNewBookDropdownsValues()
        {
            var response = new NewBookDropdownsVM();
            response.Authors = await _context.Authors.OrderBy(n => n.FullName).ToListAsync();
            return response;
        }

        public async Task UpdateBookAsync(NewBookVM data)
        {
            var book = await _context.Books.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (book != null)
            {
                book.Title = data.Title;
                book.Description = data.Description;
                book.Cover = data.Cover;
                book.Price = data.Price;
                book.Quantity = data.Quantity;
                book.AuthorId = data.AuthorId;
                book.Category = data.Category;
                await _context.SaveChangesAsync();
            }
        }
    }
}
