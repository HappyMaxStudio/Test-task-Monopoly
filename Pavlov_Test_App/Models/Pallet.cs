using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pavlov_Test_App.Models
{
    internal class Pallet : StorageProperty
    {
        public List<Box> boxes = new List<Box>();
        public Pallet(int id, int height, int width, int depth) : base(id, height, width, depth)
        {
            Weight = 30;
            Volume = height * width * depth;

        }
        public void AddBox(Box box)
        {
            try
            {
                if (Width >= box.Width || Depth >= box.Depth)
                {
                    boxes.Add(box);
                    UpdateValues();
                }
                else
                {
                    throw new InvalidDataException("Габариты коробки больше габаритов паллеты");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        public void RemoveBox(int id)
        {
            boxes = boxes.Where(x => x.Id != id).ToList();
            UpdateValues();
        }
        void UpdateValues()
        {
            Volume = boxes.Sum(x => x.Volume) + Height * Width * Depth;
            Weight = boxes.Sum(x => x.Weight) + 30;
            ExpirationDate = boxes.Min(x => x.ExpirationDate);
        }
    }
}
