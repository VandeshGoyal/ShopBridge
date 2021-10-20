using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Helper
{
    public class Response
    {
        public string Message { get; set; }
        public bool success { get; set; }
        public List<Inventory> Items { get; set; }
    }
}
