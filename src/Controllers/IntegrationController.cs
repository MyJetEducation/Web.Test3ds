using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Test3ds.Models;
using Web.Test3ds.Services;

namespace Web.Test3ds.Controllers
{
	[ApiController]
	[Route("/deposit")]
	public class IntegrationController : ControllerBase
	{
		private readonly IDepositCache<DepositInfo> _depositCache;

		public IntegrationController(IDepositCache<DepositInfo> depositCache) => _depositCache = depositCache;

		[AllowAnonymous]
		[HttpGet("register")]
		public DepositRegisterResponse DepositRegister([FromQuery] DepositRegisterRequest request)
		{
			string externalId = NewUid;

			_depositCache.Add(externalId, new DepositInfo
			{
				TransactionId = request.TransactionId,
				CallbackUrl = request.CallbackUrl,
				ExternalId = externalId,
				Info = request.Info,
				FailUrl = request.FailUrl,
				OkUrl = request.OkUrl
			});

			return new DepositRegisterResponse
			{
				State = "accept",
				ExternalId = externalId,
				RedirectUrl = $"https://test-3ds.dfnt.work/?id={externalId}"
			};
		}

		[AllowAnonymous]
		[HttpGet("callback")]
		public string Callback(string id, string state)
		{
			DepositInfo depositInfo = _depositCache.Get(id);

			return $"{depositInfo.CallbackUrl}?transaction-id={depositInfo.TransactionId}&state={state}&external-id={depositInfo.ExternalId}";
		}

		private static string NewUid => Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
	}
}