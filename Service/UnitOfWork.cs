using IService;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// 工作單位實作(連接)
    /// </summary>
    /// <typeparam name="T">DbContext</typeparam>
    public class UnitOfWork<T> : IUnitOfWork<T> where T : DbContext
	{
		public T Context { get; }
		public UnitOfWork(T context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		/// <summary>
		/// 儲存資料
		/// </summary>
		/// <returns></returns>
		public int Commit()
		{
			return Context.SaveChanges();
		}

		/// <summary>
		/// 非同步儲存資料
		/// </summary>
		/// <returns></returns>
		public Task<int> CommitAsync()
		{
			return Context.SaveChangesAsync();
		}

		/// <summary>
		/// 釋放資源
		/// </summary>
		public void Dispose()
		{
			Context?.Dispose();
		}

		/// <summary>
		/// 取得倉儲
		/// </summary>
		/// <typeparam name="TEntity">class</typeparam>
		/// <returns></returns>
		public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
		{
			return new Repository<TEntity>(Context);
		}
	}
}
