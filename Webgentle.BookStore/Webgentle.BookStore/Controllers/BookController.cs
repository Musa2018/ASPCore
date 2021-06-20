using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Models;
using Webgentle.BookStore.Repository;

namespace Webgentle.BookStore.Controllers
{    [Route("[controller]/[action]")]

    public class BookController : Controller
    {
        [ViewData]
        public string Title { get; set; }

        public readonly BookRepository _bookRepository=null;
        public readonly LanguagesRepository _languagesRepository=null;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(BookRepository bookRepository,LanguagesRepository languagesRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languagesRepository = languagesRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [Route("~/All-Books")]
        public async Task<ViewResult>  GetAllBooks()
        {
            Title = "All Books";
            var data= await _bookRepository.GetAllBooks();
            return View(data);
        }
        [Route("~/book-details/{id:int:min(1)}",Name ="BookDetailsRoute")]
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
                if (bookModel.coverPhoto!=null) {
                    string folder = "books/cover/";
                  bookModel.CoverImageUrl =  await UploadImage(folder, bookModel.coverPhoto);
                    //folder += Guid.NewGuid().ToString() + "_" + bookModel.coverPhoto.FileName;
                    //bookModel.CoverImageUrl = "/"+folder;
                    //string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath,folder);
                    //await bookModel.coverPhoto.CopyToAsync(new FileStream(serverFolder,FileMode.Create));


                }
                if (bookModel.galleryFiles != null)
                {
                    string folder = "books/gallery/";
                    bookModel.Gallery = new List<GalleryModel>();

                    foreach (var file in bookModel.galleryFiles)
                    {
                        var gallery = new GalleryModel() {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                    };

                        bookModel.Gallery.Add(gallery);

                    }

                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BoohPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                    
                }
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddBook), new { isSuccess = true, bookId = id });
                }
            }

            ViewBag.Language = new SelectList(await _languagesRepository.GetLanguages(), "Id", "Name");


            return View();
        }

        private async Task<string> UploadImage(string folderPath,IFormFile file)
        {
            
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }
       
    }
}
