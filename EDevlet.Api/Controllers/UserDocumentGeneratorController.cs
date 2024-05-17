using EDevletDocument.Common.Constants;
using EDevletDocument.Common.Dtos;
using EDevletDocument.Common.Entities.enums;
using EDevletDocument.Common.Service;
using EDevletDocument.Common.Service.Abstracts;
using EDevletDocument.Common.Service.Managers;
using Microsoft.AspNetCore.Mvc;


namespace EDevlet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDocumentGeneratorController : ControllerBase
    {
        private readonly IRabbitMQService<RabbitMQManager> messageBroker;

		public UserDocumentGeneratorController(IRabbitMQService<RabbitMQManager> messageBroker)
		{
			this.messageBroker = messageBroker;
		}

		[HttpPost]
        public IActionResult PublishMessageToMQ(UserDacumentDto userDocumentDto)
        {
			var exchange = new Exchange
			{
				ExchangeName = Constant.DocExchange,
				Type = "direct"
			};


			messageBroker.BindExchangeToQueue(Constant.CreateDocumentQueue,
				Constant.CreateDocumentQueue,
				exchange
                );

			messageBroker.Publish(userDocumentDto, exchange,Constant.CreateDocumentQueue);

			messageBroker.BindCurrentExchangeToQueue(Constant.QueueCreatedDocument, Constant.QueueCreatedDocument);

			return Ok("successfully publish the message ");
        }


		[HttpGet("consumedocument")]
		public  IActionResult ConsumeMessageToMQ()
        {

			var data =  messageBroker.ConsumeToMQ<UserDacumentDto>(Constant.QueueCreatedDocument);

            return data != null ? Ok(new
            {
                returnDataCreatedQueue = data,
                message = "success"
            })
            : BadRequest(new
			{
				returnDataCreatedQueue = data,
				message = "UnSuccess"
			});
		}
	}
}
