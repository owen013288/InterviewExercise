using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IService
{
    /// <summary>
    /// 倉儲介面
    /// </summary>
    /// <typeparam name="T">class</typeparam>
    public interface IRepository<T> : IDisposable where T : class
	{
		/// <summary>
		/// 取得單一實體
		/// </summary>
		/// <param name="predicate">搜尋條件</param>
		/// <param name="orderBy">排序</param>
		/// <param name="enableTracking">啟用跟踪</param>
		/// <returns></returns>
		T SingleOrDefault(Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			bool enableTracking = true);

		/// <summary>
		/// 新增單一實體
		/// </summary>
		/// <param name="entity">實體</param>
		/// <returns></returns>
		T Insert(T entity);

		/// <summary>
		/// 新增多個實體
		/// </summary>
		/// <param name="entities">實體</param>
		void Insert(params T[] entities);

		/// <summary>
		/// 新增多個實體
		/// </summary>
		/// <param name="entities">實體</param>
		void Insert(IEnumerable<T> entities);

		/// <summary>
		/// 更新單一實體
		/// </summary>
		/// <param name="entity">實體</param>
		/// <param name="key">資料的PK</param>
		void Update(T entity, object key);

		/// <summary>
		/// 刪除單一實體
		/// </summary>
		/// <param name="entity">實體</param>
		void Delete(T entity);

		/// <summary>
		/// 取得實體清單
		/// </summary>
		/// <param name="predicate">搜尋條件</param>
		/// <returns></returns>
		List<T> GetList(Expression<Func<T, T>> predicate);

	}
}
