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
        int Length, Width, Area;
        public int Total = 0;
        public double LossPercent = 100;
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

        void Rotate()
        {
            int buffer = Length;
            Length = Width;
            Width = buffer;
        }

        internal void Cut(bool allowRotation)
        {
            if (Area < OrderItem.Area || Width < OrderItem.SmallerRib || Length < OrderItem.SmallerRib)
                return;
            if (allowRotation)
                CutWithRotation();
            else
                CutWithOutRotation();
            if (Total != 0)
                LossPercent = (100 * Total * OrderItem.Area) / Area;
        }

        private void CutWithOutRotation()
        {
            Total =  (Width / OrderItem.Width) * (Length / OrderItem.Length);
        }

        private void CutWithRotation()
        {
            if (FindinDictionary())
                return;
            if (!IsGoodResult())
            {
                Rotate();
                if (!IsGoodResult())
                {
                    int total1 = Split();
                    Rotate();
                    int total2 = Split();
                    if (total1 > total2)
                        Total = total1;
                    else
                        Total = total2;
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
            CutWithOutRotation();
            return (Area - (Total * OrderItem.Area)) < OrderItem.Area;
        }

        private int Split()
        {
            int t = 0;
            if (Width > OrderItem.Width)
            {
                Sheet sheet = new Sheet(Length, Width - OrderItem.Width);
                sheet.Cut(true);
                t = sheet.Total;
                sheet = new Sheet(Length, OrderItem.Width);
                sheet.Cut(true);
                t += sheet.Total;
            }
            return t;
        }

        private void AddtoDictinary()
        {
            CalculatedSheetdictinary.TryAdd((Length,Width),Total);
        }
    }
}
