using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VroomReconditionHouse.AppDbContext;

namespace VroomReconditionHouse.Models.ViewModels
{
    public class BikeViewModel
    {
        public Bike Bike { get; set; }
        public IEnumerable<Model> Models { get; set; }
        public IEnumerable<Make> Makes { get; set; }     
        public IEnumerable<Currency> Currencies { get; set; }

        public List<Currency> CList = new List<Currency>();

        public List<Currency> CreateList()
        {
            CList.Add(new Currency("US", "US"));
            CList.Add(new Currency("INR", "INR"));
            CList.Add(new Currency("EUR", "EUR"));
            return CList;
        } 

        public BikeViewModel()
        {
            Currencies = CreateList();
        }
    }

    public class Currency {
        public string Id { get; set; }
        public string Name { get; set; }
        public Currency(string id, string name)
        {
            Id = id; 
            Name = name;
        }
    }
}
