// 10. defines UserManager, RoleManager
// 20. identity role
// 45. for HttpStatusCode
// 50. for Task<>
using igoryen.Models;
using igoryen.ViewModels;
using Microsoft.AspNet.Identity; // 10
using Microsoft.AspNet.Identity.EntityFramework; // 20
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace igoryen.Controllers {
  public class HomeController : Controller {

    //==================================================
    // Bring in namespaces
    //==================================================
    private DataContext db = new DataContext();
    private UserManager<ApplicationUser> manager;
    private Repo_Faculty rf = new Repo_Faculty();

    public ActionResult Index() {
      //return View();
      return View(rf.getListOfFacultyBase());
    }

    public ActionResult About() {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact() {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}