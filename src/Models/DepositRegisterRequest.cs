using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Web.Test3ds.Models
{
	public class DepositRegisterRequest
	{
		[Required, FromQuery(Name = "transaction-id")]
		public Guid TransactionId { get; set; }

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