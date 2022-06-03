using Dashboard.Core.Caching;
using Microsoft.AspNetCore.Mvc;
using RedisPerformance.Core;
using RedisPerformance.Models;
using System.Diagnostics;
using static RedisPerformance.Core.Enums;

namespace RedisPerformance.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRedisCacheService _redisCacheManager;
        public HomeController(ILogger<HomeController> logger, IRedisCacheService redisCacheManager)
        {
            _redisCacheManager = redisCacheManager;
            _logger = logger;
        }       
        public IActionResult Index()
        {          
            return View();
        }

        [ServiceFilter(typeof(PermissonFilter))]
        [SecurityActionAttribute((int)HomeControllers.UserPermission, (Int64)UserPermissionActions.GetRolePermissionsById)]
        [HttpGet("Index/{userID}")]
        public IActionResult Index(int userID)
        {
            _redisCacheManager.Set("Name", "Mustafa Ayaz");
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