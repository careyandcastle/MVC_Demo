using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Demo2.Models;
using MVC_Demo2.Models.MvcDemoModel;

namespace MVC_Demo2.Controllers
{
    public class UserController : Controller
    {
        private readonly MvcDemoContext _context;

        public UserController(MvcDemoContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()  //0527 09:36 
        {
            var mvcDemoContext = _context.承租人檔; 
            //_context.承租人檔.Include(承 => 承.身分別編號Navigation);
            return View(await mvcDemoContext.ToListAsync());
            //D:\每日資料\20250523_工作日\MVC\MVC_Demo2\MVC_Demo2\Views\User\Index.cshtml
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var 承租人檔 = await _context.承租人檔
                .Include(承 => 承.身分別編號Navigation)
                .FirstOrDefaultAsync(m => m.事業 == id);
            if (承租人檔 == null)
            {
                return NotFound();
            }

            return View(承租人檔);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            ViewData["身分別編號"] = new SelectList(_context.身分別檔, "身分別編號", "身分別編號");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("事業,單位,部門,分部,承租人編號,承租人,身分別編號,統一編號,電話,行動電話,傳真,eMail,地址,發票寄送地址,銀行帳號,備註,發票載具,刪除註記,修改人,修改時間")] 承租人檔 承租人檔)
        {
            if (ModelState.IsValid)
            {
                _context.Add(承租人檔);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["身分別編號"] = new SelectList(_context.身分別檔, "身分別編號", "身分別編號", 承租人檔.身分別編號);
            return View(承租人檔);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var 承租人檔 = await _context.承租人檔.FindAsync(id);
            if (承租人檔 == null)
            {
                return NotFound();
            }
            ViewData["身分別編號"] = new SelectList(_context.身分別檔, "身分別編號", "身分別編號", 承租人檔.身分別編號);
            return View(承租人檔);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("事業,單位,部門,分部,承租人編號,承租人,身分別編號,統一編號,電話,行動電話,傳真,eMail,地址,發票寄送地址,銀行帳號,備註,發票載具,刪除註記,修改人,修改時間")] 承租人檔 承租人檔)
        {
            if (id != 承租人檔.事業)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(承租人檔);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!承租人檔Exists(承租人檔.事業))
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
            ViewData["身分別編號"] = new SelectList(_context.身分別檔, "身分別編號", "身分別編號", 承租人檔.身分別編號);
            return View(承租人檔);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var 承租人檔 = await _context.承租人檔
                .Include(承 => 承.身分別編號Navigation)
                .FirstOrDefaultAsync(m => m.事業 == id);
            if (承租人檔 == null)
            {
                return NotFound();
            }

            return View(承租人檔);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var 承租人檔 = await _context.承租人檔.FindAsync(id);
            _context.承租人檔.Remove(承租人檔);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool 承租人檔Exists(string id)
        {
            return _context.承租人檔.Any(e => e.事業 == id);
        }
    }
}
