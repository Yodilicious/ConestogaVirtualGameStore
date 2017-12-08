using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaVirtualGameStore.Web.Models
{
    public class Wishlist : BaseModel
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImageFileName { get; set; }
    }
}
