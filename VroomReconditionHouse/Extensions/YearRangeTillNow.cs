using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VroomReconditionHouse.Extensions
{
    public class YearRangeTillNow:RangeAttribute
    {
        public  YearRangeTillNow( int StartYear): base(StartYear, DateTime.Today.Year)
        {

        }
    }
}
