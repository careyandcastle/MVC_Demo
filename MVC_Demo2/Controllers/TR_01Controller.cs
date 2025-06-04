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

        private static IConfigurationProvider _config;  // <-新增
        private static IMapper _mapper;                 // <-新增


        public TR_01Controller(TRDBContext context)
        {
            _context = context;

            // 建立MapperConfiguration
            _config ??= new MapperConfiguration(cfg =>
            {
                // 定義部門的mapping

                // 這個mapping將"部門"轉為"TR_01_部門BasicViewModel"
                cfg.CreateMap<部門, TR_01_部門BasicViewModel>()
                 .ForMember(dest => dest.部門, opts => opts.MapFrom(src => src.部門1));

                // 這個mapping將"TR_01_部門BasicViewModel"轉回"部門"
                cfg.CreateMap<TR_01_部門BasicViewModel, 部門>()
                .ForMember(dest => dest.部門1, opts => opts.MapFrom(src => src.部門));

                cfg.CreateMap<部門, TR_01_部門CreateViewModel>()
                .IncludeBase<部門, TR_01_部門BasicViewModel>();

                cfg.CreateMap<TR_01_部門CreateViewModel, 部門>()
                 .IncludeBase<TR_01_部門BasicViewModel, 部門>();

                cfg.CreateMap<部門, TR_01_部門EditViewModel>()
                  .IncludeBase<部門, TR_01_部門BasicViewModel>();

                cfg.CreateMap<TR_01_部門EditViewModel, 部門>()
                  .IncludeBase<TR_01_部門BasicViewModel, 部門>();

                // 定義分部的mapping

            });
            // 建立Mapper
            _mapper = _config.CreateMapper();
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
            var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));
 
            var 單位選項 = await _context.單位 //<-新增
                //限縮單位資料範圍
                .Where(s => ua.DataRang > 1? s.單位1 == ua.DepartmentNo : true && s.組織狀態)
                .Select(s => new SelectListItem
                {
                    //Text = s.單位1 + "_" + s.單位名稱,
                    Text = HttpUtility.HtmlEncode(s.單位1 + "_" + s.單位名稱),
                    Value = HttpUtility.HtmlEncode(s.單位1),
                }).ToListAsync();
 
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
            await ValidateForCreateAsync(postData); //<-取消註解

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
                message = "發生未知錯誤，請聯絡管理員"
            });
        }
        /* 以Create舉例 */
        private async Task ValidateForCreateAsync(TR_01_部門CreateViewModel postData)
        {
            // 講師: UserAccountForSession ua 是我自己加的，實際上不需要

            // 執行各種驗證，例如驗證資料是否已存在於DB中
            (bool result, string errorMsg) = await 驗證部門是否重複Asyn(postData);

            // 如果驗證失敗，將錯誤訊息加入ModelState
            if (!result)
            {
                //ModelState.AddModelError("欄位名稱", errorMsg);
                ModelState.AddModelError("部門", errorMsg);
            }
        }

        private async Task<(bool result, string errorMsg)> 驗證部門是否重複Asyn(TR_01_部門CreateViewModel postData)
        {
            if (await _context.部門.AnyAsync(
                s => s.單位 == postData.單位
                && s.部門1 == postData.部門))
                return (false, "部門已存在");
            return (true, "");
        }

        


        // GET: TR_01/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var 部門 = await _context.部門.FindAsync(id);
        //    if (部門 == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(部門);
        //}

        public async Task<IActionResult> Edit(string 單位, string 部門)
        {
            // 檢查參數
            if (單位 == null || 部門 == null)
            {
                return NotFound();
            }

            // 查詢資料
            var model = await _context.部門
                .Include(s => s.單位Navigation)
                .Where(s => s.單位 == 單位 && s.部門1 == 部門)
                .SingleOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }

            // 使用AutoMapper將Model轉為ViewModel
            var viewModel = _mapper.Map<部門, TR_01_部門EditViewModel>(model);

            ViewBag.單位名稱 = model.單位 + "_" + model.單位Navigation.單位名稱;
 
            return PartialView(viewModel);
        }

        // POST: TR_01/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Update)]
        public async Task<IActionResult> Edit([Bind("單位,部門,部門名稱,組織狀態")] TR_01_部門EditViewModel postData)
        {
            // 檢查Model驗證
            if (!ModelState.IsValid)
            {
                return Ok(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
                {
                    data = ModelState.ToErrorInfos()
                });
            }

            try
            {

                // 驗證
                await ValidateForEdit(postData);

                if (!ModelState.IsValid)
                {
                    return Ok(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
                    {
                        data = ModelState.ToErrorInfos()
                    });
                }

                // 得到要編輯的資料
                var model = _mapper.Map<TR_01_部門EditViewModel, 部門>(postData);

                if (model == null)
                {
                    return NotFound();
                }

                //取得ua
                var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));

                // 設定修改人與修改時間
                model.修改人 = ua.UserNo;
                model.修改日期時間 = DateTime.Now;

                // 告訴EF Core 我們要更新這筆資料
                _context.部門.Update(model);

                // 實際執行，並得到受影響的資料筆數(opCount)
                int opCount = await _context.SaveChangesAsync();

                if (opCount > 0)
                {
                    return Ok(new ReturnData(ReturnState.ReturnCode.OK)
                    {
                        data = postData
                    });
                }
            }
            catch (Exception ex)
            {
                // 處理例外
                Exception realEx = ex.GetOriginalException();

                return CreatedAtAction(nameof(Edit), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
                {
                    message = realEx.ToMeaningfulMessage()
                });
            }

            // 如果opCount <= 0 的話，就會回傳這個錯誤。
            return CreatedAtAction(nameof(Edit), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
            {
                message = "發生未知錯誤，請聯絡管理員"
            });
        }
        // GET: TR_01/Delete/5

        /* 以Create舉例 */
        private async Task ValidateForEdit(TR_01_部門EditViewModel postData)
        {
            // 執行各種驗證，例如驗證資料是否已存在於DB中
            (bool result, string errorMsg) = await Edit驗證部門是否撤除Asyn(postData);

            // 如果驗證失敗，將錯誤訊息加入ModelState
            if (!result)
            {
                ModelState.AddModelError("部門", errorMsg);
                //討論到這部分與showvalidateErrorMessage有關係
            }
        }

        //0604
        private async Task<(bool result, string errorMsg)> Edit驗證部門是否撤除Asyn(TR_01_部門EditViewModel postData)
        {

            if (await _context.部門.AnyAsync(
           s => s.單位 == postData.單位
           && s.部門1 == postData.部門
           && s.組織狀態 == false))
            {
                return (false, "部門已撤除");
            }
            return (true, "");
        }
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
                        /* join 其他需要的 table */
                        //join dep in _context.單位                                               //註解掉這個
                        //on s.單位 equals dep.單位1                                              //註解掉這個

                        .Include(s => s.單位Navigation)
                        join m in _context.修改人 on s.修改人 equals m.修改人1 into mleftjoin   //註解掉這個
                        from _m in mleftjoin.DefaultIfEmpty()                                   //註解掉這個



                        //        /* left join 修改人 table 以取得修改人的姓名 */
                        //join m in _context.修改人 on s.修改人 equals m.修改人1 into mleftjoin
                        //        from _m in mleftjoin.DefaultIfEmpty()

                        // select 需要的欄位
                        // 這裡使用`部門`資料表舉例
                    select new TR_01_部門DisplayViewModel
                    {
                        單位 = s.單位,
                        //單位顯示 = CustomSqlFunctions.ConcatCodeAndName(s.單位, dep.單位名稱),
                        單位顯示 = CustomSqlFunctions.ConcatCodeAndName(s.單位, s.單位Navigation.單位名稱),
                        //單位顯示 = s.單位 + "_" + dep.單位名稱,
                        部門 = s.部門1,
                        部門名稱 = s.部門名稱,
                        組織狀態 = s.組織狀態,
                        組織狀態顯示 = s.組織狀態 ? "是" : "否", //新增這個
                        //修改人 = s.修改人,
                        修改人 = CustomSqlFunctions.ConcatCodeAndName(s.修改人, CustomSqlFunctions.DecryptToString(_m.姓名)),
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
