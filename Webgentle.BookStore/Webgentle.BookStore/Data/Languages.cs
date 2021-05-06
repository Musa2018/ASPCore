using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webgentle.BookStore.Data
{
    public class Languages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dsecription { get; set; }

        public ICollection<Books> Books { get; set; }
    }
}
