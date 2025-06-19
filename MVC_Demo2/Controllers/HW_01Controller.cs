using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

        // ✅ 這裡是 InitInventoryDefaultValues() 的正確位置
        private (string org, string period, string date, DateTime format_date, string userNo, string businessNo, string departmentNo, string divisionNo, string branchNo) InitInventoryDefaultValues()
        {
            var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));
            if (ua == null)
            {
                Debug.WriteLine("[InitInventoryDefaultValues] ⚠️ 無法從 Session 取得使用者帳號資訊");
                return (null, null, null, default(DateTime), null, null, null, null, null);
            }

            var orgRecord = _context.進銷存組織
                .Where(x =>
                    x.列帳事業 == ua.BusinessNo && x.列帳單位 == ua.DepartmentNo &&
                    (string.IsNullOrEmpty(x.列帳部門) || x.列帳部門 == ua.DivisionNo) &&
                    (string.IsNullOrEmpty(x.列帳分部) || x.列帳分部 == ua.BranchNo) &&
                    x.是否物流組織 == false
                )
                .OrderByDescending(x =>
                    (x.列帳事業 + x.列帳單位 + x.列帳部門 + x.列帳分部).Length
                )
                .FirstOrDefault();

            if (orgRecord == null)
            {
                Debug.WriteLine("[InitInventoryDefaultValues] ⚠️ 找不到符合條件的進銷存組織資料");
                return (null, null, null, default(DateTime), ua.UserNo, ua.BusinessNo, ua.DepartmentNo, ua.DivisionNo, ua.BranchNo);
            }

            string org = orgRecord.進銷存組織1;
            string period = orgRecord.列帳日期.ToString("yyyyMM");
            string date = orgRecord.列帳日期.ToString("yyyy-MM-dd");

            //Debug.WriteLine($"[InitInventoryDefaultValues] ✅ 組織代號={org}, 列帳年月={period}, 列帳日期={date}");
            //Debug.WriteLine($"[Create] ▶ 使用者帳號={userNo}，組織代號={org}，年月={period}，列帳日期={date}，格式日期{formate_date}，事業={biz}，單位={dept}，部門={div}，分部={branch}");
            Debug.WriteLine($"[Create] ▶ 使用者帳號={ ua.UserNo}，組織代號={org}，年月={period}，列帳日期={date}，格式日期{orgRecord.列帳日期}，事業={ ua.BusinessNo}，單位={ua.DepartmentNo}，部門={ ua.DivisionNo}，分部={ua.BranchNo}");

            return (org, period, date, orgRecord.列帳日期, ua.UserNo, ua.BusinessNo, ua.DepartmentNo, ua.DivisionNo, ua.BranchNo);
        }


        public IActionResult Index()
        {
            var (org, period, date, formate_date, userNo, biz, dept, div, branch) = InitInventoryDefaultValues();

            Debug.WriteLine($"[Create] ▶ 使用者帳號={userNo}，組織代號={org}，年月={period}，日期={date}，事業={biz}，單位={dept}，部門={div}，分部={branch}");

            ViewBag.var進銷存組織 = org;
            ViewBag.var列帳年月 = period;
            ViewBag.var列帳日期 = date;

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
                    join pi in _context.修改人 on m.盤點人 equals pi.修改人1 into pijoin
                    from _pi in pijoin.DefaultIfEmpty()
                    select new HW_01_庫存盤點主檔DisplayViewModel
                    {
                        進銷存組織 = m.進銷存組織,
                        進銷存組織名稱 = m.進銷存組織 + "_" + m.進銷存組織Navigation.進銷存組織簡稱,
                        單據別 = m.單據別,
                        單據別名稱 = m.單據別 + "_" + m.單據別Navigation.單據別名稱,
                        日期 = m.日期,
                        流水號 = m.流水號,
                        倉庫代號 = m.倉庫代號,
                        倉庫代號名稱 = m.倉庫代號 + "_" + m.倉庫基本檔.倉庫簡稱,
                        盤點種類 = m.盤點種類,
                        盤點種類名稱 = m.盤點種類Navigation.盤點種類1,
                        災害別 = m.災害別,
                        災害別名稱 = m.災害別 + "_" + m.災害別Navigation.災害別名稱,
                        盤點人 = m.盤點人,
                        盤點人姓名 = m.盤點人 + "_" + CustomSqlFunctions.DecryptToString(_pi.姓名), // <- 若有帳號表可加入
                        備註 = m.備註,
                        盤點日期 = m.盤點日期,
                        庫存異動狀態 = m.庫存異動狀態,
                        庫存異動狀態名稱 = m.庫存異動狀態Navigation.庫存異動狀態1,
                        是否註記刪除 = m.是否註記刪除, // 若有欄位再加
                        是否註記刪除顯示 = m.是否註記刪除 ? "是" : "否",
                        修改人 = m.修改人,
                        修改人姓名 = CustomSqlFunctions.ConcatCodeAndName(m.修改人, CustomSqlFunctions.DecryptToString(_u.姓名)),
                        修改時間 = m.修改日期時間
                    }).AsNoTracking();
        }

        [NeglectActionFilter]
        public bool CanClickCreate(int index) => index % 2 == 0;

        public async Task<IActionResult> Create()
        {
            var (org, period, date, formate_date, userNo, biz, dept, div, branch) = InitInventoryDefaultValues();

            Debug.WriteLine($"[Create] ▶ 使用者帳號={userNo}，組織代號={org}，年月={period}，日期={date}，格式日期{formate_date}，事業={biz}，單位={dept}，部門={div}，分部={branch}");

            ViewBag.var進銷存組織 = org;
            ViewBag.var列帳日期 = date;

            var viewModel = new HW_01_庫存盤點主檔BasicViewModel
            {
                日期 = formate_date,
                庫存異動狀態 = "0",   // ✅ 初始狀態為「未異動」
                是否註記刪除 = false,
                進銷存組織 = org,
                單據別 = "INV",
                流水號 = 0
            };


            var 倉庫選項 = await Get倉庫選項Async(biz, dept, div, branch);

            // 🔍 除錯輸出：顯示符合條件的倉庫筆數與清單
            Debug.WriteLine($"[Create] ▶ 查詢符合條件的倉庫筆數：{倉庫選項.Count}");
            foreach (var item in 倉庫選項)
            {
                Debug.WriteLine($"[Create] ▶ 倉庫選項：Value={item.Value}, Text={item.Text}");
            }

            if (!倉庫選項.Any()) Debug.WriteLine("[Create] ⚠️ 倉庫基本檔為空");
            倉庫選項.Insert(0, new SelectListItem { Text = "--請選擇--", Value = "" });
            ViewBag.倉庫選項 = 倉庫選項;

            // ===== 盤點種類下拉 =====
            var 盤點種類選項 = await _context.盤點種類
                .Select(s => new SelectListItem
                {
                    Text = s.盤點種類1 + "_" + s.盤點種類名稱,
                    Value = s.盤點種類1
                }).ToListAsync();

            if (!盤點種類選項.Any()) Debug.WriteLine("[Create] ⚠️ 盤點種類資料為空");
            盤點種類選項.Insert(0, new SelectListItem { Text = "--請選擇--", Value = "" });
            ViewBag.盤點種類選項 = 盤點種類選項;

            // ===== 災害別：預設為空，依盤點種類動態載入 =====
            ViewBag.災害別選項 = new List<SelectListItem> {
        new SelectListItem { Text = "--請先選擇盤點種類--", Value = "" }
    };

            // ===== 盤點人下拉選單：從 byte[] 轉為字串 =====
            var 盤點人選項 = await _context.修改人
                .Select(s => new SelectListItem
                {
                    Text = s.修改人1 + "_" + CustomSqlFunctions.DecryptToString(s.姓名),
                    Value = s.修改人1
                }).ToListAsync();

            if (!盤點人選項.Any()) Debug.WriteLine("[Create] ⚠️ 無盤點人選項（修改人表為空）");
            盤點人選項.Insert(0, new SelectListItem { Text = "--請選擇--", Value = "" });
            ViewBag.盤點人選項 = 盤點人選項;

            return PartialView(viewModel);
        }

        [HttpGet("HW_01/依盤點種類取得災害別")]
        public JsonResult 依盤點種類取得災害別(string 盤點種類)
        {
            Debug.WriteLine($"[依盤點種類取得災害別] ▶ 收到盤點種類：{盤點種類}");

            // 判斷此盤點種類是否屬於「災害盤點」
            var isDisaster = _context.盤點種類
                .Where(p => p.盤點種類1 == 盤點種類 && p.是否災害盤點)
                .Any();

            if (!isDisaster)
            {
                Debug.WriteLine("[依盤點種類取得災害別] ❌ 非災害盤點，不回傳災害別");
                return Json(new List<SelectListItem>());
            }

            // 撈出「未停用」的災害別選項
            var 災害別選項 = _context.災害別
                .Where(z => z.是否停用 == false)
                .Select(z => new SelectListItem
                {
                    Value = z.災害別1,
                    Text = z.災害別1 + "_" + z.災害別名稱
                })
                .ToList();

            Debug.WriteLine($"[依盤點種類取得災害別] ✅ 成功，筆數：{災害別選項.Count}");
            return Json(災害別選項);
        }

        private async Task<List<SelectListItem>> Get倉庫選項Async(string biz, string dept, string div, string branch)
        {
            var 倉庫選項 = await _context.倉庫基本檔
                .Where(x =>
                    x.FA列帳事業 == biz &&
                    x.FA列帳單位 == dept &&
                    (x.FA列帳部門 == div || x.FA列帳部門 == null) &&
                    (x.FA列帳分部 == branch || x.FA列帳分部 == null) &&
                    !x.是否暫停入庫 &&
                    !x.是否裁撤
                )
                .OrderBy(x => x.倉庫代號)
                .Select(x => new SelectListItem
                {
                    Text = x.倉庫代號 + "_" + x.倉庫名稱,
                    Value = x.倉庫代號
                })
                .ToListAsync();

            // 🔍 除錯輸出
            Debug.WriteLine($"[Get倉庫選項Async] ▶ 查詢符合條件的倉庫筆數：{倉庫選項.Count}");
            foreach (var item in 倉庫選項)
            {
                Debug.WriteLine($"[Get倉庫選項Async] ▶ 倉庫選項：Value={item.Value}, Text={item.Text}");
            }

            // 插入預設選項
            倉庫選項.Insert(0, new SelectListItem { Text = "--請選擇--", Value = "" });

            return 倉庫選項;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Add)]
        public async Task<IActionResult> Create([Bind("進銷存組織,單據別,日期,倉庫代號,盤點種類,災害別,盤點人,盤點日期,備註")] HW_01_庫存盤點主檔CreateViewModel postData)
        {
            Debug.WriteLine("[Create] ▶ 收到建立請求，表單內容如下：");
            Debug.WriteLine($"        進銷存組織 = {postData.進銷存組織}");
            Debug.WriteLine($"        單據別     = {postData.單據別}");
            Debug.WriteLine($"        日期       = {postData.日期:yyyy-MM-dd}");
            Debug.WriteLine($"        倉庫代號   = {postData.倉庫代號}");
            Debug.WriteLine($"        盤點種類   = {postData.盤點種類}");
            Debug.WriteLine($"        災害別     = {postData.災害別 ?? "[null]"}");
            Debug.WriteLine($"        盤點人     = {postData.盤點人}");
            Debug.WriteLine($"        盤點日期   = {postData.盤點日期:yyyy-MM-dd}");
            Debug.WriteLine($"        備註       = {postData.備註 ?? "[null]"}");

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("[Edit] [ERROR] ModelState 無效（初始驗證）");
                foreach (var kv in ModelState.ToErrorInfos())
                {
                    foreach (var msg in kv.Value)
                        Debug.WriteLine($"        ↳ 欄位：{kv.Key}，錯誤：{msg}");
                }
                //Debug.WriteLine("[Create] [ERROR] ModelState 初始驗證失敗");
                return Ok(new ReturnData(ReturnState.ReturnCode.CREATE_ERROR) { data = ModelState.ToErrorInfos() });
            }

            await ValidateForCreate(postData);
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("[Create] [ERROR] 驗證邏輯後 ModelState 失敗");
                return Ok(new ReturnData(ReturnState.ReturnCode.CREATE_ERROR) { data = ModelState.ToErrorInfos() });
            }

            try
            {
                var model = _mapper.Map<庫存盤點主檔>(postData);

                // 補必要欄位（防止 NULL 或 FK 錯誤）
                model.單據別 = "INV";
                model.庫存異動狀態 = "0";           // ✅ 外鍵欄位，避免外鍵例外
                model.是否註記刪除 = false;           // ✅ NOT NULL 欄位
                model.備註 ??= string.Empty;         // ✅ 備註不能為 null

                // 產生流水號（依組織＋單據別＋日期）
                //model.流水號 = await _context.庫存盤點主檔
                //    .Where(x => x.進銷存組織 == model.進銷存組織 && x.單據別 == "INV" && x.日期 == model.日期)
                //    .Select(x => x.流水號)
                //    .DefaultIfEmpty(0)
                //    .MaxAsync() + 1;
                var 流水號清單 = await _context.庫存盤點主檔
    .Where(x => x.進銷存組織 == model.進銷存組織 && x.單據別 == "INV" && x.日期 == model.日期)
    .Select(x => x.流水號)
    .ToListAsync();

                model.流水號 = (流水號清單.Any() ? 流水號清單.Max() : 0) + 1;

                var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));
                model.修改人 = ua.UserNo;
                model.修改日期時間 = DateTime.Now;

                _context.庫存盤點主檔.Add(model);
                int opCount = await _context.SaveChangesAsync();

                Debug.WriteLine($"[Create] ▶ 建立成功，新增筆數：{opCount}，流水號：{model.流水號}");

                if (opCount > 0)
                {
                    var newData = await GetBaseQuery()
                        .Where(x =>
                            x.進銷存組織 == model.進銷存組織 &&
                            x.單據別 == model.單據別 &&
                            x.日期 == model.日期 &&
                            x.流水號 == model.流水號
                        ).SingleOrDefaultAsync();

                    return Ok(new ReturnData(ReturnState.ReturnCode.OK)
                    {
                        data = newData
                    });
                }
            }
            catch (Exception ex)
            {
                var realEx = ex.GetOriginalException();
                Debug.WriteLine($"[Create] [ERROR] 發生例外：{realEx.ToMeaningfulMessage()}");
                return CreatedAtAction(nameof(Create), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
                {
                    message = realEx.ToMeaningfulMessage()
                });
            }

            Debug.WriteLine("[Create] [ERROR] 未知錯誤，未能儲存資料");
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

            var (org, period, date, formate_date, userNo, biz, dept, div, branch) = InitInventoryDefaultValues();

            Debug.WriteLine($"[Create] ▶ 使用者帳號={userNo}，組織代號={org}，年月={period}，日期={date}，事業={biz}，單位={dept}，部門={div}，分部={branch}");

            ViewBag.var進銷存組織 = org;
            //ViewBag.var列帳年月 = period;
            ViewBag.var列帳日期 = date;

            //viewModel.備註 ??= string.Empty;

            //var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));

            //var viewModel = new HW_01_庫存盤點主檔BasicViewModel
            //{ };


            var 倉庫選項 = await Get倉庫選項Async(biz, dept, div, branch);
            ViewBag.倉庫代號選項 = 倉庫選項;
            // 預備倉庫下拉（含條件）
            //ViewBag.倉庫代號選項 = await _context.倉庫基本檔
            //    .Where(w =>
            //        w.是否裁撤 == false &&
            //        w.是否暫停入庫 == false &&
            //        w.是否暫停出庫 == false &&
            //        w.是否允許負庫存銷售 == true)
            //    .OrderBy(o => o.倉庫代號)
            //    .Select(s => new SelectListItem
            //    {
            //        Text = s.倉庫代號 + "_" + s.倉庫簡稱,
            //        Value = s.倉庫代號
            //    })
            //    .ToListAsync();

            // 預備盤點種類下拉
            ViewBag.盤點種類選項 = await _context.盤點種類
                .Where(w => !w.是否停用)
                .Select(s => new SelectListItem
                {
                    Text = s.盤點種類1 + "_" + s.盤點種類名稱,
                    Value = s.盤點種類1
                })
                .ToListAsync();

            var 選取的盤點種類值 = model.盤點種類;

            // 判斷是否需要災害別下拉
            var 是否災害盤點 = await _context.盤點種類
                .Where(x => x.盤點種類1 == 選取的盤點種類值)
                .Select(x => x.是否災害盤點)
                .FirstOrDefaultAsync();

            if (是否災害盤點)
            {
                var 災害別選項 = await _context.災害別
                    .Where(x => !x.是否停用)
                    .Select(x => new SelectListItem
                    {
                        Text = x.災害別1 + "_" + x.災害別名稱,
                        Value = x.災害別1
                    })
                    .ToListAsync();

                災害別選項.Insert(0, new SelectListItem { Text = "--請選擇--", Value = "" });
                ViewBag.災害別選項 = 災害別選項;
            }
            else
            {
                ViewBag.災害別選項 = new List<SelectListItem> { new SelectListItem { Text = "(無)", Value = "" } };
            }

            return PartialView(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Update)]
        public async Task<IActionResult> Edit([Bind("進銷存組織,單據別,日期,流水號,倉庫代號,盤點種類,災害別,備註, 盤點人,盤點日期,  庫存異動狀態, 是否註記刪除")] HW_01_庫存盤點主檔EditViewModel postData)
        {
            //Debug.WriteLine($"[Edit] ▶ 收到編輯請求 - 組織：{postData.進銷存組織}, 單據別: {postData.單據別} ,日期：{postData.日期:yyyy-MM-dd}, 流水號：{postData.流水號}");
            Debug.WriteLine("[Edit] ▶ 收到編輯請求內容：");
            Debug.WriteLine($"        進銷存組織 = {postData.進銷存組織}");
            Debug.WriteLine($"        單據別     = {postData.單據別}");
            Debug.WriteLine($"        日期       = {postData.日期:yyyy-MM-dd}");
            Debug.WriteLine($"        流水號     = {postData.流水號}");
            Debug.WriteLine($"        倉庫代號   = {postData.倉庫代號}");
            Debug.WriteLine($"        盤點種類   = {postData.盤點種類}");
            Debug.WriteLine($"        災害別     = {postData.災害別 ?? "[null]"}");
            Debug.WriteLine($"        備註       = {postData.備註 ?? "[null]"}");
            Debug.WriteLine($"        盤點人     = {postData.盤點人}");
            Debug.WriteLine($"        庫存異動狀態   = {postData.庫存異動狀態 ?? "[null]"}");
            Debug.WriteLine($"        是否註記刪除   = {postData.是否註記刪除}");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("[Edit] [ERROR] ModelState 無效（初始驗證）");
                foreach (var kv in ModelState.ToErrorInfos())
                {
                    foreach (var msg in kv.Value)
                        Debug.WriteLine($"        ↳ 欄位：{kv.Key}，錯誤：{msg}");
                }

                return Ok(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
                {
                    data = ModelState.ToErrorInfos()
                });
            }

            try
            {
                //Debug.WriteLine("[Edit] ▶ 進行驗證邏輯...");
                //await ValidateForEdit(postData);

                if (!ModelState.IsValid)
                {
                    Debug.WriteLine("[Edit] [ERROR] ModelState 無效（驗證邏輯後）");
                    foreach (var kv in ModelState.ToErrorInfos())
                    {
                        foreach (var msg in kv.Value)
                            Debug.WriteLine($"        ↳ 欄位：{kv.Key}，錯誤：{msg}");
                    }

                    return Ok(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
                    {
                        data = ModelState.ToErrorInfos()
                    });
                }

                var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));
                Debug.WriteLine($"[Edit] ▶ 使用者帳號：{ua?.UserNo}");

                // 讀取原本的資料庫記錄
                var originalModel = await _context.庫存盤點主檔
                    .AsNoTracking()
                    .Where(x =>
                        x.進銷存組織 == postData.進銷存組織 &&
                        x.單據別 == postData.單據別 &&
                        x.日期 == postData.日期 &&
                        x.流水號 == postData.流水號)
                    .Select(x => new { x.庫存異動狀態, x.是否註記刪除 })
                    .FirstOrDefaultAsync();

                if (originalModel == null)
                {
                    Debug.WriteLine("[Edit] [ERROR] 查無原始資料");
                    return NotFound();
                }

                // 將 postData 映射為 model
                var model = _mapper.Map<HW_01_庫存盤點主檔EditViewModel, 庫存盤點主檔>(postData);
                if (model == null)
                {
                    Debug.WriteLine("[Edit] [ERROR] 映射失敗，model 為 null");
                    return NotFound();
                }

                // 這裡是重點：強制回填資料庫查得的值（以避免被惡意改寫）
                model.庫存異動狀態 = originalModel.庫存異動狀態;
                model.是否註記刪除 = originalModel.是否註記刪除;

                // 補上修改人與時間
                model.修改人 = ua.UserNo;
                model.修改日期時間 = DateTime.Now;
                model.備註 ??= string.Empty;

                // 寫入資料庫
                _context.庫存盤點主檔.Update(model);
                int opCount = await _context.SaveChangesAsync();

                //model.庫存異動狀態 
                //model.庫存異動狀態 ??= string.Empty;
                //model.備註 ??= string.Empty;
                //model.備註 ??= string.Empty;

                _context.庫存盤點主檔.Update(model);
                Debug.WriteLine("[Edit] ▶ 已加入 Update 追蹤");

                //int opCount = await _context.SaveChangesAsync();
                Debug.WriteLine($"[Edit] ▶ SaveChanges 完成，受影響筆數：{opCount}");

                if (opCount > 0)
                {
                    Debug.WriteLine("[Edit] [OK] 資料更新成功");
                    return Ok(new ReturnData(ReturnState.ReturnCode.OK)
                    {
                        data = postData
                    });
                }
            }
            catch (Exception ex)
            {
                var realEx = ex.GetOriginalException();
                Debug.WriteLine($"[Edit] [ERROR] 發生例外：{realEx.ToMeaningfulMessage()}");
                return CreatedAtAction(nameof(Edit), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
                {
                    message = realEx.ToMeaningfulMessage()
                });
            }

            Debug.WriteLine("[Edit] [ERROR] 未發生例外但未成功儲存任何資料");
            return CreatedAtAction(nameof(Edit), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
            {
                message = "發生未知錯誤，請聯絡管理員"
            });
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



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ProcUseRang(ProcNo, ProcUseRang.Update)]
        //public async Task<IActionResult> Edit([Bind("進銷存組織,單據別,日期,流水號,倉庫代號,盤點種類,災害別,盤點人,盤點日期,備註")] HW_01_庫存盤點主檔EditViewModel postData)
        //{
        //    if (!ModelState.IsValid)
        //        return Ok(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR) { data = ModelState.ToErrorInfos() });

        //    try
        //    {
        //        var model = _mapper.Map<庫存盤點主檔>(postData);
        //        var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));
        //        model.修改人 = ua.UserNo;
        //        model.修改日期時間 = DateTime.Now;

        //        _context.庫存盤點主檔.Update(model);
        //        int opCount = await _context.SaveChangesAsync();

        //        if (opCount > 0)
        //            return Ok(new ReturnData(ReturnState.ReturnCode.OK) { data = postData });
        //    }
        //    catch (Exception ex)
        //    {
        //        return CreatedAtAction(nameof(Edit), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
        //        {
        //            message = ex.GetOriginalException().ToMeaningfulMessage()
        //        });
        //    }

        //    return CreatedAtAction(nameof(Edit), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
        //    {
        //        message = "更新失敗"
        //    });
        //}
        //[ProcUseRang(ProcNo, ProcUseRang.Delete)]
        //public async Task<ActionResult> Delete(string 進銷存組織, string 單據別, DateTime 日期, decimal 流水號)
        //{
        //    if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 日期 == default || 流水號 == default)
        //    {
        //        return NotFound(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));
        //    }

        //    var viewModel = await GetBaseQuery()
        //        .Where(s => s.進銷存組織 == 進銷存組織
        //                 && s.單據別 == 單據別
        //                 && s.日期 == 日期
        //                 && s.流水號 == 流水號)
        //        .SingleOrDefaultAsync();

        //    var (org, period, date, formate_date, userNo, biz, dept, div, branch) = InitInventoryDefaultValues();

        //    ViewBag.var列帳日期 = date;
        //    if (viewModel == null)
        //    {
        //        return NotFound(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));
        //    }

        //    return PartialView(viewModel); // 回傳 Delete.cshtml 的 PartialView
        //}
        public async Task<ActionResult> Delete(string 進銷存組織, string 單據別, DateTime 日期, decimal 流水號)
        {
            Debug.WriteLine("[Delete] ▶ 收到刪除請求：");
            Debug.WriteLine($"    進銷存組織 = {進銷存組織}");
            Debug.WriteLine($"    單據別     = {單據別}");
            Debug.WriteLine($"    日期       = {日期:yyyy-MM-dd}");
            Debug.WriteLine($"    流水號     = {流水號}");

            //if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 日期 == default || 流水號 == default)
            //{
            //    Debug.WriteLine("[Delete] [ERROR] 參數為空或無效");
            //    return NotFound(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));
            //}
            if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 流水號 == default)
            {
                Debug.WriteLine("[Delete] [ERROR] 參數為空或無效");
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
                Debug.WriteLine("[Delete] [ERROR] 查無資料");
                return NotFound(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));
            }

            var (org, period, date, formate_date, userNo, biz, dept, div, branch) = InitInventoryDefaultValues();
            ViewBag.var列帳日期 = date;

            Debug.WriteLine("[Delete] ▶ 成功載入資料，準備回傳 PartialView");
            return PartialView(viewModel);
        }



        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[ProcUseRang(ProcNo, ProcUseRang.Delete)]
        //public async Task<IActionResult> DeleteConfirmed([Bind("進銷存組織,單據別,日期,流水號")] HW_01_庫存盤點主檔DisplayViewModel postData)
        //{
        //    Debug.WriteLine("[DeleteConfirmed] ▶ 收到刪除確認請求");

        //    if (postData == null)
        //    {
        //        Debug.WriteLine("[DeleteConfirmed] ❌ postData 為 null");
        //        return BadRequest(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR) { message = "參數為空" });
        //    }

        //    Debug.WriteLine($"    進銷存組織 = {postData.進銷存組織}");
        //    Debug.WriteLine($"    單據別     = {postData.單據別}");
        //    Debug.WriteLine($"    日期       = {postData.日期:yyyy-MM-dd}");
        //    Debug.WriteLine($"    流水號     = {postData.流水號}");

        //    if (string.IsNullOrWhiteSpace(postData.進銷存組織) || string.IsNullOrWhiteSpace(postData.單據別) || postData.日期 == default)
        //    {
        //        Debug.WriteLine("[DeleteConfirmed] ❌ 參數遺失或格式不正確");
        //        return NotFound(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));
        //    }

        //    try
        //    {
        //        var model = await _context.庫存盤點主檔
        //            .Where(x =>
        //                x.進銷存組織 == postData.進銷存組織 &&
        //                x.單據別 == postData.單據別 &&
        //                x.日期 == postData.日期 &&
        //                x.流水號 == postData.流水號)
        //            .SingleOrDefaultAsync();

        //        if (model == null)
        //        {
        //            Debug.WriteLine("[DeleteConfirmed] ❌ 查無資料");
        //            return NotFound(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));
        //        }

        //        Debug.WriteLine("[DeleteConfirmed] ✅ 成功找到資料，準備刪除");

        //        _context.庫存盤點主檔.Remove(model);
        //        int opCount = await _context.SaveChangesAsync();

        //        Debug.WriteLine($"[DeleteConfirmed] ✅ 刪除完成，筆數：{opCount}");

        //        if (opCount > 0)
        //            return Ok(new ReturnData(ReturnState.ReturnCode.OK));
        //    }
        //    catch (Exception ex)
        //    {
        //        var realEx = ex.GetOriginalException();
        //        Debug.WriteLine($"[DeleteConfirmed] ❌ 發生例外：{realEx.ToMeaningfulMessage()}");

        //        return CreatedAtAction(nameof(DeleteConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR)
        //        {
        //            message = realEx.ToMeaningfulMessage()
        //        });
        //    }

        //    Debug.WriteLine("[DeleteConfirmed] ❌ 未知錯誤或資料已不存在");
        //    return CreatedAtAction(nameof(DeleteConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR)
        //    {
        //        message = "資料已不存在"
        //    });
        //}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Delete)]
        public async Task<IActionResult> DeleteConfirmed([Bind("進銷存組織,單據別,日期,流水號")] HW_01_庫存盤點主檔DisplayViewModel postData)
        {
            Debug.WriteLine("[DeleteConfirmed] ▶ 收到刪除請求");

            if (postData == null ||
                string.IsNullOrWhiteSpace(postData.進銷存組織) ||
                string.IsNullOrWhiteSpace(postData.單據別) ||
                postData.日期 == default)
            {
                return BadRequest(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR) { message = "參數錯誤" });
            }

            try
            {
                var model = await _context.庫存盤點主檔
                    .Where(x =>
                        x.進銷存組織 == postData.進銷存組織 &&
                        x.單據別 == postData.單據別 &&
                        x.日期 == postData.日期 &&
                        x.流水號 == postData.流水號)
                    .SingleOrDefaultAsync();

                if (model == null)
                    return NotFound(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR) { message = "找不到資料" });

                var (org, period, date, formate_date, userNo, biz, dept, div, branch) = InitInventoryDefaultValues();
                if (model.庫存異動狀態 == "3")
                    return Ok(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR)
                    {
                        message = "資料已完成異動，無法刪除"
                    });
                var ua = HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));

                // 註記刪除主檔
                model.是否註記刪除 = true;
                model.修改人 = userNo;
                model.修改日期時間 = DateTime.Now;

                // 移除所有對應明細
                var details = await _context.庫存盤點明細
                    .Where(d =>
                        d.進銷存組織 == model.進銷存組織 &&
                        d.單據別 == model.單據別 &&
                        d.日期 == model.日期 &&
                        d.流水號 == model.流水號)
                    .ToListAsync();

                _context.庫存盤點明細.RemoveRange(details);

                await _context.SaveChangesAsync();

                Debug.WriteLine("[DeleteConfirmed] ✅ 成功註記刪除並移除明細");

                return Ok(new ReturnData(ReturnState.ReturnCode.OK));
            }
            catch (Exception ex)
            {
                var realEx = ex.GetOriginalException();
                return CreatedAtAction(nameof(DeleteConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR)
                {
                    message = realEx.ToMeaningfulMessage()
                });
            }
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
        [HttpPost]
        public async Task<IActionResult> CheckHasDetail([FromBody] HW_01_庫存盤點主檔DisplayViewModel key)
        {
            var count = await _context.庫存盤點明細
                .CountAsync(x =>
                    x.進銷存組織 == key.進銷存組織 &&
                    x.單據別 == key.單據別 &&
                    x.日期.Date == key.日期.Date &&
                    x.流水號 == key.流水號);

            return Ok(new { count });
        }


        //[ProcUseRang(ProcNo, ProcUseRang.Create)]
        //[HttpGet]
        //[ProcUseRang(ProcNo, ProcUseRang.Add)]
        public async Task<IActionResult> CreateMultiInput(string 進銷存組織, string 單據別, DateTime 日期, int 流水號, string 倉庫代號)
        {
            try
            {
                var (org, _, 列帳日, 列帳日格式化, userNo, biz, dept, div, branch) = InitInventoryDefaultValues();

                Debug.WriteLine("┌──────────────────────────────────────┐");
                Debug.WriteLine($"│ [CreateMultiInput] ▶ 使用者帳號   = {userNo}");
                Debug.WriteLine($"│                          組織代號 = {org}");
                Debug.WriteLine($"│                          列帳日期 = {列帳日:yyyy-MM-dd}");
                Debug.WriteLine($"│                          事業別   = {biz}");
                Debug.WriteLine($"│                          單位     = {dept}");
                Debug.WriteLine($"│                          部門     = {div}");
                Debug.WriteLine($"│                          分部     = {branch}");
                Debug.WriteLine($"│                          倉庫代號     = {倉庫代號}");
                Debug.WriteLine("└──────────────────────────────────────┘");

                //ViewBag.var列帳日期 = 列帳日;
                // Controller
                //ViewBag.var列帳日期 = 列帳日.ToString("yyyy/MM/dd"); // 不送 DateTime，直接送格式化字串
                //ViewBag.var列帳日期 = 列帳日?.ToString("yyyy/MM/dd", CultureInfo.GetCultureInfo("zh-TW"));
                ViewBag.var列帳日期 = 列帳日;

                ViewBag.var進銷存組織 = org;
                ViewBag.var倉庫代號 = 倉庫代號;

                var warehouse = await _context.倉庫基本檔
    .Where(x => x.倉庫代號 == 倉庫代號)
    .Select(x => x.倉庫簡稱)
    .FirstOrDefaultAsync();

                ViewBag.倉庫簡稱 = warehouse ?? "(查無簡稱)";
                // 取得商品選項（目前為基礎條件）
                var 品項選項 = await Get品項類別選項Async(biz);
                //ViewBag.品項選項 = 品項選項;
                ViewBag.品項選項 = (List<SelectListItem>)await Get品項類別選項Async(biz);

                Debug.WriteLine($"[CreateMultiInput] ▶ 商品選項筆數 = {品項選項.Count}");

                // 建立初始 ViewModel
                var vm = new HW_01_庫存盤點明細檔CreateViewModel
                {
                    進銷存組織 = 進銷存組織,
                    單據別 = 單據別,
                    日期 = 日期,
                    流水號 = 流水號,
                    倉庫代號 = 倉庫代號, // ✅ 傳入 ViewModel
                    項次 = 0,
                    商品編號 = "",
                    盤點數量 = 0
                };

                Debug.WriteLine("┌──────────── ViewModel 建立完成 ────────────┐");
                Debug.WriteLine($"│ 進銷存組織 = {vm.進銷存組織}");
                Debug.WriteLine($"│ 單據別     = {vm.單據別}");
                Debug.WriteLine($"│ 日期       = {vm.日期:yyyy-MM-dd}");
                Debug.WriteLine($"│ 流水號     = {vm.流水號}");
                Debug.WriteLine($"│ 項次       = {vm.項次}");
                Debug.WriteLine("└───────────────────────────────────────────┘");

                return PartialView(vm);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CreateMultiInput] ❌ 發生錯誤：{ex.Message}");
                return StatusCode(500, "CreateMultiInput 錯誤：" + ex.GetOriginalException().Message);
            }

        }


        private async Task<List<SelectListItem>> Get品項類別選項Async(string biz)
        {
            // 先查詢符合事業的商品清單
            var 商品清單 =   _context.事業商品檔
                .Where(x => x.事業 == biz)
    .AsEnumerable() // 🔍 ← 這行是關鍵！把資料拉進記憶體處理 GroupBy
    .GroupBy(x => x.商品編號)
    .Select(g => g.OrderBy(x => x.商品簡稱).First()) // 每個商品編號只取第一筆（以名稱排序）
    .OrderBy(x => x.商品編號)
    .Select(x => new SelectListItem
    {
        Text = x.商品編號 + "_" + x.商品簡稱,
        Value = x.商品編號
    }).ToList(); // ← ❌ 這裡錯，因為上面是 AsEnumerable()

            // ⬇️ 改為 ToList()
            var 品項類別選項 = 商品清單.ToList();

            // 除錯輸出
            Debug.WriteLine($"[Get品項類別選項Async] ▶ 查詢條件：事業 = {biz}");
            Debug.WriteLine($"[Get品項類別選項Async] ▶ 商品筆數 = {品項類別選項.Count}");
            foreach (var item in 品項類別選項)
            {
                Debug.WriteLine($"[Get品項類別選項Async] ▶ 商品選項：Value={item.Value}, Text={item.Text}");
            }

            // 插入第一筆提示
            品項類別選項.Insert(0, new SelectListItem { Text = "--請選擇--", Value = "" });

            return 品項類別選項;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMultiInput([FromBody] HW_01_庫存盤點品項SubmitViewModel postData)
        {
            try
            {
                var (org, _, 列帳日, 列帳日格式化, userNo, biz, dept, div, branch) = InitInventoryDefaultValues();
 
                var now = DateTime.Now;

                // 確保至少有一筆資料
                if (postData.選項清單 == null || !postData.選項清單.Any())
                    return BadRequest("請至少加入一筆盤點品項");

                // 取得目前最大項次
                var maxItemNo = await _context.庫存盤點明細
                    .Where(x =>
                        x.進銷存組織 == postData.進銷存組織 &&
                        x.單據別 == postData.單據別 &&
                        x.日期 == postData.日期 &&
                        x.流水號 == postData.流水號)
                    .Select(x => (int?)x.項次)
                    .MaxAsync() ?? 0;

                Debug.WriteLine($"[CreateMultiInput][POST] ▶ 當前最大項次 = {maxItemNo}");

                var newItems = new List<庫存盤點明細>();

                foreach (var (item, idx) in postData.選項清單.Select((val, i) => (val, i)))
                {
                    var stockQty = await _context.庫存日檔
                        .Where(x =>
                            x.倉庫組織 == postData.進銷存組織 &&
x.倉庫代號 == postData.倉庫代號 &&
x.日期 == postData.日期 &&
x.商品編號 == item.商品編號
)
                        .Select(x => x.本日結存數量)
                        .FirstOrDefaultAsync();

                    var entity = new 庫存盤點明細
                    {
                        進銷存組織 = postData.進銷存組織,
                        單據別 = postData.單據別,
                        日期 = postData.日期,
                        流水號 = postData.流水號,
                        項次 = maxItemNo + idx + 1,
                        商品編號 = item.商品編號,
                        庫存數量 = stockQty,
                        盤點數量 = 0,
                        修改人 = userNo,
                        修改日期時間 = now
                    };

                    newItems.Add(entity);
                }

                await _context.庫存盤點明細.AddRangeAsync(newItems);
                await _context.SaveChangesAsync();

                Debug.WriteLine($"[CreateMultiInput][POST] ✅ 寫入完成，共 {newItems.Count} 筆");

                return Ok(new ReturnData(ReturnState.ReturnCode.OK)
                {
                    message = $"成功新增 {newItems.Count} 筆盤點明細資料"
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CreateMultiInput][POST] ❌ 發生錯誤：{ex.Message}");
                return StatusCode(500, "CreateMultiInput 儲存錯誤：" + ex.GetOriginalException().Message);
            }
        }


    }
}
