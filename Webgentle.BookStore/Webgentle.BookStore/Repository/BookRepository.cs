﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Data;
using Webgentle.BookStore.Models;

namespace Webgentle.BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context = null;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author=model.Author,
                CreatedOn=DateTime.UtcNow,
                Description=model.Description,
                Title=model.Title,
                LanguagesId=model.LanguageId,
                ToTalPages=model.ToTalPages.HasValue? model.ToTalPages.Value:0,
                UpdatedOn=DateTime.UtcNow
            };

          await  _context.Books.AddAsync(newBook);
          await  _context.SaveChangesAsync();
            return newBook.id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allBooks = await _context.Books.ToListAsync();
            if (allBooks?.Any() == true)
            {
                foreach (var book in allBooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Category=book.Category,
                        id=book.id,
                        ToTalPages=book.ToTalPages,
                        LanguageId=book.LanguagesId,
                        Title=book.Title,
                        Description=book.Description
                    });
                    
                }
            }
            return books;
        }

        public async Task<BookModel>  GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book != null)
            {
                var bookDetails = new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    id = book.id,
                    ToTalPages = book.ToTalPages,
                    LanguageId = book.LanguagesId,
                    Title = book.Title,
                    Description = book.Description

                };
                return bookDetails;
            }
            return null;
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return null;
        }
        
        
    }
}
