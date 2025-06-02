using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Demo2.Models;
using MVC_Demo2.Models.ViewModel;
using TscLibCore.BaseObject;
using TscLibCore.Commons;

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
            //return View(await _context.部門.ToListAsync());
            ViewBag.TableFieldDescDict = new CreateTableFieldsDescription()
            .Create<TR_01_部門DisplayViewModel, TR_01_分部DisplayViewModel>();

            return View();
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

        [HttpPost, ActionName("GetDataPost")]
        [ValidateAntiForgeryToken]
        [NeglectActionFilter]
        public async Task<IActionResult> GetData([FromBody] QueryConditions qc)
        {
            // 取得資料查詢基底
            IQueryable<TR_01_部門DisplayViewModel> sql = GetBaseQuery();

            // 執行查詢語法並分頁
            PaginatedList<TR_01_部門DisplayViewModel> queryedData
                = await PaginatedList<TR_01_部門DisplayViewModel>.CreateAsync(sql, qc);

            return Ok(new
            {
                //📦 把查出來的資料（data）與總筆數（total）用 JSON 格式回傳給前端。
                data = queryedData,
                total = queryedData.TotalCount
            });
        }

        private IQueryable<TR_01_部門DisplayViewModel> GetBaseQuery()
        {
            return (from s in _context.部門
            //            /* join 其他需要的 table */
                    join dep in _context.單位//新增這個
                    on s.單位 equals dep.單位1//新增這個
                                          //        /* left join 修改人 table 以取得修改人的姓名 */
                                          //join m in _context.修改人 on s.修改人 equals m.修改人1 into mleftjoin
                                          //        from _m in mleftjoin.DefaultIfEmpty()

                    // select 需要的欄位
                    // 這裡使用`部門`資料表舉例
                    select new TR_01_部門DisplayViewModel
                    {
                        單位 = s.單位,
                        單位顯示 = s.單位 + "_" + dep.單位名稱, 
                        部門 = s.部門1,
                        部門名稱 = s.部門名稱,
                        組織狀態 = s.組織狀態,
                        組織狀態顯示 = s.組織狀態 ? "是" : "否", //新增這個
                        修改人 = s.修改人,
                        修改日期時間 = s.修改日期時間,

                        // 顯示最近一次修改資訊
                        //修改人 = CustomSqlFunctions.ConcatCodeAndName(s.修改人, CustomSqlFunctions.DecryptToString(_m.姓名)),
                        //修改日期時間 = s.修改日期時間
                    }
                   ).AsNoTracking(); // 使用AsNoTracking()提升效能
        }


        private bool 部門Exists(string id)
        {
            return _context.部門.Any(e => e.單位 == id);
        }
    }
}
