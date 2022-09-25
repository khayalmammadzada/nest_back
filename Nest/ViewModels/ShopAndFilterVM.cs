using System;
using Nest.Models;

namespace Nest.ViewModels
{
    public class ShopAndFilterVM
    {
        //public List<Product> Products { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<int> VendorIds { get; set; }
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }
    }
}

