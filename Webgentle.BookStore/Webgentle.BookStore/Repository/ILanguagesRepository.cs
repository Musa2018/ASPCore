using System.Collections.Generic;
using System.Threading.Tasks;
using Webgentle.BookStore.Models;

namespace Webgentle.BookStore.Repository
{
    public interface ILanguagesRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}