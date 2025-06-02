using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Demo2.Models;
using TscLibCore.BaseObject;

namespace MVC_Demo2.Controllers
{
    [ProcUseRang(ProcNo, ProcUseRang.Menu)]
    [TypeFilter(typeof(BaseActionFilter))]
    public class TR_01Controller : Controller
    {
        private readonly TRDBContext _context;
        private const string ProcNo = "TR_01";
        

        public TR_01Controller(TRDBContext context)
        {
            _context = context;
        }

        // GET: TR_01
        public async Task<IActionResult> Index()
        {
            return View(await _context.部門.ToListAsync());
        }

        // GET: TR_01/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var 部門 = await _context.部門
                .FirstOrDefaultAsync(m => m.單位 == id);
            if (部門 == null)
            {
                return NotFound();
            }

            return View(部門);
        }

        // GET: TR_01/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TR_01/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("單位,部門1,部門名稱,組織狀態,修改人,修改日期時間")] 部門 部門)
        {
            if (ModelState.IsValid)
            {
                _context.Add(部門);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(部門);
        }

        // GET: TR_01/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var 部門 = await _context.部門.FindAsync(id);
            if (部門 == null)
            {
                return NotFound();
            }
            return View(部門);
        }

        // POST: TR_01/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("單位,部門1,部門名稱,組織狀態,修改人,修改日期時間")] 部門 部門)
        {
            if (id != 部門.單位)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(部門);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!部門Exists(部門.單位))
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
            return View(部門);
        }

        // GET: TR_01/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var 部門 = await _context.部門
                .FirstOrDefaultAsync(m => m.單位 == id);
            if (部門 == null)
            {
                return NotFound();
            }

            return View(部門);
        }

        // POST: TR_01/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var 部門 = await _context.部門.FindAsync(id);
            _context.部門.Remove(部門);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool 部門Exists(string id)
        {
            return _context.部門.Any(e => e.單位 == id);
        }
    }
}
