﻿using Microsoft.AspNetCore.Mvc;
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
        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
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
       public ViewResult AddBook(bool isSuccess = false,int bookId=0)
        {
            Title = "Add Book";
            ViewBag.IsSuccess = isSuccess;
            ViewBag.bookId = bookId;
            return View();
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
            ModelState.AddModelError("", "custom error message");
           
            return View();
        }
    }
}
