using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Test3ds.Models;
using Web.Test3ds.Services;

namespace Web.Test3ds.Pages
{
	public class IndexModel : PageModel
	{
		private readonly IDepositCache<DepositInfo> _depositCache;

		public IndexModel(IDepositCache<DepositInfo> depositCache) => _depositCache = depositCache;

		public void OnGet([Required, FromQuery(Name = "id")] string id)
		{
			if (id == null)
				throw new Exception("No id");

			DepositInfo info = _depositCache.Get(id);
			if (info == null)
				throw new Exception("No data");

			Input = new InputModel
			{
				TransactionId = info.TransactionId,
				ExternalId = info.ExternalId,
				OkUrl = info.OkUrl,
				FailUrl = info.FailUrl,
				Info = info.Info
			};
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel
		{
			[Required, FromQuery(Name = "transaction-id")]
			public Guid? TransactionId { get; set; }

			[Required, FromQuery(Name = "external-id")]
			public string ExternalId { get; set; }

			[Required, FromQuery(Name = "info")]
			public string Info { get; set; }

			[Required, FromQuery(Name = "ok-url")]
			public string OkUrl { get; set; }

			[Required, FromQuery(Name = "fail-url")]
			public string FailUrl { get; set; }
		}
	}
}