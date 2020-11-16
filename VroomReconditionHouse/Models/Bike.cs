using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VroomReconditionHouse.Extensions;

namespace VroomReconditionHouse.Models
{
    public class Bike
    {
        public int Id { get; set; }
        public Make Make { get; set; }
        [RegularExpression("^[1-9]*$", ErrorMessage ="Select Make")]
        public int MakeID { get; set; }
        public Model Model { get; set; }
        [RegularExpression("^[1-9]*$", ErrorMessage = "Select Model")]
        public int ModelID { get; set; }
        [YearRangeTillNow(2000, ErrorMessage = "Provide Year after 2000")]
        public int Year { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Provide Mileage")]
        public int Mileage { get; set; }
        public string Features { get; set; }
        [Required, Display(Name = "Seller Name")]
        public string SellerName { get; set; }
        [Display(Name = "Seller Email")]
        public string SellerEmail { get; set; }
        [Required, Display(Name = "Seller Phone")]
        public string SellerPhone { get; set; }
        [Required(ErrorMessage = "Provide Price")]
        public int Price { get; set; }
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Select Currency")]
        public string Currency { get; set; }
        public string ImagePath { get; set; }
    }
}
