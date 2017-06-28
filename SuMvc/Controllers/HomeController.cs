using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuMvc.Core.Domain.Tests;
using SuMvc.Services.Test;

namespace SuMvc.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestService _testService;

        public HomeController(
            ITestService testService)
        {
            _testService = testService;
        }

        // GET: Home
        public ActionResult Index()
        {
            _testService.InsertTest(new TestEtity(){Name = "测试"});
            return View();
        }
    }
}