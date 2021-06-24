using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Data;
using Webgentle.BookStore.Models;

namespace Webgentle.BookStore.Repository
{
    public class LanguagesRepository : ILanguagesRepository
    {
        private readonly BookStoreContext _context = null;
        public LanguagesRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<List<LanguageModel>> GetLanguages()
        {
            return await _context.Languages.Select(x => new LanguageModel()
            {
                Id = x.Id,
                Dsecription = x.Dsecription,
                Name = x.Name
            }).ToListAsync();
        }

    }
}
