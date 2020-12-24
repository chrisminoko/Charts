using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Charts.Models;
using Charts.Data;
using System.Globalization;

namespace Charts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger , ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult SalesDataYearWise()
        {
            using (_context)
            {
                var v = (from a in _context.salesRecords
                         group a by a.SaleDate.Year into g
                         select new
                         {
                             Year = g.Key,
                             Electronics = g.Sum(a => a.Electronics),
                             BookAndMedia = g.Sum(a => a.BookAndMedia),
                             HomeAndKitchen = g.Sum(a => a.HomeAndKitchen)
                         });
                if (v != null)
                {
                    var chartData = new object[v.Count() + 1];
                    chartData[0] = new object[]
                    {
                "Year",
                "Electronics",
                "Book And Media",
                "Home And Kitchen"
                    };
                    int j = 0;
                    foreach (var i in v)
                    {
                        j++;
                        chartData[j] = new object[] { i.Year.ToString(), i.Electronics, i.BookAndMedia, i.HomeAndKitchen };
                    }
                    return Json(chartData);
                }
            }
            return Json(null);
        }
        [HttpGet]
        public JsonResult SalesDataMonthWise(int year)
        {
            using (_context)
            {
                var v = (from a in _context.salesRecords
                         where a.SaleDate.Year.Equals(year)
                         group a by a.SaleDate.Month into g
                         select new
                         {
                             Month = g.Key,
                             Electronics = g.Sum(a => a.Electronics),
                             BookAndMedia = g.Sum(a => a.BookAndMedia),
                             HomeAndKitchen = g.Sum(a => a.HomeAndKitchen)
                         });
                if (v != null)
                {
                    var chartData = new object[12 + 1];
                    chartData[0] = new object[]
                    {
                "Month",
                "Electronics",
                "Book And Media",
                "Home And Kitchen"
                    };
                    for (int i = 1; i <= 12; i++)
                    {
                        string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                        var monthData = v.Where(a => a.Month.Equals(i)).FirstOrDefault();
                        if (monthData != null)
                        {
                            chartData[i] = new object[] { monthName, monthData.Electronics, monthData.BookAndMedia, monthData.HomeAndKitchen };
                        }
                        else
                        {
                            chartData[i] = new object[] { monthName, 0, 0, 0 };
                        }
                    }
                    return Json(chartData);
                }
            }
            return Json(null);
        }
        [HttpGet]
        public JsonResult SalesDataDayWise(int year, string month)
        {
            int monthNumber = DateTime.ParseExact(month, "MMMM", CultureInfo.InvariantCulture).Month;
            int days = DateTime.DaysInMonth(year, monthNumber);
            List<SalesRecords> sr = new List<SalesRecords>();
            using (_context)
            {
                sr = (from a in _context.salesRecords
                      where a.SaleDate.Year.Equals(year) && a.SaleDate.Month.Equals(monthNumber)
                      select a).ToList();
                if (sr != null)
                {
                    var chartData = new object[days + 1];
                    chartData[0] = new object[]
                    {
                "Month",
                "Electronics",
                "Book And Media",
                "Home And Kitchen"
                    };

                    for (int i = 1; i <= days; i++)
                    {
                        var daysData = sr.Where(a => a.SaleDate.Day.Equals(i)).FirstOrDefault();
                        if (daysData != null)
                        {
                            chartData[i] = new object[] { i.ToString(), daysData.Electronics, daysData.BookAndMedia, daysData.HomeAndKitchen };
                        }
                        else
                        {
                            chartData[i] = new object[] { i.ToString(), 0, 0, 0 };
                        }
                    }
                    return Json(chartData);
                }
            }
            return Json(null);
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
