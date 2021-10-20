using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Helper
{
    public static class ResponseDtoHelper
    {
        public static Response GetResponseDto(List<Inventory> items)
        {
            Response resp = new Response();
            if (items.Count > 0 && items[0] != null)
            {
                resp.Message = "Operation Successfull";
                resp.success = true;
                resp.Items = items;
            }
            else
            {
                resp.Message = "No Data Found";
                resp.success = true;
                resp.Items = new List<Inventory>();
            };

            return resp;
        }
    }
}
