using IService;
using System.Web.Mvc;

namespace InterviewExercise.Controllers
{
    public class HomeController : Controller
    {
		readonly INorthwindService _northwindService;
		public HomeController(INorthwindService northwindService)
		{
			_northwindService = northwindService;
		}

		#region Index [Employees首頁]
		/// <summary>
		/// Employees首頁
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			var result = _northwindService.GetAllEmployees();

			return result.Match(
				employees =>
                {
					return (ActionResult)View(employees);
				},
				notFound =>
				{
					TempData["ErrorMessage"] = "查無資料";
					return Json(new { }, JsonRequestBehavior.AllowGet);
				},
				err =>
				{
					return Json(err.Value, JsonRequestBehavior.AllowGet);
				}
			);
		}
		#endregion
	}
}