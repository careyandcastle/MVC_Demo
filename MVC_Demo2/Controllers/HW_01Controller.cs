using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Demo2.Models;

namespace MVC_Demo2.Controllers
{
    public class HW_01Controller : Controller
    {
        private readonly TRDBContext _context;

        public HW_01Controller(TRDBContext context)
        {
            _context = context;
        }

        // GET: HW_01
        public async Task<IActionResult> Index()
        {
            var tRDBContext = _context.庫存盤點主檔.Include(庫 => 庫.倉庫基本檔).Include(庫 => 庫.單據別Navigation).Include(庫 => 庫.庫存異動狀態Navigation).Include(庫 => 庫.災害別Navigation).Include(庫 => 庫.盤點種類Navigation).Include(庫 => 庫.進銷存組織Navigation);
            return View(await tRDBContext.ToListAsync());
        }

        // GET: HW_01/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var 庫存盤點主檔 = await _context.庫存盤點主檔
                .Include(庫 => 庫.倉庫基本檔)
                .Include(庫 => 庫.單據別Navigation)
                .Include(庫 => 庫.庫存異動狀態Navigation)
                .Include(庫 => 庫.災害別Navigation)
                .Include(庫 => 庫.盤點種類Navigation)
                .Include(庫 => 庫.進銷存組織Navigation)
                .FirstOrDefaultAsync(m => m.進銷存組織 == id);
            if (庫存盤點主檔 == null)
            {
                return NotFound();
            }

            return View(庫存盤點主檔);
        }

        // GET: HW_01/Create
        public IActionResult Create()
        {
            ViewData["進銷存組織"] = new SelectList(_context.倉庫基本檔, "倉庫組織", "倉庫組織");
            ViewData["單據別"] = new SelectList(_context.單據別, "單據別1", "單據別1");
            ViewData["庫存異動狀態"] = new SelectList(_context.庫存異動狀態, "庫存異動狀態1", "庫存異動狀態1");
            ViewData["災害別"] = new SelectList(_context.災害別, "災害別1", "災害別1");
            ViewData["盤點種類"] = new SelectList(_context.盤點種類, "盤點種類1", "盤點種類1");
            ViewData["進銷存組織"] = new SelectList(_context.進銷存組織, "進銷存組織1", "進銷存組織1");
            return View();
        }

        // POST: HW_01/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("進銷存組織,單據別,日期,流水號,倉庫代號,盤點種類,災害別,盤點人,盤點日期,備註,盤點狀態,核准人,核准日期,庫存異動狀態,是否註記刪除,修改人,修改日期時間")] 庫存盤點主檔 庫存盤點主檔)
        {
            if (ModelState.IsValid)
            {
                _context.Add(庫存盤點主檔);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["進銷存組織"] = new SelectList(_context.倉庫基本檔, "倉庫組織", "倉庫組織", 庫存盤點主檔.進銷存組織);
            ViewData["單據別"] = new SelectList(_context.單據別, "單據別1", "單據別1", 庫存盤點主檔.單據別);
            ViewData["庫存異動狀態"] = new SelectList(_context.庫存異動狀態, "庫存異動狀態1", "庫存異動狀態1", 庫存盤點主檔.庫存異動狀態);
            ViewData["災害別"] = new SelectList(_context.災害別, "災害別1", "災害別1", 庫存盤點主檔.災害別);
            ViewData["盤點種類"] = new SelectList(_context.盤點種類, "盤點種類1", "盤點種類1", 庫存盤點主檔.盤點種類);
            ViewData["進銷存組織"] = new SelectList(_context.進銷存組織, "進銷存組織1", "進銷存組織1", 庫存盤點主檔.進銷存組織);
            return View(庫存盤點主檔);
        }

        // GET: HW_01/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var 庫存盤點主檔 = await _context.庫存盤點主檔.FindAsync(id);
            if (庫存盤點主檔 == null)
            {
                return NotFound();
            }
            ViewData["進銷存組織"] = new SelectList(_context.倉庫基本檔, "倉庫組織", "倉庫組織", 庫存盤點主檔.進銷存組織);
            ViewData["單據別"] = new SelectList(_context.單據別, "單據別1", "單據別1", 庫存盤點主檔.單據別);
            ViewData["庫存異動狀態"] = new SelectList(_context.庫存異動狀態, "庫存異動狀態1", "庫存異動狀態1", 庫存盤點主檔.庫存異動狀態);
            ViewData["災害別"] = new SelectList(_context.災害別, "災害別1", "災害別1", 庫存盤點主檔.災害別);
            ViewData["盤點種類"] = new SelectList(_context.盤點種類, "盤點種類1", "盤點種類1", 庫存盤點主檔.盤點種類);
            ViewData["進銷存組織"] = new SelectList(_context.進銷存組織, "進銷存組織1", "進銷存組織1", 庫存盤點主檔.進銷存組織);
            return View(庫存盤點主檔);
        }

        // POST: HW_01/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("進銷存組織,單據別,日期,流水號,倉庫代號,盤點種類,災害別,盤點人,盤點日期,備註,盤點狀態,核准人,核准日期,庫存異動狀態,是否註記刪除,修改人,修改日期時間")] 庫存盤點主檔 庫存盤點主檔)
        {
            if (id != 庫存盤點主檔.進銷存組織)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(庫存盤點主檔);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!庫存盤點主檔Exists(庫存盤點主檔.進銷存組織))
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
            ViewData["進銷存組織"] = new SelectList(_context.倉庫基本檔, "倉庫組織", "倉庫組織", 庫存盤點主檔.進銷存組織);
            ViewData["單據別"] = new SelectList(_context.單據別, "單據別1", "單據別1", 庫存盤點主檔.單據別);
            ViewData["庫存異動狀態"] = new SelectList(_context.庫存異動狀態, "庫存異動狀態1", "庫存異動狀態1", 庫存盤點主檔.庫存異動狀態);
            ViewData["災害別"] = new SelectList(_context.災害別, "災害別1", "災害別1", 庫存盤點主檔.災害別);
            ViewData["盤點種類"] = new SelectList(_context.盤點種類, "盤點種類1", "盤點種類1", 庫存盤點主檔.盤點種類);
            ViewData["進銷存組織"] = new SelectList(_context.進銷存組織, "進銷存組織1", "進銷存組織1", 庫存盤點主檔.進銷存組織);
            return View(庫存盤點主檔);
        }

        // GET: HW_01/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var 庫存盤點主檔 = await _context.庫存盤點主檔
                .Include(庫 => 庫.倉庫基本檔)
                .Include(庫 => 庫.單據別Navigation)
                .Include(庫 => 庫.庫存異動狀態Navigation)
                .Include(庫 => 庫.災害別Navigation)
                .Include(庫 => 庫.盤點種類Navigation)
                .Include(庫 => 庫.進銷存組織Navigation)
                .FirstOrDefaultAsync(m => m.進銷存組織 == id);
            if (庫存盤點主檔 == null)
            {
                return NotFound();
            }

            return View(庫存盤點主檔);
        }

        // POST: HW_01/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var 庫存盤點主檔 = await _context.庫存盤點主檔.FindAsync(id);
            _context.庫存盤點主檔.Remove(庫存盤點主檔);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool 庫存盤點主檔Exists(string id)
        {
            return _context.庫存盤點主檔.Any(e => e.進銷存組織 == id);
        }
    }
}
