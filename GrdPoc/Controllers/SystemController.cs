using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrdPoc.Controllers
{
    [Authorize]
    public class SystemController : Controller
    {
        // GET: System
        public ActionResult Index()
        {
            return View();
        }
    }
}