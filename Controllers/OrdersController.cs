using Kitaab.Data.Cart;
using Kitaab.Data.Services;
using Kitaab.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kitaab.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;

        public OrdersController(IBooksService booksService, ShoppingCart shoppingCart, IOrderService orderService)
        {
            _booksService = booksService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _orderService.GetOrderByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var book = await _booksService.GetBookByIdAsync(id);
            if (book != null)
            {
                _shoppingCart.AddItemToCart(book);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var book = await _booksService.GetBookByIdAsync(id);
            if (book != null)
            {
                _shoppingCart.RemoveItemFromCart(book);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
            foreach (var item in items)
            {
                var book = await _booksService.GetBookByIdAsync(item.Book.Id);
                if (book == null || book.Quantity - item.Amount < 0)
                {
                    TempData["Error"] = "Quantity Exceeded";
                    return View("OrderCompleted");
                }
            }
            await _orderService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    } 
}
