using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class HW_01Controller : Controller
    {
        private readonly TRDBContext _context;
        private const string ProcNo = "HW_01";
        private static IConfigurationProvider _config;
        private static IMapper _mapper;

        public HW_01Controller(TRDBContext context)
        {
            _context = context;
            _config ??= new MapperConfiguration(cfg =>
            {
                // 主檔 Mapping（庫存盤點主檔）
                cfg.CreateMap<庫存盤點主檔, HW_01_庫存盤點主檔BasicViewModel>()
                    .ForMember(dest => dest.流水號, opt => opt.MapFrom(src => src.流水號));
                cfg.CreateMap<HW_01_庫存盤點主檔BasicViewModel, 庫存盤點主檔>();

                cfg.CreateMap<庫存盤點主檔, HW_01_庫存盤點主檔CreateViewModel>()
                    .IncludeBase<庫存盤點主檔, HW_01_庫存盤點主檔BasicViewModel>();
                cfg.CreateMap<HW_01_庫存盤點主檔CreateViewModel, 庫存盤點主檔>()
                    .IncludeBase<HW_01_庫存盤點主檔BasicViewModel, 庫存盤點主檔>();

                cfg.CreateMap<庫存盤點主檔, HW_01_庫存盤點主檔EditViewModel>()
                    .IncludeBase<庫存盤點主檔, HW_01_庫存盤點主檔BasicViewModel>();
                cfg.CreateMap<HW_01_庫存盤點主檔EditViewModel, 庫存盤點主檔>()
                    .IncludeBase<HW_01_庫存盤點主檔BasicViewModel, 庫存盤點主檔>();


                // 明細 Mapping（庫存盤點明細檔）
                cfg.CreateMap<庫存盤點明細, HW_01_庫存盤點明細檔BasicViewModel>()
                    .ForMember(dest => dest.流水號, opt => opt.MapFrom(src => (int)src.流水號))
                    .ForMember(dest => dest.項次, opt => opt.MapFrom(src => (int)src.項次));

                cfg.CreateMap<HW_01_庫存盤點明細檔BasicViewModel, 庫存盤點明細>()
                    .ForMember(dest => dest.流水號, opt => opt.MapFrom(src => (decimal)src.流水號))
                    .ForMember(dest => dest.項次, opt => opt.MapFrom(src => (decimal)src.項次));

                // 明細 CreateViewModel
                cfg.CreateMap<庫存盤點明細, HW_01_庫存盤點明細檔CreateViewModel>()
                    .IncludeBase<庫存盤點明細, HW_01_庫存盤點明細檔BasicViewModel>();
                cfg.CreateMap<HW_01_庫存盤點明細檔CreateViewModel, 庫存盤點明細>()
                    .IncludeBase<HW_01_庫存盤點明細檔BasicViewModel, 庫存盤點明細>();

                // 明細 EditViewModel（若你有）
                cfg.CreateMap<庫存盤點明細, HW_01_庫存盤點明細檔EditViewModel>()
                    .IncludeBase<庫存盤點明細, HW_01_庫存盤點明細檔BasicViewModel>();
                cfg.CreateMap<HW_01_庫存盤點明細檔EditViewModel, 庫存盤點明細>()
                    .IncludeBase<HW_01_庫存盤點明細檔BasicViewModel, 庫存盤點明細>();


            });
            _mapper = _config.CreateMapper();
        }

        public IActionResult Index()
        {
            ViewBag.TableFieldDescDict = new CreateTableFieldsDescription()
                .Create<HW_01_庫存盤點主檔DisplayViewModel, HW_01_庫存盤點明細檔DisplayViewModel>();
            return View();
        }

        [HttpPost, ActionName("GetDataPost")]
        [ValidateAntiForgeryToken]
        [NeglectActionFilter]
        public async Task<IActionResult> GetData([FromBody] QueryConditions qc)
        {
            IQueryable<HW_01_庫存盤點主檔DisplayViewModel> sql = GetBaseQuery();
            PaginatedList<HW_01_庫存盤點主檔DisplayViewModel> queryedData =
                await PaginatedList<HW_01_庫存盤點主檔DisplayViewModel>.CreateAsync(sql, qc);

            return Ok(new
            {
                data = queryedData,
                total = queryedData.TotalCount
            });
        }

        private IQueryable<HW_01_庫存盤點主檔DisplayViewModel> GetBaseQuery()
        {
            return (from m in _context.庫存盤點主檔
                        .Include(m => m.倉庫基本檔)
                        .Include(m => m.盤點種類Navigation)
                        .Include(m => m.災害別Navigation)
                        .Include(m => m.庫存異動狀態Navigation)
                        .Include(m => m.單據別Navigation)
                        .Include(m => m.進銷存組織Navigation)
                    join u in _context.修改人 on m.修改人 equals u.修改人1 into ujoin
                    from _u in ujoin.DefaultIfEmpty()
                    select new HW_01_庫存盤點主檔DisplayViewModel
                    {
                        進銷存組織 = m.進銷存組織,
                        單據別 = m.單據別,
                        單據別名稱 = m.單據別 + "_" + m.單據別Navigation.單據別名稱,
                        日期 = m.日期,
                        //日期 = m.日期,
                        流水號 = m.流水號,
                        倉庫代號 = m.倉庫代號,
                        倉庫名稱 = m.倉庫基本檔.倉庫代號 + "_" + m.倉庫基本檔.倉庫組織,
                        盤點種類 = m.盤點種類,
                        盤點種類名稱 = m.盤點種類Navigation.盤點種類1,
                        災害別 = m.災害別,
                        災害別名稱 = m.災害別 + "_" + m.災害別Navigation.災害別名稱,
                        盤點人 = m.盤點人,
                        盤點人姓名 = m.盤點人 + "_" + CustomSqlFunctions.DecryptToString(_u.姓名), // <- 若有帳號表可加入
                        備註 = m.備註,
                        盤點日期 = m.盤點日期,
                        庫存異動狀態 = m.庫存異動狀態,
                        庫存異動狀態名稱 = m.庫存異動狀態Navigation.庫存異動狀態1,
                        是否註記刪除 = m.是否註記刪除, // 若有欄位再加
                        是否註記刪除顯示 = m.是否註記刪除 ? "是" : "否",
                        修改人 = CustomSqlFunctions.ConcatCodeAndName(m.修改人, CustomSqlFunctions.DecryptToString(_u.姓名)),
                        修改時間 = m.修改日期時間
                    }).AsNoTracking();
        }

        [NeglectActionFilter]
        public bool CanClickCreate(int index) => index % 2 == 0;

        public async Task<IActionResult> Create()
        {
            var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));

            // 取得列帳日（假設你已存在方法或變數）
            //DateTime 列帳日期 = await Get列帳日期Async(); // 可自行實作，也可以用 DateTime.Today;
            DateTime 列帳日期 = DateTime.Today; // 可自行實作，也可以用 DateTime.Today;

            var viewModel = new HW_01_庫存盤點主檔BasicViewModel
            {
                進銷存組織 = ua.BusinessNo,
                單據別 = "INV", // 固定 INV
                日期 = 列帳日期
            };

            // ===== 倉庫代號下拉選單 =====
            var 倉庫選項 = await _context.倉庫基本檔
                //.Where(s => s.是否暫停 == false && s.是否裁撤 == false)
                .Select(s => new SelectListItem
                {
                    Text = s.倉庫代號 + "_" + s.倉庫名稱,
                    Value = s.倉庫代號
                }).ToListAsync();
            倉庫選項.Insert(0, new SelectListItem { Text = "--請選擇--", Value = "" });
            ViewBag.倉庫選項 = 倉庫選項;

            // ===== 盤點種類下拉 =====
            var 盤點種類選項 = await _context.盤點種類
                .Select(s => new SelectListItem
                {
                    Text = s.盤點種類1 + "_" + s.盤點種類名稱,
                    Value = s.盤點種類1
                }).ToListAsync();
            盤點種類選項.Insert(0, new SelectListItem { Text = "--請選擇--", Value = "" });
            ViewBag.盤點種類選項 = 盤點種類選項;

            // ===== 災害別：預設為空，依盤點種類動態載入 =====
            ViewBag.災害別選項 = new List<SelectListItem> { new SelectListItem { Text = "--請先選擇盤點種類--", Value = "" } };

            // ===== 盤點人下拉：從修改人資料表轉換 byte[] 姓名 =====
            var 盤點人選項 = await _context.修改人
                .Select(s => new SelectListItem
                {
                    Text = s.修改人1 + "_" + System.Text.Encoding.UTF8.GetString(s.姓名),
                    Value = s.修改人1
                }).ToListAsync();
            盤點人選項.Insert(0, new SelectListItem { Text = "--請選擇--", Value = "" });
            ViewBag.盤點人選項 = 盤點人選項;

            return PartialView(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Add)]
        public async Task<IActionResult> Create([Bind("進銷存組織,日期,倉庫代號,盤點種類,災害別,盤點人,盤點日期,備註")] HW_01_庫存盤點主檔CreateViewModel postData)
        {
            if (!ModelState.IsValid)
                return Ok(new ReturnData(ReturnState.ReturnCode.CREATE_ERROR) { data = ModelState.ToErrorInfos() });

            await ValidateForCreate(postData);
            if (!ModelState.IsValid)
                return Ok(new ReturnData(ReturnState.ReturnCode.CREATE_ERROR) { data = ModelState.ToErrorInfos() });

            try
            {
                var model = _mapper.Map<庫存盤點主檔>(postData);
                model.單據別 = "INV";
                model.流水號 = await _context.庫存盤點主檔
                    .Where(x => x.進銷存組織 == model.進銷存組織 && x.單據別 == "INV" && x.日期 == model.日期)
                    .Select(x => x.流水號).DefaultIfEmpty(0).MaxAsync() + 1;

                var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));
                model.修改人 = ua.UserNo;
                model.修改日期時間 = DateTime.Now;

                _context.庫存盤點主檔.Add(model);
                int opCount = await _context.SaveChangesAsync();
                if (opCount > 0)
                {
                    return Ok(new ReturnData(ReturnState.ReturnCode.OK)
                    {
                        data = await GetBaseQuery().Where(x =>
                            x.進銷存組織 == model.進銷存組織 &&
                            //x.單據別名稱 == model.單據別 &&
                            x.單據別 == model.單據別 &&
                            x.日期 == model.日期 &&
                            x.流水號 == model.流水號
                        ).SingleOrDefaultAsync()
                    });
                }
            }
            catch (Exception ex)
            {
                return CreatedAtAction(nameof(Create), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
                {
                    message = ex.GetOriginalException().ToMeaningfulMessage()
                });
            }

            return CreatedAtAction(nameof(Create), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
            {
                message = "發生未知錯誤，請聯絡管理員"
            });
        }

        private async Task ValidateForCreate(HW_01_庫存盤點主檔CreateViewModel postData)
        {
            bool exists = await _context.庫存盤點主檔.AnyAsync(x =>
                x.進銷存組織 == postData.進銷存組織 &&
                x.單據別 == "INV" &&
                x.日期 == postData.日期 &&
                x.倉庫代號 == postData.倉庫代號);

            if (exists)
                ModelState.AddModelError("倉庫代號", "相同日期與倉庫的盤點紀錄已存在");
        }

        public async Task<IActionResult> Edit(string 進銷存組織, string 單據別, DateTime 日期, int 流水號)
        {
            if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 日期 == default || 流水號 == 0)
            {
                return NotFound();
            }

            // 讀取主檔資料
            var model = await _context.庫存盤點主檔
    .Include(x => x.倉庫基本檔)
    .Include(x => x.盤點種類Navigation)
    .Include(x => x.災害別Navigation)
    .Include(x => x.庫存異動狀態Navigation)
    .Include(x => x.進銷存組織Navigation)
    .Where(x =>
        x.進銷存組織 == 進銷存組織 &&
        x.單據別 == 單據別 &&
        x.日期 == 日期 &&
        x.流水號 == 流水號)
    .SingleOrDefaultAsync();


            if (model == null)
            {
                return NotFound();
            }

            // 使用 AutoMapper 映射到 EditViewModel
            var viewModel = _mapper.Map<庫存盤點主檔, HW_01_庫存盤點主檔EditViewModel>(model);

            // 預備下拉選單資料
            ViewBag.倉庫代號選項 = await _context.倉庫基本檔
                //.Where(w => w.是否裁撤 == false && w.是否暫停使用 == false)
                .OrderBy(o => o.倉庫代號)
                .Select(s => new SelectListItem
                {
                    Text = s.倉庫代號 + "_" + s.倉庫名稱,
                    Value = s.倉庫代號
                })
                .ToListAsync();

            ViewBag.盤點種類選項 = await _context.盤點種類
    .Where(w => !w.是否停用)
    .Select(s => new SelectListItem
    {
        Text = s.盤點種類1 + "_" + s.盤點種類名稱,
        Value = s.盤點種類1
    }).ToListAsync();


            var 選取的盤點種類值 = model.盤點種類;

            // 根據盤點種類，判斷是否需要災害別下拉
            var 是否災害盤點 = await _context.盤點種類
                .Where(x => x.盤點種類1 == 選取的盤點種類值)
                .Select(x => x.是否災害盤點)
                .FirstOrDefaultAsync();

            if (是否災害盤點)
            {
                // 載入災害別下拉選單
                var 災害別選項 = await _context.災害別
                    .Where(x => !x.是否停用)
                    .Select(x => new SelectListItem
                    {
                        Text = x.災害別1 + "_" + x.災害別名稱,
                        Value = x.災害別1
                    }).ToListAsync();

                災害別選項.Insert(0, new SelectListItem { Text = "--請選擇--", Value = "" });
                ViewBag.災害別選項 = 災害別選項;
            }
            else
            {
                ViewBag.災害別選項 = new List<SelectListItem> { new SelectListItem { Text = "(無)", Value = "" } };
            }

            return PartialView(viewModel);
        }

        [HttpGet]
        [ProcUseRang(ProcNo, ProcUseRang.Update)]
        public async Task<IActionResult> EditDetail(string 進銷存組織, string 單據別, DateTime 日期, decimal 流水號, decimal 項次)
        {
            if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 日期 == default || 流水號 == default || 項次 == default)
            {
                return NotFound();
            }

            var model = await _context.庫存盤點明細
                .Where(x => x.進銷存組織 == 進銷存組織 &&
                            x.單據別 == 單據別 &&
                            x.日期 == 日期 &&
                            x.流水號 == 流水號 &&
                            x.項次 == 項次)
                .SingleOrDefaultAsync();

            if (model == null)
                return NotFound();

            var viewModel = _mapper.Map<庫存盤點明細, HW_01_庫存盤點明細檔EditViewModel>(model);

            return PartialView(viewModel); // 對應 EditDetail.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Update)]
        public async Task<IActionResult> EditDetail([Bind("進銷存組織,單據別,日期,流水號,項次,商品編號,盤點數量")] HW_01_庫存盤點明細檔EditViewModel postData)
        {
            if (!ModelState.IsValid)
                return Ok(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR) { data = ModelState.ToErrorInfos() });

            try
            {
                var model = _mapper.Map<庫存盤點明細>(postData);
                var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));
                model.修改人 = ua.UserNo;
                model.修改日期時間 = DateTime.Now;

                _context.庫存盤點明細.Update(model);
                int opCount = await _context.SaveChangesAsync();

                if (opCount > 0)
                {
                    return Ok(new ReturnData(ReturnState.ReturnCode.OK)
                    {
                        data = await GetDetailBaseQuery()
                            .Where(x =>
                                x.進銷存組織 == model.進銷存組織 &&
                                x.單據別 == model.單據別 &&
                                x.日期 == model.日期 &&
                                x.流水號 == model.流水號 &&
                                x.項次 == model.項次)
                            .SingleOrDefaultAsync()
                    });
                }
            }
            catch (Exception ex)
            {
                return CreatedAtAction(nameof(EditDetail), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
                {
                    message = ex.GetOriginalException().ToMeaningfulMessage()
                });
            }

            return CreatedAtAction(nameof(EditDetail), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
            {
                message = "資料更新失敗"
            });
        }



        [HttpGet]
        [NeglectActionFilter]
        [HttpGet]
        public async Task<IActionResult> Get災害別選項(string 盤點種類)
        {
            var 是否災害盤點 = await _context.盤點種類
                .Where(x => x.盤點種類1 == 盤點種類)
                .Select(x => x.是否災害盤點)
                .FirstOrDefaultAsync();

            if (是否災害盤點)
            {
                var 災害別選項 = await _context.災害別
                    .Where(x => !x.是否停用)
                    .Select(x => new { Value = x.災害別1, Text = x.災害別1 + "_" + x.災害別名稱 })
                    .ToListAsync();

                return Json(new { show = true, options = 災害別選項 });
            }
            else
            {
                return Json(new { show = false, options = new object[0] });
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Update)]
        public async Task<IActionResult> Edit([Bind("進銷存組織,單據別,日期,流水號,倉庫代號,盤點種類,災害別,盤點人,盤點日期,備註")] HW_01_庫存盤點主檔EditViewModel postData)
        {
            if (!ModelState.IsValid)
                return Ok(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR) { data = ModelState.ToErrorInfos() });

            try
            {
                var model = _mapper.Map<庫存盤點主檔>(postData);
                var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));
                model.修改人 = ua.UserNo;
                model.修改日期時間 = DateTime.Now;

                _context.庫存盤點主檔.Update(model);
                int opCount = await _context.SaveChangesAsync();

                if (opCount > 0)
                    return Ok(new ReturnData(ReturnState.ReturnCode.OK) { data = postData });
            }
            catch (Exception ex)
            {
                return CreatedAtAction(nameof(Edit), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
                {
                    message = ex.GetOriginalException().ToMeaningfulMessage()
                });
            }

            return CreatedAtAction(nameof(Edit), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
            {
                message = "更新失敗"
            });
        }
        [ProcUseRang(ProcNo, ProcUseRang.Delete)]
        public async Task<ActionResult> Delete(string 進銷存組織, string 單據別, DateTime 日期, decimal 流水號)
        {
            if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 日期 == default || 流水號 == default)
            {
                return NotFound(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));
            }

            var viewModel = await GetBaseQuery()
                .Where(s => s.進銷存組織 == 進銷存組織
                         && s.單據別 == 單據別
                         && s.日期 == 日期
                         && s.流水號 == 流水號)
                .SingleOrDefaultAsync();

            if (viewModel == null)
            {
                return NotFound(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));
            }

            return PartialView(viewModel); // 回傳 Delete.cshtml 的 PartialView
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Delete)]
        public async Task<IActionResult> DeleteConfirmed([Bind("進銷存組織,單據別,日期,流水號")] HW_01_庫存盤點主檔DisplayViewModel postData)
        {
            //if (postData.進銷存組織 == null || postData.單據別名稱 == null)
            if (postData.進銷存組織 == null || postData.單據別 == null)
                return NotFound();

            try
            {
                var model = await _context.庫存盤點主檔
                    .Where(x =>
                        x.進銷存組織 == postData.進銷存組織 &&
                        //x.單據別 == postData.單據別名稱 &&
                        x.單據別 == postData.單據別 &&
                        x.日期 == postData.日期 &&
                        x.流水號 == postData.流水號)
                    .SingleOrDefaultAsync();

                if (model == null)
                    return NotFound();

                _context.庫存盤點主檔.Remove(model);
                int opCount = await _context.SaveChangesAsync();

                if (opCount > 0)
                    return Ok(new ReturnData(ReturnState.ReturnCode.OK));
            }
            catch (Exception ex)
            {
                return CreatedAtAction(nameof(DeleteConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR)
                {
                    message = ex.GetOriginalException().ToMeaningfulMessage()
                });
            }

            return CreatedAtAction(nameof(DeleteConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR)
            {
                message = "資料已不存在"
            });
        }
        //[HttpPost, ActionName("GetDetailDataPost")]
        //[ValidateAntiForgeryToken]
        //[NeglectActionFilter]
        //public async Task<IActionResult> GetDetails([FromBody] HW_01_庫存盤點主檔DisplayViewModel keys)
        //{
        //    if (keys.進銷存組織 == null || keys.單據別 == null)
        //    {
        //        return NotFound();
        //    }

        //    IQueryable<HW_01_庫存盤點明細檔DisplayViewModel> sql = GetDetailBaseQuery()
        //        .Where(x => x.進銷存組織 == keys.進銷存組織
        //                 && x.單據別 == keys.單據別
        //                 && x.日期 == keys.日期
        //                 && x.流水號 == keys.流水號);

        //    var queryedData = await PaginatedList<HW_01_庫存盤點明細檔DisplayViewModel>.CreateAsync(sql);

        //    return CreatedAtAction(nameof(GetDetails), new ReturnData(ReturnState.ReturnCode.OK)
        //    {
        //        data = queryedData
        //    });
        //}

        [HttpPost, ActionName("GetDetailDataPost")]
        [ValidateAntiForgeryToken]
        [NeglectActionFilter]
        public async Task<IActionResult> GetDetailData([FromBody] HW_01_庫存盤點主檔DisplayViewModel keys)
        {
            if (keys == null)
            {
                return BadRequest("未提供主檔鍵值資料");
            }
            if (string.IsNullOrEmpty(keys.進銷存組織)
                || string.IsNullOrEmpty(keys.單據別)
                || keys.日期 == default
                || keys.流水號 == default)
            {
                return NotFound();
            }
            #region test
            // 🔍 1. 先印出傳入的主鍵條件
            Debug.WriteLine($"[DEBUG] 傳入條件：進銷存組織={keys.進銷存組織}, 單據別={keys.單據別}, 日期={keys.日期:yyyy-MM-dd HH:mm:ss}, 流水號={keys.流水號}");

            //// 🔍 2. 先查 DB 中是否有符合該日期的資料（這段可放 try 區塊外也可內）
            //var checkDateList = await GetDetailBaseQuery()
            //    .Where(x => x.日期.Date == keys.日期.Date)
            //    .Select(x => x.日期)
            //    .ToListAsync();


            //foreach (var dt in checkDateList)
            //{
            //    Debug.WriteLine($"[DEBUG] DB 中存在的日期：{dt:yyyy-MM-dd HH:mm:ss}");
            //}

            // ✅ 先過濾條件
            var baseQuery = GetDetailBaseQuery()
                .Where(x => x.進銷存組織 == keys.進銷存組織
                         && x.單據別 == keys.單據別
                         && x.日期.Date == keys.日期.Date
                         && x.流水號 == keys.流水號);

            // ✅ 先拉出來 Debug 看有哪些資料（這樣才精準）
            var debugList = baseQuery.ToList();

            // DEBUG：逐筆比對欄位差異
            foreach (var x in debugList)
            {
                Debug.WriteLine($@"[DEBUG] 明細比對：
進銷存組織 => DB={x.進銷存組織} / 查詢={keys.進銷存組織} / 相符: {x.進銷存組織 == keys.進銷存組織}
單據別     => DB={x.單據別} / 查詢={keys.單據別} / 相符: {x.單據別 == keys.單據別}
日期       => DB={x.日期:yyyy-MM-dd HH:mm:ss} / 查詢={keys.日期:yyyy-MM-dd HH:mm:ss} / 相符: {x.日期.Date == keys.日期.Date}
流水號     => DB={x.流水號} / 查詢={keys.流水號} / 相符: {x.流水號 == keys.流水號}
");
            }

            #endregion
            var detailQuery = GetDetailBaseQuery()
                .Where(x => x.進銷存組織 == keys.進銷存組織
                         && x.單據別 == keys.單據別
                         //&& x.日期 == keys.日期
                         && x.日期.Date == keys.日期.Date
                         && x.流水號 == keys.流水號);

            #region test
            // 🔍 4. 將結果轉成 List，再來 Debug 比對
            var resultList = await detailQuery.ToListAsync();
            foreach (var x in resultList)
            {
                Debug.WriteLine($"[DEBUG] 比對結果 => DB 日期: {x.日期:yyyy-MM-dd HH:mm:ss} / 傳入日期: {keys.日期:yyyy-MM-dd HH:mm:ss} / 相符: {x.日期.Date == keys.日期.Date}");
            }

            // 🔍 5. 確認是否查無資料
            if (!resultList.Any())
            {
                Debug.WriteLine("[DEBUG] 查無符合條件的明細資料。");
            }
            #endregion
            var pagedData = await PaginatedList<HW_01_庫存盤點明細檔DisplayViewModel>.CreateAsync(detailQuery);

            return Ok(new ReturnData(ReturnState.ReturnCode.OK) { data = pagedData });
        }

        //private IQueryable<HW_01_庫存盤點明細檔DisplayViewModel> GetDetailBaseQuery()
        //        {
        //            #region 測試
        //            Debug.WriteLine("[DEBUG] 進入 GetDetailBaseQuery()"); // 👉 插入位置 #1：方法開頭

        //            // 插入位置 #2：印出左右資料表筆數
        //            Debug.WriteLine($"[DEBUG] 庫存盤點明細原始筆數：{_context.庫存盤點明細.Count()}");
        //            Debug.WriteLine($"[DEBUG] 事業商品檔原始筆數：{_context.事業商品檔.Count()}");
        //            // 插入位置 #3：測試 JOIN 條件是否成立
        //            var joinTest = (from d in _context.庫存盤點明細
        //                            select new { d.進銷存組織, d.商品編號 })
        //                           .Distinct()
        //                           .ToList();

        //            foreach (var item in joinTest)
        //            {
        //                bool exists = _context.事業商品檔.Any(p =>
        //                    p.事業 == item.進銷存組織 && p.商品編號 == item.商品編號);

        //                Debug.WriteLine($"[DEBUG] 測試 JOIN 是否成立：事業={item.進銷存組織}, 商品={item.商品編號}, 是否存在於商品檔：{exists}");
        //            }

        //#endregion
        //            var query = from d in _context.庫存盤點明細
        //                        join p in _context.事業商品檔
        //                            on new { d.進銷存組織, d.商品編號 }
        //                            equals new { 進銷存組織 = p.事業, p.商品編號 }
        //                        select new HW_01_庫存盤點明細檔DisplayViewModel
        //                        {
        //                            進銷存組織 = d.進銷存組織,
        //                            單據別 = d.單據別,
        //                            日期 = d.日期,
        //                            流水號 = d.流水號,
        //                            項次 = d.項次,
        //                            商品編號 = d.商品編號,
        //                            商品名稱 = p.商品名稱,
        //                            商品規格 = p.商品規格,
        //                            單位 = p.銷售商品單位,
        //                            庫存數量 = d.庫存數量,
        //                            盤點數量 = d.盤點數量
        //                        };

        //            var list = query.ToList(); // 強制執行查詢
        //            Debug.WriteLine($"[DEBUG] 明細查詢筆數（JOIN後）：{list.Count}");

        //            return list.AsQueryable();
        //        }
        private IQueryable<HW_01_庫存盤點明細檔DisplayViewModel> GetDetailBaseQuery()
        {
            Debug.WriteLine("[DEBUG] 進入 GetDetailBaseQuery()");

            var baseQuery = from d in _context.庫存盤點明細
                            join p in _context.事業商品檔
                                on new { 事業 = d.進銷存組織, d.商品編號 }
                                equals new { p.事業, p.商品編號 }
                                into gj
                            from sub in gj.DefaultIfEmpty()
                            select new HW_01_庫存盤點明細檔DisplayViewModel
                            {
                                進銷存組織 = d.進銷存組織,
                                單據別 = d.單據別,
                                日期 = d.日期,
                                流水號 = d.流水號,
                                項次 = d.項次,
                                商品編號 = d.商品編號,
                                商品名稱 = sub != null ? sub.商品名稱 : "[❌無對應商品]",
                                商品規格 = sub != null ? sub.商品規格 : null,
                                單位 = sub != null ? sub.銷售商品單位 : null,
                                庫存數量 = d.庫存數量,
                                盤點數量 = d.盤點數量
                            };

            Debug.WriteLine($"[DEBUG] 明細查詢筆數（JOIN後）：{baseQuery.Count()}");

            return baseQuery;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Add)]
        public async Task<IActionResult> CreateDetail([Bind("進銷存組織,單據別,日期,流水號,商品編號,盤點數量")] HW_01_庫存盤點明細檔CreateViewModel postData)
        {
            if (!ModelState.IsValid)
                return Ok(new ReturnData(ReturnState.ReturnCode.CREATE_ERROR) { data = ModelState.ToErrorInfos() });

            try
            {
                var maxItem = await _context.庫存盤點明細
                    .Where(x => x.進銷存組織 == postData.進銷存組織
                             && x.單據別 == postData.單據別
                             && x.日期 == postData.日期
                             && x.流水號 == postData.流水號)
                    .Select(x => (int?)x.項次)
                    .MaxAsync() ?? 0;

                postData.項次 = maxItem + 1;

                var model = _mapper.Map<庫存盤點明細>(postData);

                var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));
                model.修改人 = ua.UserNo;
                model.修改日期時間 = DateTime.Now;
                _context.庫存盤點明細.Add(model);
                int opCount = await _context.SaveChangesAsync();

                if (opCount > 0)
                {
                    return Ok(new ReturnData(ReturnState.ReturnCode.OK)
                    {
                        data = await GetDetailBaseQuery()
                            .Where(x => x.進銷存組織 == model.進銷存組織 &&
                                        x.單據別 == model.單據別 &&
                                        x.日期 == model.日期 &&
                                        x.流水號 == model.流水號 &&
                                        x.項次 == model.項次)
                            .SingleOrDefaultAsync()
                    });
                }
            }
            catch (Exception ex)
            {
                return CreatedAtAction(nameof(CreateDetail), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
                {
                    message = ex.GetOriginalException().ToMeaningfulMessage()
                });
            }

            return CreatedAtAction(nameof(CreateDetail), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
            {
                message = "發生未知錯誤"
            });
        }


        [HttpGet]
        [ProcUseRang(ProcNo, ProcUseRang.Delete)]
        public async Task<IActionResult> DeleteDetail(string 進銷存組織, string 單據別, DateTime 日期, decimal 流水號, decimal 項次)
        {
            if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 日期 == default || 流水號 == default || 項次 == default)
            {
                return NotFound();
            }

            var viewModel = await GetDetailBaseQuery()
                .Where(x => x.進銷存組織 == 進銷存組織 &&
                            x.單據別 == 單據別 &&
                            x.日期 == 日期 &&
                            x.流水號 == 流水號 &&
                            x.項次 == 項次)
                .SingleOrDefaultAsync();

            if (viewModel == null)
                return NotFound();

            return PartialView(viewModel); // 對應 Views/HW_01/DeleteDetail.cshtml
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Delete)]
        public async Task<IActionResult> DeleteDetailConfirmed([Bind("進銷存組織,單據別,日期,流水號,項次")] HW_01_庫存盤點明細檔DisplayViewModel postData)
        {
            if (postData.進銷存組織 == null || postData.商品編號 == null)
                return NotFound();

            try
            {
                var model = await _context.庫存盤點明細
                    .Where(x => x.進銷存組織 == postData.進銷存組織
                             && x.單據別 == postData.單據別
                             && x.日期 == postData.日期
                             && x.流水號 == postData.流水號
                             && x.項次 == postData.項次)
                    .SingleOrDefaultAsync();

                if (model == null)
                    return NotFound();

                _context.庫存盤點明細.Remove(model);
                int opCount = await _context.SaveChangesAsync();

                if (opCount > 0)
                    return Ok(new ReturnData(ReturnState.ReturnCode.OK));
            }
            catch (Exception ex)
            {
                return CreatedAtAction(nameof(DeleteDetailConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR)
                {
                    message = ex.GetOriginalException().ToMeaningfulMessage()
                });
            }

            return CreatedAtAction(nameof(DeleteDetailConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR)
            {
                message = "資料已不存在"
            });
        }




    }
}
