using SmartCut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCut.Services
{
    public class Sheet 
    {
        int Length, Width, Area;
        public int Total = 0;
        public double LossPercent = 100;
        public static OrderItem OrderItem;

        public Sheet(int length, int width, bool sameDirection = true)
        {
            Length = length;
            Width = width;
            Area = width * length;
            if (!sameDirection)
                Rotate();
        }

        void Rotate()
        {
            int buffer = Length;
            Length = Width;
            Width = buffer;
        }

        internal void Cut(bool allowRotation = false)
        {
            if (allowRotation)
                CutWithRotation();
            else
                CutWithOutRotation();
        }

        private void CutWithOutRotation()
        {
            Total =  (Width / OrderItem.Width) * (Length / OrderItem.Length);
        }

        private void CutWithRotation()
        {
            //check dictionary
            CutWithOutRotation();
            if (IsWastedSmallerOrderItemArea())
                return;
            Rotate();
                 CutWithOutRotation();
            
            int totalfoeWidthCut = totalfoeWidthCut();

            //add to dictinary
        }

        bool IsWastedSmallerOrderItemArea()
        {
            return (Area - (Total * OrderItemArea)) < OrderItemArea;
        }
    }
}
