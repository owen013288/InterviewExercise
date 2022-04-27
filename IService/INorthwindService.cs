using Models;
using OneOf;
using OneOf.Types;
using System.Collections.Generic;

namespace IService
{
    /// <summary>
	/// 北風資料庫介面
	/// </summary>
    public interface INorthwindService
	{
		/// <summary>
		/// 取得所有Employees
		/// </summary>
		/// <returns></returns>
		OneOf<List<Employees>, NotFound, Error<string>> GetAllEmployees();

		/// <summary>
		/// 新增Employees
		/// </summary>
		/// <param name="model">Employees資料</param>
		/// <returns></returns>
		OneOf<Success, Error<string>> Add(Employees model);

		/// <summary>
		/// 更新Employees
		/// </summary>
		/// <param name="model">Employees資料</param>
		/// <returns></returns>
		OneOf<Success, NotFound, Error<string>> Update(Employees model);

		/// <summary>
		/// 透過ID取得單一Employee
		/// </summary>
		/// <param name="id">流水號</param>
		/// <returns></returns>
		OneOf<Employees, NotFound, Error<string>> GetEmployee(int id);
	}
}
