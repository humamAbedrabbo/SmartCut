using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SmartCut.Data;
using SmartCut.Models;

namespace SmartCut.Controllers
{
    public class StockItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StockItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StockItems.Include(s => s.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StockItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return View(stockItem);
        }

        // GET: StockItems/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: StockItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemType,CategoryId,Length,Width,Hardness,Weight,Gramage,IsAvailable,ShipmentNo,Notes")] StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", stockItem.CategoryId);
            return View(stockItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        List<StockItemImport> output = new List<StockItemImport>();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            output.Add(new StockItemImport
                            {
                                ItemType = int.Parse( worksheet.Cells[row, 1].Value.ToString().Trim() ),
                                CategoryId = int.Parse(worksheet.Cells[row, 2].Value.ToString().Trim()),
                                Length = Convert.ToInt32( float.Parse(worksheet.Cells[row, 3].Value.ToString().Trim()) * 10),
                                Width = Convert.ToInt32( float.Parse(worksheet.Cells[row, 4].Value.ToString().Trim()) * 10),
                                Hardness = int.Parse(worksheet.Cells[row, 5].Value.ToString().Trim()),
                                Weight = double.Parse(worksheet.Cells[row, 6].Value.ToString().Trim()),
                                Gramage = int.Parse(worksheet.Cells[row, 7].Value.ToString().Trim()),
                                IsAvailable = int.Parse(worksheet.Cells[row, 8].Value.ToString().Trim()),
                                ShipmentNo = worksheet.Cells[row, 9].Value.ToString().Trim(),
                                Notes = worksheet.Cells[row, 10].Value.ToString().Trim(),
                            });
                        }

                        if(output.Count > 0)
                        {
                            List<StockItem> items = output.Select(x => (StockItem)x).ToList();
                            await _context.StockItems.AddRangeAsync(items);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                //var data = System.IO.File.ReadAllBytes(filePath);
                //ImportFromExcel import = new ImportFromExcel();
                //import.LoadXlsx(data);
                ////first parameter it's the sheet number in the excel workbook
                ////second parameter it's the number of rows to skip at the start(we have an header in the file)
                //List<StockItemImport> output = import.ExcelToList<StockItemImport>(0, 1);


            }
            return RedirectToAction(nameof(Index));
        }

        // GET: StockItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems.FindAsync(id);
            if (stockItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", stockItem.CategoryId);
            return View(stockItem);
        }

        // POST: StockItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemType,CategoryId,Length,Width,Hardness,Weight,Gramage,IsAvailable,ShipmentNo,Notes")] StockItem stockItem)
        {
            if (id != stockItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockItemExists(stockItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", stockItem.CategoryId);
            return View(stockItem);
        }

        // GET: StockItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return View(stockItem);
        }

        // POST: StockItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockItem = await _context.StockItems.FindAsync(id);
            _context.StockItems.Remove(stockItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockItemExists(int id)
        {
            return _context.StockItems.Any(e => e.Id == id);
        }
    }
}
