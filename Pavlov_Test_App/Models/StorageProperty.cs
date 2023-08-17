using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pavlov_Test_App.Models
{
    internal class StorageProperty
    {
        public int Id { set; get; }
        [Range(1, 100, ErrorMessage = "Ширина должна быть в диапазоне {1} - {2}!")]
        public int Width { get; set; }
        [Range(1, 100, ErrorMessage = "Высота должна быть в диапазоне {1} - {2}!")]
        public int Height { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Вес должен быть больше нуля!")]
        public int Depth { get; set; }
        public int Weight { get; set; }
        public int Volume { get; set; }
        public StorageProperty(int id, int height, int width, int depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
            Id = id;
        }
        public DateOnly ExpirationDate { get; set; }
    }
}
