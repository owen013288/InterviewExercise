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

		#region Edit [編輯Employee功能]
		/// <summary>
		/// 編輯Employee功能 GET方法
		/// </summary>
		/// <param name="id">流水號</param>
		/// <returns></returns>
		public ActionResult Edit(int id)
		{
			var result = _northwindService.GetEmployee(id);
			return result.Match(
				model =>
				{
					ViewBag.BirthDate = (model.BirthDate.Value.ToString().Contains("下午") ? ((DateTime)model.BirthDate).AddHours(12) : model.BirthDate).ToString().Replace(" 上午 ", "T").Replace(" 下午 ", "T");
                    return (ActionResult)View(model);
				},
				notFound =>
				{
					TempData["ErrorMessage"] = "無此筆資料";
					return (ActionResult)RedirectToAction("Index");
				},
				err =>
				{
					TempData["Status"] = "修改失敗";
					return (ActionResult)View(result);
				}
			);
		}

		/// <summary>
		/// 編輯Employee功能 POST方法
		/// </summary>
		/// <param name="model">Employee資料</param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Employees model)
		{
			try
			{
				if (!ModelState.IsValid)
					return View(model);

				var result = _northwindService.Update(model);

				return result.Match(
						success =>
						{
							return (ActionResult)RedirectToAction("Index");
						},
						notFound =>
						{
							TempData["ErrorMessage"] = "無此筆資料";
							return (ActionResult)RedirectToAction("Index");
						},
						err =>
						{
							TempData["Status"] = "修改失敗";
							return Content(err.Value);
						}
					);
			}
			catch (Exception e)
			{
				TempData["Status"] = "修改失敗";
				return Content(e.Message);
			}
		}
		#endregion

		#region Delete [刪除Employee功能]
		/// <summary>
		/// 刪除Employee功能
		/// </summary>
		/// <param name="id">流水號</param>
		/// <returns></returns>
		public ActionResult Delete(int id)
		{
			var result = _northwindService.DeleteEmployee(id);
			return result.Match(
				model =>
				{
					return (ActionResult)RedirectToAction("Index");
				},
				notFound =>
				{
					TempData["ErrorMessage"] = "無此筆資料";
					return (ActionResult)RedirectToAction("Index");
				},
				err =>
				{
					TempData["Status"] = "刪除失敗";
					return Content(err.Value);
				}
			);
		}
        #endregion
    }
}