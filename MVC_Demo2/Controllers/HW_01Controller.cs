using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TscLibCore.Commons;
using MVC_Demo2.Models.MvcDemoModel;
using MVC_Demo2.Models.ViewModel;
using TscLibCore.BaseObject;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Data;
using TscLibCore.FileTool;
using System.IO;
using TscLibCore.Modules;
using MVC_Demo2.Models;

namespace MVC_Demo2.Controllers
{
    [ProcUseRang(ProcNo, ProcUseRang.Menu)]
    [TypeFilter(typeof(BaseActionFilter))]
    public class HW_01Controller : Controller
    {
        //更新 ProcNo 與 Entity 基礎類型
        private readonly TRDBContext _context;
        private const string ProcNo = "HW_01";

        private static IConfigurationProvider _config;
        private static IMapper _mapper;

        public HW_01Controller(TRDBContext context)
        {
            _context = context;
            _config ??= new MapperConfiguration(cfg =>
            {
                //用於 ProjectTo<T>() 時的 EF Core 查詢投影（只需定義一次）
                cfg.CreateProjection<庫存盤點主檔, 庫存盤點主檔ViewModel>();
                //一般用於.Map() 時轉換資料（新增 / 編輯等）
                cfg.CreateMap<庫存盤點主檔, 庫存盤點主檔ViewModel>();
                cfg.CreateMap<庫存盤點主檔ViewModel, 庫存盤點主檔>();
                cfg.CreateMap<庫存盤點明細, 庫存盤點明細ViewModel>();
                cfg.CreateMap<庫存盤點明細ViewModel, 庫存盤點明細>();

            });

            _mapper ??= _config.CreateMapper();
        }

        public IActionResult Index()
        {
            ViewBag.TableFieldDescDict = new CreateTableFieldsDescription()
                .Create<庫存盤點主檔ViewModel, 庫存盤點明細ViewModel>();

            return View();
        }

        [HttpPost, ActionName("GetDataPost")]
        [ValidateAntiForgeryToken]
        [NeglectActionFilter]
        public async Task<IActionResult> GetData([FromBody] QueryConditions qc)
        {
            var sql = _context.庫存盤點主檔
                .AsNoTracking()
                .ProjectTo<庫存盤點主檔ViewModel>(_config);

            PaginatedList<庫存盤點主檔ViewModel> queryedData = await PaginatedList<庫存盤點主檔ViewModel>.CreateAsync(sql, qc);

            return Ok(new
            {
                data = queryedData,
                total = queryedData.TotalCount
            });
        }


        //[NeglectActionFilter]
        //public bool CanClickCreate(int index)
        //{
        //    return index % 2 == 0;
        //}



