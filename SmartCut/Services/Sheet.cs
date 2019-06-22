using SmartCut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCut.Services
{
    public class Sheet 
    {
        public int Length { get; set; }

        public int Width { get; set; }

        public double LossPercent { get; set; }

        public int Total { get; set; }

        public Sheet(int length, int width, bool sameDirection = true)
        {
            if (sameDirection)
            {
                Length = length;
                Width = width;
            }
            else
            {
                Length = width;
                Width = length;
            }
        }

        internal void Cut(OrderFilter filter, bool allowRotation = false)
        {
            throw new NotImplementedException();
        }
    }
}
