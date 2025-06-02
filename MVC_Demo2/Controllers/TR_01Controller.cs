using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Demo2.Models;
using MVC_Demo2.Models.ViewModel;
using MVC_Demo2.PC;
using TscLibCore.Authority;
using TscLibCore.BaseObject;
using TscLibCore.Commons;
using TscLibCore.Modules;

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
        public async Task<IActionResult> Create()
        {
            var 單位選項 = await _context.單位 //<-新增
            //ViewBag.單位選項 = await _context.單位 //<-註解掉
                .Where(s => s.組織狀態)
                .Select(s => new SelectListItem
            {
                //Text = s.單位1 + "_" + s.單位名稱,
                Text = HttpUtility.HtmlEncode(s.單位1 + "_" + s.單位名稱),
                Value = HttpUtility.HtmlEncode(s.單位1),
            }).ToListAsync();
            //return View();


            //方法一
            //單位選項 = 單位選項.Prepend(new SelectListItem
            //{ //<-新增
            //    Text = "--請選擇--", //<-新增
            //    Value = ""//<-新增
            //}).ToList();//<-新增
            //ViewBag.單位選項 = 單位選項;  //<-新增

            //方法二
            單位選項.Insert(0, new SelectListItem
            {
                Text = "--請選擇--", //<-新增
                Value = ""//<-新增
            });
            ViewBag.單位選項 = 單位選項;  //<-新增

            return PartialView();
        }

        // POST: TR_01/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [ProcUseRang(ProcNo, ProcUseRang.Add)] //新增
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("單位,部門1,部門名稱,組織狀態,修改人,修改日期時間")] 部門 部門)
        public async Task<IActionResult> Create([Bind("單位,部門,部門名稱")] TR_01_部門CreateViewModel postData)
        {
            // 檢查Model驗證結果。如果有任何驗證未通過，則回傳錯誤。
            // 請使用 early return 來回傳錯誤!
            if (!ModelState.IsValid)
            {
                //這裡的Ok是HTTP 200 Ok的意思
                return Ok(new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
                {
                    data = ModelState.ToErrorInfos()
                });
            }

            // 後端驗證 <-- 還沒有實作，要自己寫! (例如，重複PK插入要擋掉)
            //await ValidateForCreateAsync(postData);

            // 檢查後端驗證結果。如果有任何驗證未通過，則回傳錯誤。
            if (!ModelState.IsValid)
            {
                return Ok(new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
                {
                    data = ModelState.ToErrorInfos()
                });
            }

            try
            {
                // 使用AutoMapper將ViewModel轉為Model(因為DataBase只吃Model!! 他挑食!!)
                部門 model = _mapper.Map<TR_01_部門CreateViewModel, 部門>(postData);

                // 取得ua。這段語法有點長，可以寫成一個function，直接用也沒關係。
                // 在實務上，專案中會有其他語法用來取得ua。
                var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));

                // 設定修改人與修改時間
                model.修改人 = ua.UserNo;
                model.修改日期時間 = DateTime.Now;

                //(驗證過了，修改人也給了，我們就真正將資料加入model裡面)
                //
                // 告訴 EF Core 我們要新增這筆資料
                //"_context.Add(model);" 也會work，但指定資料表名稱會比較嚴謹。*/
                _context.部門.Add(model);


                // 執行到這裡時，EF Core才會真的將上面的語法轉化為sql，並實際執行Insert，並得到受影響的資料筆數(opCount)
                //上面的內容，真正變成sql語句了!

                var opCount = await _context.SaveChangesAsync();

                // (integer大於0，代表成功執行sql了!)
                if (opCount > 0)
                {
                    // 回傳成功結果與新增的資料
                    return Ok(new ReturnData(ReturnState.ReturnCode.OK)
                    {
                        // 回傳新增的資料，作為newItem顯示在主表格的最上面
                        data = await GetBaseQuery()
                              .Where(s => s.單位 == model.單位 && s.部門 == model.部門1)
                              .SingleOrDefaultAsync()
                        //右側為displayViewModel
                    });
                }
            }
            //如果integer小於等於0，就會進catch，表示錯誤
            catch (Exception ex)
            {
                // 處理例外
                Exception realEx = ex.GetOriginalException();

                return CreatedAtAction(nameof(Create), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
                {
                    message = realEx.ToMeaningfulMessage()
                });
            }

            return CreatedAtAction(nameof(Create), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
            {
                message = StringConsts.DATA_NOT_EXIST
            });
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
