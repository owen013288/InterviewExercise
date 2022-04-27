﻿using Models;
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
	}
}
