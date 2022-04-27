using IService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Service
{
	/// <summary>
	/// 倉儲實作
	/// </summary>
	/// <typeparam name="T">class</typeparam>
	public class Repository<T> : IRepository<T> where T : class
	{
		DbContext DbContext { get; set; }
		protected DbSet<T> DbSet { get; set; }

		public Repository(DbContext context)
		{
			DbContext = context ?? throw new ArgumentException(nameof(context));
			DbSet = DbContext.Set<T>();
		}

		/// <summary>
		/// 刪除單一實體
		/// </summary>
		/// <param name="entity">實體</param>
		public void Delete(T entity)
		{
			DbSet.Remove(entity);
		}

		/// <summary>
		/// 釋放資源
		/// </summary>
		public void Dispose()
		{
			DbContext?.Dispose();
		}

		/// <summary>
		/// 取得List
		/// </summary>
		/// <param name="predicate">搜尋條件</param>
		/// <param name="orderBy">排序</param>
		/// <param name="pagination">分頁</param>
		/// <returns></returns>
		public List<T> GetList()
		{
			IQueryable<T> query = DbSet;

			query = query.AsNoTracking();

			return query.ToList();
		}

		/// <summary>
		/// 新增單一實體
		/// </summary>
		/// <param name="entity">實體</param>
		/// <returns></returns>
		public T Insert(T entity)
		{
			return DbSet.Add(entity);
		}

		/// <summary>
		/// 新增多個實體
		/// </summary>
		/// <param name="entities">實體</param>
		public void Insert(params T[] entities)
		{
			DbSet.AddRange(entities);
		}

		/// <summary>
		/// 新增多個實體
		/// </summary>
		/// <param name="entities">實體</param>
		public void Insert(IEnumerable<T> entities)
		{
			DbSet.AddRange(entities);
		}

		/// <summary>
		/// 取得單一實體
		/// </summary>
		/// <param name="predicate">搜尋條件</param>
		/// <param name="orderBy">排序</param>
		/// <param name="enableTracking">啟用跟踪</param>
		/// <returns></returns>
		public T SingleOrDefault(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool enableTracking = true)
		{
			IQueryable<T> query = DbSet;

			if (!enableTracking) query = query.AsNoTracking();

			if (predicate != null) query = query.Where(predicate);

			return orderBy != null ? orderBy(query).FirstOrDefault() : query.FirstOrDefault();
		}

		/// <summary>
		/// 更新單一實體
		/// </summary>
		/// <param name="entity">實體</param>
		/// <param name="key">資料的PK</param>
		public void Update(T entity, object key)
		{
			if (entity == null) return;

			T existing = DbSet.Find(key);

			if (existing != null)
			{
				DbContext.Entry(existing).CurrentValues.SetValues(entity);
			}
		}
	}
}
