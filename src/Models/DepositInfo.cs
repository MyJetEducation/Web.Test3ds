namespace Web.Test3ds.Models
{
	public class DepositInfo
	{
		public Guid? TransactionId { get; set; }

		public string ExternalId { get; set; }

		public string Info { get; set; }

		public string OkUrl { get; set; }

		public string FailUrl { get; set; }

		public string CallbackUrl { get; set; }

		public DateTime Date { get; set; }
	}
}