using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Models;

namespace Webgentle.BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }

        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBook(string title,string authorName)
        {
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorName)).ToList();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel() {id=1,Title="MVC",Author="Musa",Description="This is description of MVC book"},
                new BookModel() {id=2,Title="C#",Author="John",Description="This is description of C# book"},
                new BookModel() {id=3,Title="Java",Author="Ali",Description="This is description of Java book"},
                new BookModel() {id=4,Title="F#",Author="Mohammad",Description="This is description of F# book"},
                new BookModel() {id=5,Title="VB",Author="Tamer",Description="This is description of VB book"},
            };
        }
    }
}
