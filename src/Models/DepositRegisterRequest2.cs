using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Web.Test3ds.Models
{
	public class DepositRegisterRequest2
	{
		[Required, FromQuery(Name = "transaction-id")]
		public Guid TransactionId { get; set; }
	}
}