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

		#region
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
    }
}
