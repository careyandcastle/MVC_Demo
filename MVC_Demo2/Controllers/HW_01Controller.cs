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
                        單據別名稱 = m.單據別,
                        日期 = m.日期,
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
                        是否註記刪除 = false, // 若有欄位再加
                        修改人 = CustomSqlFunctions.ConcatCodeAndName(m.修改人, CustomSqlFunctions.DecryptToString(_u.姓名)),
                        修改時間 = m.修改日期時間
                    }).AsNoTracking();
        }

        public async Task<IActionResult> Create()
        {
            var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));

            // 取得列帳日（假設你已存在方法或變數）
            //DateTime 列帳日期 = await Get列帳日期Async(); // 可自行實作，也可以用 DateTime.Today;
            DateTime 列帳日期 = DateTime.Today; // 可自行實作，也可以用 DateTime.Today;

            var viewModel = new HW_01_庫存盤點主檔BasicViewModel
            {
                進銷存組織 =  ua.BusinessNo,
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
                            x.單據別名稱 == model.單據別 &&
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Delete)]
        public async Task<IActionResult> DeleteConfirmed([Bind("進銷存組織,單據別名稱,日期,流水號")] HW_01_庫存盤點主檔DisplayViewModel postData)
        {
            if (postData.進銷存組織 == null || postData.單據別名稱 == null)
                return NotFound();

            try
            {
                var model = await _context.庫存盤點主檔
                    .Where(x =>
                        x.進銷存組織 == postData.進銷存組織 &&
                        x.單據別 == postData.單據別名稱 &&
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


        [HttpPost, ActionName("GetDetailDataPost")]
        [ValidateAntiForgeryToken]
        [NeglectActionFilter]
        public async Task<IActionResult> GetDetailData([FromBody] HW_01_庫存盤點主檔DisplayViewModel keys)
        {
            if (keys.進銷存組織 == null || keys.單據別名稱 == null)
                return NotFound();

            IQueryable<HW_01_庫存盤點明細檔DisplayViewModel> sql = GetDetailBaseQuery()
                .Where(x => x.進銷存組織 == keys.進銷存組織
                         && x.單據別 == keys.單據別名稱
                         && x.日期 == keys.日期
                         && x.流水號 == keys.流水號);

            var queryedData = await PaginatedList<HW_01_庫存盤點明細檔DisplayViewModel>.CreateAsync(sql);
            return CreatedAtAction(nameof(GetDetailData), new ReturnData(ReturnState.ReturnCode.OK) { data = queryedData });
        }

        private IQueryable<HW_01_庫存盤點明細檔DisplayViewModel> GetDetailBaseQuery()
        {
            return from d in _context.庫存盤點明細
                   join p in _context.事業商品檔 on new { d.進銷存組織, d.商品編號 } equals new { 進銷存組織 = p.事業, p.商品編號 }
                   select new HW_01_庫存盤點明細檔DisplayViewModel
                   {
                       進銷存組織 = d.進銷存組織,
                       單據別 = d.單據別,
                       日期 = d.日期,
                       流水號 = d.流水號,
                       項次 = d.項次,
                       商品編號 = d.商品編號,
                       商品名稱 = p.商品編號, // 若有中文名稱欄位請改寫
                       商品規格 = "", // 加上規格欄位可補充
                       單位 = "",       // 加上單位欄位可補充
                       庫存數量 = d.庫存數量,
                       盤點數量 = d.盤點數量
                   };
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
