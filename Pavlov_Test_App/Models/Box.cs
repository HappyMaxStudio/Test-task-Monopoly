using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pavlov_Test_App.Models
{
    internal class Box : StorageProperty
    {
        protected DateOnly ProductionDate { get; set; }
        public Box(int id, int height, int width, int depth, int weight, DateOnly productionDate) : base(id, height, width, depth)
        {
            Weight = weight;
            Volume = depth * height * width;
            this.ProductionDate = productionDate;
            this.ExpirationDate = productionDate.AddDays(100);
        }


    }
}
