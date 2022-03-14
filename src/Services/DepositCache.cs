using System.Collections.Concurrent;

namespace Web.Test3ds.Services
{
	public class DepositCache<T> : IDepositCache<T> where T : class, new()
	{
		private static readonly ConcurrentDictionary<string, (T, DateTime)> Dictionary;

		static DepositCache() => Dictionary = new ConcurrentDictionary<string, (T, DateTime)>();

		public void Add(string id, T data)
		{
			Clean();

			Dictionary.TryRemove(id, out _);

			Dictionary.TryAdd(id, (data, DateTime.Now.AddHours(1)));
		}

		public T Get(string id) => Dictionary.TryGetValue(id, out (T, DateTime) value)
			? value.Item1
			: null;

		private void Clean()
		{
			KeyValuePair<string, (T, DateTime)>[] expiredPairs = Dictionary
				.Where(pair => pair.Value.Item2 < DateTime.Now)
				.ToArray();

			foreach (KeyValuePair<string, (T, DateTime)> pair in expiredPairs)
				Dictionary.TryRemove(pair);
		}
	}
}