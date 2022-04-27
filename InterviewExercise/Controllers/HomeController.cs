using IService;
using Models;
using System;
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
					return Content("查無資料");
				},
				err =>
				{
					return Content(err.Value);
				}
			);
		}
		#endregion

		#region Create [新增Employees]
		/// <summary>
		/// 新增Employees GET方法
		/// </summary>
		/// <returns></returns>
		public ActionResult Create()
		{
			return View();
		}

		/// <summary>
		/// 新增Employees POST方法
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Employees employees)
		{
			try
			{
				if (!ModelState.IsValid)
					return View(employees);

				var result = _northwindService.Add(employees);
				return result.Match(
					success =>
					{
						TempData["Status"] = "新增成功";
						return (ActionResult)RedirectToAction("Index");
					},
					err =>
					{
						TempData["Status"] = "新增失敗";
						return Content(err.Value);
					}
				);
			}
			catch (Exception e)
			{
				TempData["Status"] = "新增失敗";
				return Content(e.Message);
			}
		}
		#endregion
	}
}