        [ProcUseRang(ProcNo, ProcUseRang.Add)]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Add)]
        public async Task<IActionResult> Create([FromBody] 庫存盤點主檔ViewModel postData)
        {
            // 清除欄位驗證（如有不需驗證欄位可加）
            ModelState.Remove("修改人");
            ModelState.Remove("修改時間");

            if (!ModelState.IsValid)
                return BadRequest(new ReturnData(ReturnState.ReturnCode.CREATE_ERROR));

            // 檢查主鍵是否已存在
            bool exists = _context.庫存盤點主檔.Any(x =>
                x.進銷存組織 == postData.進銷存組織 &&
                x.單據別 == postData.單據別 &&
                x.日期 == postData.日期 &&
                x.流水號 == postData.流水號);

            if (exists)
            {
                return BadRequest(new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
                {
                    message = "資料已存在，請勿重複新增。"
                });
            }

            // 映射並寫入
            庫存盤點主檔 entity = _mapper.Map<庫存盤點主檔>(postData);
            _context.庫存盤點主檔.Add(entity);

            try
            {
                var opCount = await _context.SaveChangesAsync();
                if (opCount > 0)
                    return Ok(new ReturnData(ReturnState.ReturnCode.OK) { data = postData });
            }
            catch (Exception ex)
            {
                return CreatedAtAction(nameof(Create), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
                {
                    message = ex.Message
                });
            }

            return CreatedAtAction(nameof(Create), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR));
        }

        public bool isMasterKeyExist(string 進銷存組織, string 單據別, DateTime 日期, decimal 流水號)
        {
            return _context.庫存盤點主檔.Any(m =>
                m.進銷存組織 == 進銷存組織 &&
                m.單據別 == 單據別 &&
                m.日期 == 日期 &&
                m.流水號 == 流水號);
        }

        public bool isDetailKeyExist(string 進銷存組織, string 單據別, DateTime 日期, int 流水號, int 項次)
        {
            return (_context.庫存盤點明細.Any(m =>
                m.進銷存組織 == 進銷存組織 &&
                m.單據別 == 單據別 &&
                m.日期 == 日期 &&
                m.流水號 == 流水號 &&
                m.項次 == 項次) == false);
        }
        //[ProcUseRang(ProcNo, ProcUseRang.Add)]
        //public IActionResult CreateMulti()
        //{
        //    return PartialView();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ProcUseRang(ProcNo, ProcUseRang.Add)]
        //public async Task<IActionResult> CreateMulti(List<庫存盤點主檔ViewModel> postData)
        //{
        //    //以下不驗證欄位值是否正確，請視欄位自行刪減
        //    for (int idx = 0; idx < postData.Count; idx++)
        //    {
        //        ModelState.Remove($"postData[{idx}].欄位1");
        //        ModelState.Remove($"postData[{idx}].欄位2");
        //        ModelState.Remove($"postData[{idx}].欄位3");

        //        //.....
        //        //...

        //        ModelState.Remove($"postData[{idx}].upd_usr");
        //        ModelState.Remove($"postData[{idx}].upd_dt");
        //    }

        //    if (ModelState.IsValid == false)
        //        return BadRequest(new ReturnData(ReturnState.ReturnCode.CREATE_ERROR));

        //    foreach (var item in postData)
        //    {
        //        /*
        //         *  Put Your Code Here.
        //         */
        //        庫存盤點主檔 filledData = _mapper.Map<庫存盤點主檔ViewModel, 庫存盤點主檔>(item);
        //        _context.Add(filledData);
        //    }

        //    try
        //    {
        //        var opCount = await _context.SaveChangesAsync();
        //        if (opCount > 0)
        //            return CreatedAtAction(nameof(CreateMulti), new ReturnData(ReturnState.ReturnCode.OK)
        //            {
        //                data = postData
        //            });
        //    }
        //    catch (Exception ex)
        //    {
        //        return CreatedAtAction(nameof(CreateMulti), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
        //        {
        //            message = ex.Message
        //        });
        //    }


        //    return CreatedAtAction(nameof(CreateMulti), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR));
        //}

        [ProcUseRang(ProcNo, ProcUseRang.Update)]
        public async Task<IActionResult> Edit(string 進銷存組織, string 單據別, DateTime 日期, int 流水號)
        {
            if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 日期 == default || 流水號 <= 0)
            {
                return NotFound(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR));
            }

            var result = await _context.庫存盤點主檔.FindAsync(進銷存組織, 單據別, 日期, 流水號);
            if (result == null)
            {
                return NotFound(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR));
            }

            var vm = _mapper.Map<庫存盤點主檔ViewModel>(result);
            return PartialView(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Update)]
        public async Task<IActionResult> Edit(string 進銷存組織, string 單據別, DateTime 日期, int 流水號, [FromBody] 庫存盤點主檔ViewModel postData)
        {
            if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 日期 == default || 流水號 <= 0)
            {
                return NotFound(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR));
            }

            if (進銷存組織 != postData.進銷存組織 || 單據別 != postData.單據別 || 日期 != postData.日期 || 流水號 != postData.流水號)
            {
                return BadRequest(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR));
            }

            if (!ModelState.IsValid)
                return BadRequest(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR));

            try
            {
                var entity = _mapper.Map<庫存盤點主檔>(postData);
                _context.Update(entity);
                var opCount = await _context.SaveChangesAsync();

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
                return CreatedAtAction(nameof(Edit), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
                {
                    message = ex.Message
                });
            }

            return CreatedAtAction(nameof(Edit), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR));
        }

        // 顯示刪除確認畫面用的 Action（非執行刪除邏輯）
        [ProcUseRang(ProcNo, ProcUseRang.Delete)]
        public async Task<IActionResult> Delete(string 進銷存組織, string 單據別, DateTime 日期, decimal 流水號)
        {
            if (進銷存組織 == null || 單據別 == null || 日期 == default || 流水號 == default)
            {
                return NotFound(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));
            }

            var result = await _context.庫存盤點主檔.FindAsync(進銷存組織, 單據別, 日期, 流水號);
            if (result == null)
            {
                return NotFound(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));
            }

            return PartialView(result); // 顯示確認視窗的畫面
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Delete)]
        public async Task<IActionResult> DeleteConfirmed(string 進銷存組織, string 單據別, DateTime 日期, decimal 流水號)
        {
            if (ModelState.IsValid == false)
                return BadRequest(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));

            var result = await _context.庫存盤點主檔.FindAsync(進銷存組織, 單據別, 日期, 流水號);
            if (result == null)
                return NotFound(new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));

            _context.庫存盤點主檔.Remove(result);

            try
            {
                var opCount = await _context.SaveChangesAsync();
                if (opCount > 0)
                    return Ok(new ReturnData(ReturnState.ReturnCode.OK));
            }
            catch (Exception ex)
            {
                return CreatedAtAction(nameof(DeleteConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));
            }

            return CreatedAtAction(nameof(DeleteConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));
        }

        [ProcUseRang(ProcNo, ProcUseRang.Export)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Export([FromBody] QueryConditions qc)
        {
            var sql = _context.庫存盤點主檔
                .AsNoTracking()
                .ProjectTo<庫存盤點主檔ViewModel>(_config);

            sql = sql.Where(qc.searchBy).PermissionFilter();

            bool hasSortKey = string.IsNullOrEmpty(qc.sortBy) == false;

            if (hasSortKey && qc.isDesc)
            {
                sql = sql.OrderByDescending(qc.sortBy);
            }
            else if (hasSortKey)
            {
                sql = sql.OrderBy(qc.sortBy);
            }

            DataTable dt = sql.ToDataTable();
            GenerateSheets gs = new GenerateSheets();
            MemoryStream memory = gs.DataTableToMemoryStream(dt);
            byte[] byteContent = memory.ToArray();
            memory.Close();

            return File(byteContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

 

        //=================================================================================================//

        /*
         * Details
         */
        #region detail

        [HttpPost, ActionName("GetDetailDataPost")]
        [ValidateAntiForgeryToken]
        [NeglectActionFilter]
        public async Task<IActionResult> GetDetails([FromBody] 庫存盤點主檔 keys)
        {
            if (string.IsNullOrEmpty(keys.進銷存組織) || string.IsNullOrEmpty(keys.單據別) || keys.日期 == default || keys.流水號 <= 0)
            {
                return NotFound(new ReturnData(ReturnState.ReturnCode.ERROR));
            }

            var query = from s in _context.庫存盤點明細
                        where s.進銷存組織 == keys.進銷存組織
                           && s.單據別 == keys.單據別
                           && s.日期 == keys.日期
                           && s.流水號 == keys.流水號
                        orderby s.項次
                        select s;

            return CreatedAtAction(nameof(GetDetails), new ReturnData(ReturnState.ReturnCode.OK)
            {
                data = await query.ToListAsync()
            });
        }



        [ProcUseRang(ProcNo, ProcUseRang.Add)]
        public async Task<IActionResult> CreateDetail(string 進銷存組織, string 單據別, DateTime 日期, decimal 流水號, int 項次)
        {
            if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 日期 == default || 流水號 == default)
            {
                return NotFound(new ReturnData(ReturnState.ReturnCode.ERROR));
            }

            var item = await _context.庫存盤點主檔.FindAsync(進銷存組織, 單據別, 日期, 流水號);

            if (item == null)
            {
                return NotFound();
            }

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Add)]
        public async Task<IActionResult> CreateDetail([Bind("進銷存組織,單據別,日期,流水號,項次,商品編號,庫存數量,盤點數量,修改人,修改日期時間")] 庫存盤點明細ViewModel postData)
        {
            if (ModelState.IsValid == false)
                return BadRequest(new ReturnData(ReturnState.ReturnCode.CREATE_ERROR));

            var filledData = _mapper.Map<庫存盤點明細ViewModel, 庫存盤點明細>(postData);
            _context.Add(filledData);

            try
            {
                var opCount = await _context.SaveChangesAsync();
                if (opCount > 0)
                    return Ok(new ReturnData(ReturnState.ReturnCode.OK)
                    {
                        data = postData
                    });
            }
            catch (Exception ex)
            {
                return CreatedAtAction(nameof(CreateDetail), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
                {
                    message = ex.Message
                });
            }

            return CreatedAtAction(nameof(CreateDetail), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR));
        }

        public IActionResult CreateMultiDetails()
        {
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Add)]
        public async Task<IActionResult> CreateMultiDetails(List<庫存盤點明細ViewModel> postData)
        {
            if (ModelState.IsValid == false)
                return BadRequest(new ReturnData(ReturnState.ReturnCode.CREATE_ERROR));

            foreach (var item in postData)
            {
                // 避免主鍵重複（可依需求保留或略過）
                bool exist = await _context.庫存盤點明細.AnyAsync(m =>
                    m.進銷存組織 == item.進銷存組織 &&
                    m.單據別 == item.單據別 &&
                    m.日期 == item.日期 &&
                    m.流水號 == item.流水號 &&
                    m.項次 == item.項次);

                if (exist)
                    continue; // 或 return Conflict(...) 視情況

                庫存盤點明細 entity = _mapper.Map<庫存盤點明細>(item);
                _context.庫存盤點明細.Add(entity);
            }

            try
            {
                var opCount = await _context.SaveChangesAsync();
                if (opCount > 0)
                    return CreatedAtAction(nameof(CreateMultiDetails), new ReturnData(ReturnState.ReturnCode.OK)
                    {
                        data = postData
                    });
            }
            catch (Exception ex)
            {
                return CreatedAtAction(nameof(CreateMultiDetails), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR)
                {
                    message = ex.Message
                });
            }

            return CreatedAtAction(nameof(CreateMultiDetails), new ReturnData(ReturnState.ReturnCode.CREATE_ERROR));
        }


        [ProcUseRang(ProcNo, ProcUseRang.Update)]
        public async Task<IActionResult> EditDetail(string 進銷存組織, string 單據別, DateTime 日期, decimal 流水號, int 項次)
        {
            if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 日期 == default || 流水號 == default || 項次 == default)
            {
                return NotFound(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR));
            }

            var result = await _context.庫存盤點明細.FindAsync(進銷存組織, 單據別, 日期, 流水號, 項次);
            if (result == null)
            {
                return NotFound(new ReturnData(ReturnState.ReturnCode.EDIT_ERROR));
            }

            return PartialView(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Update)]
        public async Task<IActionResult> EditDetail(string 進銷存組織, string 單據別, DateTime 日期, decimal 流水號, int 項次, [Bind("進銷存組織,單據別,日期,流水號,項次,商品編號,庫存數量,盤點數量,修改人,修改日期時間")] 庫存盤點明細ViewModel postData)
        {
            if (ModelState.IsValid == false)
                return CreatedAtAction(nameof(EditDetail), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR));

            if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 日期 == default || 流水號 == default || 項次 == default)
                return CreatedAtAction(nameof(EditDetail), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR));

            var filledData = _mapper.Map<庫存盤點明細ViewModel, 庫存盤點明細>(postData);
            _context.Update(filledData);

            try
            {
                var opCount = await _context.SaveChangesAsync();
                if (opCount > 0)
                    return CreatedAtAction(nameof(EditDetail), new ReturnData(ReturnState.ReturnCode.OK)
                    {
                        data = postData
                    });
            }
            catch (Exception ex)
            {
                return CreatedAtAction(nameof(EditDetail), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR)
                {
                    message = ex.Message
                });
            }

            return CreatedAtAction(nameof(EditDetail), new ReturnData(ReturnState.ReturnCode.EDIT_ERROR));
        }

        [ProcUseRang(ProcNo, ProcUseRang.Delete)]
        public async Task<IActionResult> DeleteDetail(string 進銷存組織, string 單據別, DateTime 日期, decimal 流水號, int 項次)
        {
            if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 日期 == default || 流水號 == default || 項次 == default)
            {
                return NotFound();
            }

            var result = await _context.庫存盤點明細.FindAsync(進銷存組織, 單據別, 日期, 流水號, 項次);

            if (result == null)
            {
                return NotFound();
            }

            return PartialView(result);
        }

        [HttpPost, ActionName("DeleteDetail")]
        [ValidateAntiForgeryToken]
        [ProcUseRang(ProcNo, ProcUseRang.Delete)]
        public async Task<IActionResult> DeleteDetailConfirmed(string 進銷存組織, string 單據別, DateTime 日期, decimal 流水號, int 項次)
        {
            if (ModelState.IsValid == false)
                return CreatedAtAction(nameof(DeleteDetailConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));

            if (string.IsNullOrEmpty(進銷存組織) || string.IsNullOrEmpty(單據別) || 日期 == default || 流水號 == default || 項次 == default)
                return CreatedAtAction(nameof(DeleteDetailConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));

            var result = await _context.庫存盤點明細.FindAsync(進銷存組織, 單據別, 日期, 流水號, 項次);
            if (result == null)
                return CreatedAtAction(nameof(DeleteDetailConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));

            _context.庫存盤點明細.Remove(result);

            try
            {
                var opCount = await _context.SaveChangesAsync();
                if (opCount > 0)
                    return CreatedAtAction(nameof(DeleteDetailConfirmed), new ReturnData(ReturnState.ReturnCode.OK));
            }
            catch (Exception ex)
            {
                return CreatedAtAction(nameof(DeleteDetailConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR)
                {
                    message = ex.Message
                });
            }

            return CreatedAtAction(nameof(DeleteDetailConfirmed), new ReturnData(ReturnState.ReturnCode.DELETE_ERROR));
        }
 
        #endregion
    }
}