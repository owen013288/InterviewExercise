using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace IService
{
    /// <summary>
    /// 工作單位介面(釋放資源)
    /// </summary>
    public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// 取得倉儲
		/// </summary>
		/// <typeparam name="TEntity">class</typeparam>
		/// <returns></returns>
		IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

		/// <summary>
		/// 儲存資料
		/// </summary>
		/// <returns></returns>
		int Commit();

		/// <summary>
		/// 非同步儲存資料
		/// </summary>
		/// <returns></returns>
		Task<int> CommitAsync();
	}

	/// <summary>
	/// 工作單位介面(連接)
	/// </summary>
	/// <typeparam name="T">DbContext</typeparam>
	public interface IUnitOfWork<T> : IUnitOfWork where T : DbContext
	{
		T Context { get; }
	}
}
