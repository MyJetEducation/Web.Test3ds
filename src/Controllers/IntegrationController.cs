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
				OkUrl = request.OkUrl,
				Date = DateTime.Now
			});

			return new DepositRegisterResponse
			{
				State = "accept",
				ExternalId = externalId,
				RedirectUrl = $"https://test-3ds.dfnt.work/?id={externalId}"
			};
		}

		[AllowAnonymous]
		[HttpGet("register2")]
		public DepositRegisterResponse DepositRegister2([FromQuery] DepositRegisterRequest2 request)
		{
			return new DepositRegisterResponse
			{
				State = "approve",
				ExternalId = NewUid
			};
		}

		[AllowAnonymous]
		[HttpGet("register3")]
		public DepositRegisterResponse DepositRegister3([FromQuery] DepositRegisterRequest3 request)
		{
			var id = request.TransactionId.ToString();
			DepositInfo info = _depositCache.Get(id);

			if (info == null)
			{
				_depositCache.Add(id, new DepositInfo
				{
					TransactionId = request.TransactionId,
					ExternalId = NewUid,
					Date = DateTime.Now
				});

				return new DepositRegisterResponse
				{
					State = "accept"
				};
			}

			if (DateTime.Now.Subtract(info.Date).Seconds < 10)
				return new DepositRegisterResponse
				{
					State = "accept"
				};

			return new DepositRegisterResponse
			{
				State = "approve",
				ExternalId = info.ExternalId
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