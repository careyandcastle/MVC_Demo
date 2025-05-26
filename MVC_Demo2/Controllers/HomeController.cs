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
        private readonly MvcDemoContext _mvcDemoContext;//0526 11:03 新增
                                                        //👉 這行是：定義一個資料庫連線變數，類型是你之前設定的 DbContext（叫 MvcDemoContext）
                                                        //它就像是「資料庫的遙控器」。
        public HomeController(ILogger<HomeController> logger, MvcDemoContext mvcDemoContext)//0526 11:03 新增 ", MvcDemoContext mvcDemoContext"
        {                                                                                   //👉 這是 建構子（Constructor），執行的時候 ASP.NET Core 會自動「把資料庫的服務實體塞進來」。
            _logger = logger;
            _mvcDemoContext = mvcDemoContext;//0526 11:03 新增
                                             //👉 這是把注入進來的物件存到你上面定義的 _mvcDemoContext 變數裡。
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //public IActionResult Create(Models.MvcDemoModel.車位資料檔 userInput) // 0526 01:33 👉 處理「使用者送出表單後的資料」
        public IActionResult Create_A_Car(Models.MvcDemoModel.車位資料檔 userInput) // 0526 01:37 改名Create為Create_A_Car (@@@2於Create.cshtml)
        {
            //寫DB
            //List<Models.MvcDemoModel.車位資料檔> carList = (from cars in _mvcDemoContext.車位資料檔 select cars).ToList(); // 0526 01:50 註解掉 
            //讀最新資料
            //return View("CarSeatList", carList); // 0526 01:47 根據CarSeatList.cshtml(此頁面是CarSeatList右鍵新增檢視所創建的)的取名// 0526 01:50 註解掉 

            // 0526 01:50 寫DB
            _mvcDemoContext.車位資料檔.Add(userInput);
            _mvcDemoContext.SaveChanges();
            // 0526 01:50 讀最新資料
            return Redirect("CarSeatList");

        }
        public IActionResult Create() // 0526 01:33 👉 顯示「空白的新增表單」
        {
            return View(); // ✅ 傳空資料進 View

        // =============================
        // 0526 13:20 新增 Razor 檢視頁面：Create.cshtml
        // 操作：Visual Studio → Controller 方法上右鍵 → Add View
        // =============================
        //
        // 檢視名稱 (View Name):
        //     Create
        //
        // 範本 (Template):
        //     Create （建立表單頁面樣板，自動產生表單輸入與驗證）
        //
        // 模型類別 (Model class):
        //     單位資料檔 (MVC_Demo2.Models.MvcDemoModel.單位資料檔)
        //     → 這是要建立資料的型別，對應資料表結構
        //
        // 資料內容類別 (Data context class):
        //     MvcDemoContext (MVC_Demo2.Models)
        //     → Entity Framework 用來存取資料庫的 DbContext 類別
        //
        // 選項：
        //     ☑ 使用版面配置頁 (Use a layout page)
        //         → 表示這個 View 會使用 _Layout.cshtml 套用共用版面
        //     ☐ 建立成局部檢視 (Partial View)
        //     ☐ 參考指令碼程式庫 (Reference script libraries)
        //
        // Scaffold 產出結果：
        //     - Views/Home/Create.cshtml：包含 Html.BeginForm、驗證與欄位綁定
        //     - 搭配 Controller 方法：public IActionResult Create() / [HttpPost] Create()
        // =============================

        //以上將會產生D:\每日資料\20250523_工作日\MVC\MVC_Demo2\MVC_Demo2\Views\Home\Create.cshtml

    }

    //0526-10:01
    public IActionResult CarSeatList()
        {
            //List<Models.MvcDemoModel.車位資料檔> carList = (from cars in _mvcDemoContext.車位資料檔
            //                                           select cars).ToList(); // 0526 11:21 講解了List的型別
            //                                                                  // 關於ICollection、IList各自的刪除增加方法
            List<Models.MvcDemoModel.車位資料檔> carList = (from cars in _mvcDemoContext.車位資料檔 select cars).ToList();

            /*0526 11:38
            ViewBag.myData = carList;
            ViewBag.myDataLength = carList.Count();

            11:45 俊智哥提醒: 這裡的變數一旦拿掉，就要對CarSeatList.cshtml的@{}部分做處理(不確定是甚麼處理，但看起來是轉型，對應改變@@@1)
            */


            return View(carList);
            //return View("Error"); //0526 11:38
        }


        public IActionResult Privacy()
        {
            return View();
            //10:59 F12解釋@model
            //IEnumberable 是列舉，是一筆一筆的資料，所以才能被走訪
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
