using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCut.Models;
using SmartCut.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartCut.Services;

namespace SmartCut.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext context;

        public OrdersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ActionResult Details(Order order)
        {
            ViewBag.Categories = new SelectList(context.Categories, "Id", "Name");
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            Order order = new Order();
            ViewBag.Categories = new SelectList(context.Categories, "Id", "Name");
            return View(order);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order anOrder)
        {
            try
            {
                var filter = anOrder.Filter;
                var settings = context.Settings.FirstOrDefault();
                var gramagePercent = settings.GramageRangePercent / 100.0;
                anOrder.Items = context.StockItems
                    .Where(x =>
                        x.CategoryId == filter.CategoryId
                        && x.Gramage <= filter.Gramage * (1 + gramagePercent)
                        && x.Gramage >= filter.Gramage * (1 - gramagePercent)
                        && (filter.Available == 1 || x.IsAvailable == true)
                        && (!filter.ItemType.HasValue || x.ItemType == filter.ItemType.Value)
                    )
                    .Select(x => (StockItemViewModel)x)
                    .ToList();
                ;

                anOrder = OrderService.CheckOrder(anOrder, settings);
                 
                ;
                ViewBag.Categories = new SelectList(context.Categories, "Id", "Name");
                return View(nameof(Details), anOrder);
            }
            catch
            {
                return View();
            }
        }
    }
}