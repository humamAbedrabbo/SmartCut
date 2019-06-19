using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartCut.Data;
using SmartCut.Models;

namespace SmartCut.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/Edit/5
        public async Task<IActionResult> Edit(int? id = 1)
        {
            var settingModel = await _context.Settings.FindAsync(id);
            if (settingModel == null)
            {
                return NotFound();
            }
            return View(settingModel);
        }

        // POST: Settings/Edit/1
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaximumCuttingLengthInCm,GramageRangePercent")] SettingModel settingModel)
        {
            if (id != settingModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(settingModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingModelExists(settingModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(settingModel);
        }

       
        private bool SettingModelExists(int id)
        {
            return _context.Settings.Any(e => e.Id == id);
        }
    }
}
