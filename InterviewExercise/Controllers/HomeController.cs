using System.Web.Mvc;

namespace InterviewExercise.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}