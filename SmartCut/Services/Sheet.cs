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

        public double LossPercent { get; set; } = 100;

        public int Total { get; set; } = 0;

        public Sheet(int length, int width, bool sameDirection = true)
        {
            Length = length;
            Width = width;
            if (!sameDirection)
                Rotate();
        }

        void Rotate()
        {
            int buffer = Length;
            Length = Width;
            Width = buffer;
        }

        internal void Cut(OrderFilter filter, bool allowRotation = false)
        {
            if (allowRotation)
                Total = CutWithRotation(filter);
            else
                Total = CutWithOutRotation(filter);
        }

        private int CutWithOutRotation(OrderFilter filter)
        {
            return (Width / filter.Width) * (Length / filter.Length);
        }

        private int CutWithRotation(OrderFilter filter)
        {
            //check dictionary
            if (Width == filter.Width || Length == filter.Length)
                return CutWithOutRotation(filter);
            if (Width == filter.Length || Length == filter.Width)
            {
                Rotate();
                return CutWithOutRotation(filter);
            }


            //add to dictinary
        }
    }
}
