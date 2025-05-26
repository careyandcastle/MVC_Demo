using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_Demo2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Demo2.Controllers
{
    /*0526 
    //(十六) 於HomeController增加以下Attribute。 --頁面13


    //單一入口 --天培分享
    //[ProcUseRang("Home", ProcUseRang.Menu)]
    //[TypeFilter(typeof(BaseActionFilter))]
    
    
    // 五、功能權限 --頁面22
    //        (一) BaseActionFilter：
    //        1. 於Controller（內部所有Action）與Action上加入，加入後在進入Action前會進行以下判斷，未通過則返回單一入口：
    //            (1) 身分驗證。
    //            (2) 資安政策驗證。
    //            (3) 設定使用者資訊（UserAccountForSession）。
    //            (4) 不可跳轉至無權限之功能頁面(ProcUseRang)。
    //    (二) ProcUseRang：
    //        1. 於Controller（內部所有Action）與Action上加入，未通過則返回單一入口：
    //            (1) 代表此Controller/Action之功能代號，與對應之功能權限。
    //            (2) 若Controller與Action都有設定，則以Action為主。
    //        2. ProcUseRang("功能代號", ProcUseRang.對應權限(增刪修查匯))
    //        3. 可避免使用者使用無對應權限之功能。
    */
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //0526-10:01
        public IActionResult CarSeatList()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
