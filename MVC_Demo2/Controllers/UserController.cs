using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Demo2.Models;
using MVC_Demo2.Models.MvcDemoModel;
using MVC_Demo2.Models.ViewModel;

namespace MVC_Demo2.Controllers
{
    public class UserController : Controller
    {
        private readonly MvcDemoContext _context;
        // D:\每日資料\20250523_工作日\MVC\MVC_Demo2\MVC_Demo2\Models\MvcDemoContext.cs @@@6

        public UserController(MvcDemoContext context)
        {
            _context = context;
        }
        //private static string SafeDecryptStatic11(MvcDemoContext context, byte[] encrypted)
        //{
        //    if (encrypted == null || encrypted.Length == 0)
        //        return "(未填)";
        //    var decrypted = context.DecryptByKey(encrypted);
        //    return decrypted != null ? Encoding.Unicode.GetString(decrypted) : "(解密失敗)";
        //}

        // GET: User
        public async Task<IActionResult> Index()  //0527 09:36 
        {
            //var mvcDemoContext = _context.承租人檔; // 09:00
            // 09:15 _context.承租人檔.Include(承 => 承.身分別編號Navigation);
            //var mvcDemoContext = _context.承租人檔.Select(s => new承租人VM{ }); // 10:17 講解到一半
            //var mvcDemoContext = _context.承租人檔.Select(s => s.單位); // 10:20 講解到一半

            //講解，簡單來講，就是把承租人檔的item 都拋到承租人VM，
            //var mvcDemoContext = _context.承租人檔.Select(s => new { 
            //    單位 = s.單位, 
            //    //單位1 = s.單位, //左邊隨便取名，主要就是在承租人VM那邊要用這些命名
            //    事業 = s.事業, 
            //    單位 = s.單位, 
            //    單位 = s.單位,
            //    單位 = s.單位,
            //    單位 = s.單位            
            //}); // 10:20

            _context.OpenSymmetricKey = true; // 0527 11:35 // 講義55頁 使用Decrypt前，要先openSymmetricKey，否則金鑰未開時使用 DECRYPTBYKEY()，只會回傳 NULL (造成我必須使用 @@@8 來解決)
            var mvcDemoContext = _context.承租人檔.Select(s => new 承租人VM{
                單位 = s.單位,
                //單位1 = s.單位, //左邊不能隨便取名，主要就是在承租人VM那邊只有"單位"，沒有"單位1"欄位
                //頁54 Decrypt
                事業 = s.事業,
                部門 = s.部門,
                分部 = s.分部,
                //承租人姓名 = Encoding.Unicode.GetString(_context.DecryptByKey(s.承租人)),
                承租人姓名 = (s.承租人 != null && _context.DecryptByKey(s.承租人) != null) // 0527 11:50實驗 @@@8
                ? Encoding.Unicode.GetString(_context.DecryptByKey(s.承租人))
                : "(OpenSymmeticKey忘記開關囉)",

                身分別編號 = s.身分別編號 //隨便挑，來自 D:\每日資料\20250523_工作日\MVC\MVC_Demo2\MVC_Demo2\Models\MvcDemoModel\承租人檔.cs
            }); //使用system.text
                
            
            //}); // 10:20
            //return View(mvcDemoContext); // 0527 11:50實驗


            var result = await mvcDemoContext.ToListAsync(); // 0527 11:35
            _context.OpenSymmetricKey = false; // 0527 11:35
            return View(result); // 0527 11:35
            //D:\每日資料\20250523_工作日\MVC\MVC_Demo2\MVC_Demo2\Views\User\Index.cshtml
            //D:\SVN\TscLibCore\BaseObject\BaseDbContext.cs 這是解碼相關的
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
            ViewData["身分別編號"] = new SelectList(_context.身分別檔, "身分別編號", "身分別"); //0527 "身分別"
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("事業,單位,部門,分部,承租人編號,承租人,身分別編號,")] 承租人VM 承租人檔)
        {
            if (ModelState.IsValid) //Server端檢查 modelState，看誰錯 ResultView
            {
                


                //加密承租人中文姓名轉 bytes
                var dbKeyName = "WuYeahSymmKey";
                承租人檔.承租人 = _context.承租人檔.Select(x => _context.EncryptByKey(_context.Key_Guid(dbKeyName), 承租人檔.承租人姓名)
                ).FirstOrDefault();

                //承租人檔.統一編號 = new byte[5] { 1,2,3,4,5 };

                _context.Add(承租人檔);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            //ViewData["身分別編號"] = new SelectList(_context.身分別檔, "身分別編號", "身分別編號", 承租人檔.身分別編號);
            ViewData["身分別編號"] = new SelectList(_context.身分別檔, "身分別編號", "身分別", 承租人檔.身分別編號);
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
