using IService;
using Models;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Service
{
	/// <summary>
	/// 北風資料庫實作
	/// </summary>
    public class NorthwindService : INorthwindService
	{
		readonly IUnitOfWork<NorthwindEntities> _unitOfWork;
		readonly IRepository<Employees> _employeesRepo;

		public NorthwindService(NorthwindEntities entities)
		{
			_unitOfWork = new UnitOfWork<NorthwindEntities>(entities);
			_employeesRepo = _unitOfWork.GetRepository<Employees>();
		}

		#region GetAllEmployees [取得所有Employees]
		/// <summary>
		/// 取得所有Employees
		/// </summary>
		/// <returns></returns>
		public OneOf<List<Employees>, NotFound, Error<string>> GetAllEmployees()
		{
			try
			{
				Expression<Func<Employees, Employees>> query = e => e.Employees2;

				var Employees = _employeesRepo.GetList(query);

				if (Employees == null) return new NotFound();

				return Employees;
			}
			catch (Exception e)
			{
				return new Error<string>(e.Message);
			}
		}
		#endregion

		#region Add [新增Employees]
		/// <summary>
		/// 新增Employees
		/// </summary>
		/// <param name="model">Employees資料</param>
		/// <returns></returns>
		public OneOf<Success, Error<string>> Add(Employees model)
		{
			try
			{
				// 時間關係就多餘的自動帶入
				model.HireDate = DateTime.Now;
				model.Address = "地址";
				model.City = "縣市";
				model.Region = "區域";
				model.PostalCode = "郵遞區號";
				model.Country = "國家";
				model.HomePhone = "家電";
				model.Extension = "延期";
				model.Photo = null;
				model.Notes = "筆記";
				model.ReportsTo = 2;
				model.PhotoPath = "圖片網址";
				_employeesRepo.Insert(model);

				bool result = _unitOfWork.Commit() > 0;

				if (!result) return new Error<string>("新增失敗");

				return new Success();
			}
			catch (Exception e)
			{
				return new Error<string>(e.Message);
			}
		}
		#endregion
	}
}
