using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VroomReconditionHouse.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItem <T> (this IEnumerable<T> Items)
        {
            List<SelectListItem> SList = new List<SelectListItem>();
            SelectListItem sli = new SelectListItem {
                Text = "----Select----",
                Value = "0"
            };
            SList.Add(sli);
            foreach(var item in Items)
            {
             sli = new SelectListItem
                {
                    Text = item.GetType().GetProperty("Name").GetValue(item, null).ToString(),
                    Value = item.GetType().GetProperty("Id").GetValue(item, null).ToString(),
                };
                SList.Add(sli);
            }
            return SList;
         }
    }
}
