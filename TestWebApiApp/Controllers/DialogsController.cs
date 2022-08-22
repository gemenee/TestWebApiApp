using Microsoft.AspNetCore.Mvc;

using iMessengerCoreAPI.Models;

namespace TestWebApiApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DialogsController : ControllerBase
	{
		private readonly List<RGDialogsClients> _rGDialogsClients;

		public DialogsController(RGDialogsClients rGDialogsClients)
		{
			_rGDialogsClients = rGDialogsClients.Init();
		}

		/// <summary>
		/// Gets dialogs for provided clients
		/// </summary>
		/// <param name="clientIds"></param>
		/// <returns>Dialog id</returns>
		[HttpGet]
		public Guid Get([FromQuery] Guid[] clientIds)
		{
			var dialog = _rGDialogsClients
				.GroupBy(dc => dc.IDRGDialog)
				.ToDictionary(g => g.Key, g => g.Select(dc => dc.IDClient).ToList())
				.Where(d => d.Value.All(clientIds.Contains))
				.FirstOrDefault();

			return dialog.Key;
		}
	}
}
