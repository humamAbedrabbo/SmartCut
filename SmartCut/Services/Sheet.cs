using SmartCut.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCut.Services
{
    public class Sheet 
    {
        public int Length, Width;
        public readonly int Area;
        public int Total = 0;
        public static OrderItem OrderItem;
        public static ConcurrentDictionary<(int,int), int> CalculatedSheetdictinary;

        public Sheet(int length, int width, bool sameDirection = true)
        {
            Length = length;
            Width = width;
            Area = width * length;
            if (!sameDirection)
                Rotate();
        }

        public void Rotate()
        {
            int buffer = Length;
            Length = Width;
            Width = buffer;
        }

        public void Cut()
        {
            if (Area < OrderItem.Area || Width < OrderItem.SmallerRib || Length < OrderItem.SmallerRib)
                return;
            if (OrderItem.CanRotate)
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
            if (FindinDictionary())
                return;
            CutWithOutRotation();
            if (!IsGoodResult())
            {
                Rotate();
                CutWithOutRotation();
                if (!IsGoodResult())
                {
                    Split();
                    if (!IsGoodResult())
                    {
                        int total1 = Total;
                        Rotate();
                        Split();
                        if (Total < total1)
                            Total = total1;
                    }
                }
            }
            AddtoDictinary();
        }

        private bool FindinDictionary()
        {
            if (CalculatedSheetdictinary.TryGetValue((Length, Width), out Total))
                return true;
            if (CalculatedSheetdictinary.TryGetValue((Width, Length), out Total))
                return true;
            return false;
        }

        private bool IsGoodResult()
        {
            return (Area - (Total * OrderItem.Area)) < OrderItem.Area;
        }

        private void Split()
        {
            if (Width > OrderItem.Width)
            {
                Sheet sheet = new Sheet(Length, Width - OrderItem.Width);
                sheet.Cut();
                Total = sheet.Total;
                sheet = new Sheet(Length, OrderItem.Width);
                sheet.Cut();
                Total += sheet.Total;
            }
            else
                Total = 0;
            //if (Width == OrderItem.Width) should be handled before
        }

        public double UsefullPercent()
        {
            return Convert.ToDouble(Total * OrderItem.Area) / Convert.ToDouble(Area);
        }

        private void AddtoDictinary()
        {
            CalculatedSheetdictinary.TryAdd((Length,Width),Total);
        }
    }
}
