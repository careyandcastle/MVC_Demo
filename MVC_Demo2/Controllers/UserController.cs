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
                //承租人姓名 = (s.承租人 != null && _context.DecryptByKey(s.承租人) != null) // 0527 11:50實驗 @@@8
                //? Encoding.Unicode.GetString(_context.DecryptByKey(s.承租人))
                //: "(OpenSymmeticKey忘記開關囉)",

                承租人姓名 = s.承租人 == null ? "假名" :  
                Encoding.Unicode.GetString(_context.DecryptByKey(s.承租人)), // 14:20

                身分別編號 = s.身分別編號, //隨便挑，來自 D:\每日資料\20250523_工作日\MVC\MVC_Demo2\MVC_Demo2\Models\MvcDemoModel\承租人檔.cs
                承租人編號 = s.承租人編號 //隨便挑，來自 D:\每日資料\20250523_工作日\MVC\MVC_Demo2\MVC_Demo2\Models\MvcDemoModel\承租人檔.cs
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
        //public async Task<IActionResult> Create([Bind("事業,單位,部門,分部,承租人編號,承租人,身分別編號,")] 承租人VM 承租人檔)  //0527 14:36 錯誤版本
        //{
        //    if (ModelState.IsValid) //Server端檢查 modelState，看誰錯 ResultView
        //    {
        //        //加密承租人中文姓名轉 bytes
        //        var dbKeyName = "WuYeahSymmKey";
        //        _context.OpenSymmetricKey = true;
        //        承租人檔.承租人 = _context.承租人檔.Select(x => _context.EncryptByKey(_context.Key_Guid(dbKeyName), 承租人檔.承租人姓名)
        //        ).FirstOrDefault();

        //        承租人檔.統一編號 = new byte[] { 1,2,3,4,5 };
        //        承租人檔.刪除註記 = false;
        //        承租人檔.修改人 = "08844";
        //        承租人檔.修改時間 = DateTime.Now;

        //        _context.Add(承租人檔);
        //        await _context.SaveChangesAsync();

        //        _context.OpenSymmetricKey = false;

        //        return RedirectToAction(nameof(Index));//你可以住轉跳到很多action，但有時候可能會多打個空白，所以打nameof()能夠檢查，如果找不到本檔案裡的function(也就是Index)，他會是灰色警告

        //    }
        //    //ViewData["身分別編號"] = new SelectList(_context.身分別檔, "身分別編號", "身分別編號", 承租人檔.身分別編號);
        //    ViewData["身分別編號"] = new SelectList(_context.身分別檔, "身分別編號", "身分別", 承租人檔.身分別編號);
        //    return View(承租人檔);
        //}

        public async Task<IActionResult> Create([Bind("事業,單位,部門,分部,承租人編號,承租人姓名,身分別編號")] 承租人VM 承租人檔) //0527 14:36 正確版本，來自僑偉
        {
            if (ModelState.IsValid)
            {
                //_context.Add(承租人檔);
                //await _context.SaveChangesAsync();
                _context.OpenSymmetricKey = true;
                var dbKeyName = "WuYeahSymmKey";
                承租人檔.承租人 = _context.承租人檔.Select(s =>
                                    _context.EncryptByKey(_context.Key_Guid(dbKeyName), 承租人檔.承租人姓名)).FirstOrDefault();

                承租人檔.統一編號 = new byte[] { 1, 2, 3, 4, 5 };
                承租人檔.刪除註記 = false;
                承租人檔.修改人 = "08844";
                承租人檔.修改時間 = DateTime.Now;

                _context.Add(承租人檔);
                await _context.SaveChangesAsync();
                _context.OpenSymmetricKey = false;

                return RedirectToAction(nameof(Index));
            }
            ViewData["身分別編號"] = new SelectList(_context.身分別檔, "身分別編號", "身分別編號", 承租人檔.身分別編號);
            return View(承租人檔);
        }

        // GET: User/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var 承租人檔 = await _context.承租人檔.FindAsync(id);
        //    if (承租人檔 == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["身分別編號"] = new SelectList(_context.身分別檔, "身分別編號", "身分別編號", 承租人檔.身分別編號);
        //    return View(承租人檔);
        //}

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        public async Task<IActionResult> Edit(
            string 事業, string 單位, string 部門, string 分部,
            string 承租人編號)
        {

            if (單位 == null || 部門 == null)
            {
                return NotFound();
            }

            //讀DB資料
            var 承租人檔 = await _context.承租人檔.FindAsync(事業, 單位, 部門, 分部, 承租人編號);
            if (承租人檔 == null)
            {
                return NotFound();
            }
            ViewData["身分別編號"] = new SelectList(_context.身分別檔, "身分別編號", "身分別編號", 承租人檔.身分別編號);

            //讀出來的資料轉JSON 字串
            var 承租人檔JSON = System.Text.Json.JsonSerializer.Serialize(承租人檔);

            //JSON字串轉物件
            var 承租人檔VM = System.Text.Json.JsonSerializer.Deserialize<承租人VM>(承租人檔JSON);

            //承租人Byte Array解密轉中文
            _context.OpenSymmetricKey = true;
            承租人檔VM.承租人姓名 = _context.承租人檔.Select(s => Encoding.Unicode.GetString(
                    _context.DecryptByKey(承租人檔.承租人))).FirstOrDefault();
            _context.OpenSymmetricKey = false;

            //將承租人檔VM送給client
            return View(承租人檔VM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("事業,單位,部門,分部,承租人編號,身分別編號,承租人姓名")] 承租人VM User送來的承租人檔)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var 舊的承租人檔 = await _context.承租人檔.FindAsync(User送來的承租人檔.事業, User送來的承租人檔.單位, User送來的承租人檔.部門, User送來的承租人檔.分部, User送來的承租人檔.承租人編號, User送來的承租人檔.承租人姓名);

                    //_context.OpenSymmetricKey = true;
                    //var dbKeyName = "WuYeahSymmKey";
                    //User送來的承租人檔.承租人 = _context.承租人檔.Select(s =>
                    //                _context.EncryptByKey(_context.Key_Guid(dbKeyName), User送來的承租人檔.承租人姓名)).FirstOrDefault();

                    //User送來的承租人檔.統一編號 = new byte[] { 1, 2, 3, 4, 5 };
                    //User送來的承租人檔.刪除註記 = false;
                    //User送來的承租人檔.修改人 = "08844";
                    //User送來的承租人檔.修改時間 = DateTime.Now;

                    //_context.Update(User送來的承租人檔);
                    //await _context.SaveChangesAsync();

                    //_context.OpenSymmetricKey = false;
                    //return RedirectToAction(nameof(Edit));
                    var 舊有的_承租人檔 = await _context.承租人檔.FindAsync(
                                         User送來的承租人檔.事業, User送來的承租人檔.單位,
                                         User送來的承租人檔.部門, User送來的承租人檔.分部,
                                         User送來的承租人檔.承租人編號); //0527 16:00 五個主鍵(想知道幾個主鍵，可以去Edit.cshtml看 哪個被hidden了)

                    _context.OpenSymmetricKey = true;
                    var dbKeyName = "WuYeahSymmKey";
                    舊有的_承租人檔.承租人 = _context.承租人檔.Select(s =>
                     _context.EncryptByKey(
                         _context.Key_Guid(dbKeyName), User送來的承租人檔.承租人姓名)
                     ).FirstOrDefault();

                    //User送來的承租人檔.統一編號 = new byte[] { 1, 2, 3, 4, 5 };
                    //User送來的承租人檔.刪除註記 = false;
                    //User送來的承租人檔.修改人 = "08844";
                    //User送來的承租人檔.修改時間 = DateTime.Now;

                    _context.Update(舊有的_承租人檔);
                    await _context.SaveChangesAsync();

                    _context.OpenSymmetricKey = false;
                    //return RedirectToAction(nameof(Edit));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!承租人檔Exists(User送來的承租人檔.事業))
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
            ViewData["身分別編號"] = new SelectList(_context.身分別檔, "身分別編號", "身分別編號", User送來的承租人檔.身分別編號);
            return View(User送來的承租人檔);
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
