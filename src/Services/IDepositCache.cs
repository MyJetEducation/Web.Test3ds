namespace Web.Test3ds.Services
{
	public interface IDepositCache<T> where T : class, new()
	{
		void Add(string id, T data);

		T Get(string id);
	}
}