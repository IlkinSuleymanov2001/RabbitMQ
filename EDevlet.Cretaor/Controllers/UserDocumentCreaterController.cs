using EDevletDocument.Common.Constants;
using EDevletDocument.Common.Dtos;
using EDevletDocument.Common.Entities.enums;
using EDevletDocument.Common.Service;
using EDevletDocument.Common.Service.Abstracts;
using EDevletDocument.Common.Service.Managers;
using Microsoft.AspNetCore.Mvc;

namespace EDevlet.Cretaor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDocumentCreaterController : ControllerBase
    {
		private readonly IRabbitMQService<RabbitMQManager> messageBroker;

		public UserDocumentCreaterController(IRabbitMQService<RabbitMQManager> messageBroker)
		{
			this.messageBroker = messageBroker;
		}


		[HttpGet]
        public IActionResult GetUserDocumentCreateRequest()
        {

			var exchange = new Exchange
			{
				ExchangeName = Constant.DocExchange,
				Type = "direct"
			};
		

			var data = messageBroker.ConsumeToMQ<UserDacumentDto>(Constant.CreateDocumentQueue);

			data.Email = "ilkinSuleymanov200@gmail.com";
            data.DocumentType = Document.PDF;

			
			messageBroker.Publish(data, exchange, Constant.QueueCreatedDocument);

			return Ok();
        }
    }
}
