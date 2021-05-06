using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Models;
using Webgentle.BookStore.Repository;

namespace Webgentle.BookStore.Controllers
{
    public class BookController : Controller
    {
        [ViewData]
        public string Title { get; set; }

        public readonly BookRepository _bookRepository=null;
        public readonly LanguagesRepository _languagesRepository=null;
        public BookController(BookRepository bookRepository,LanguagesRepository languagesRepository)
        {
            _bookRepository = bookRepository;
            _languagesRepository = languagesRepository;
        }
        public async Task<ViewResult>  GetAllBooks()
        {
            Title = "All Books";
            var data= await _bookRepository.GetAllBooks();
            return View(data);
        }
        public async Task<ViewResult>  GetBook(int id)
        {   
            var data= await _bookRepository.GetBookById(id);
            Title = "Book Details-"+ data.Title;
            return View(data);
        }
        public List<BookModel> SearchBooks(string bookName,string authorName)
        {
            return _bookRepository.SearchBook(bookName,authorName);
        }
       public async Task<ViewResult> AddBook(bool isSuccess = false,int bookId=0)
        {
            Title = "Add Book";
            var model = new BookModel
            {
                //Language = "2"
            };
           ViewBag.Language=new SelectList( await _languagesRepository.GetLanguages(),"Id","Name");
            
            ViewBag.IsSuccess = isSuccess;
            ViewBag.bookId = bookId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>  AddBook(BookModel bookModel)
        {
            Title = "Add Book";
            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddBook), new { isSuccess = true, bookId = id });
                }
            }

            ViewBag.Language = new SelectList(await _languagesRepository.GetLanguages(), "Id", "Name");


            return View();
        }
       
    }
}
