using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Test3ds.Pages
{
	public class IndexModel : PageModel
	{
		public void OnGet(
			[Required, FromQuery(Name = "transaction-id")] string transactionId, 
			[Required, FromQuery(Name = "ok-url")] string okUrl, 
			[Required, FromQuery(Name = "fail-url")] string failUrl,
			[Required, FromQuery(Name = "callback-url")] string callbackUrl)
		{
			Input = new InputModel
			{
				TransactionId = transactionId,
				OkUrl = okUrl,
				FailUrl = failUrl,
				CallbackUrl = callbackUrl
			};
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel
		{
			[Required, FromQuery(Name = "transaction-id")]
			public string TransactionId { get; set; }
			
			[Required, FromQuery(Name = "info")]
			public string Info { get; set; }

			[Required, FromQuery(Name = "ok-url")]
			public string OkUrl { get; set; }

			[Required, FromQuery(Name = "fail-url")]
			public string FailUrl { get; set; }

			[Required, FromQuery(Name = "callback-url")]
			public string CallbackUrl { get; set; }
		}
	}
}