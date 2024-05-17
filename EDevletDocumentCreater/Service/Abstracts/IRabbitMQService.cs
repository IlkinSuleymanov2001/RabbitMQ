using EDevletDocument.Common.Service.Managers;
using RabbitMQ.Client;

namespace EDevletDocument.Common.Service.Abstracts
{
    public interface IRabbitMQService<T>:IMessageQueueBusService<T> where T : class, IMessageQueueBusService<T>
	{

		public IModel Channel { get;}
		public IConnection Connection { get; }

		public T BindExchangeToQueue(string routingKey, string creatQueueName, Exchange exchange);
		public T BindCurrentExchangeToQueue(string routingKey,string queueName);
		public T CreateExchnage(Exchange exchange);
		public void Publish<TM>(TM message, Exchange exchange, string routeKey);

		public TDeserlizeObject ConsumeToMQ<TDeserlizeObject>(string queueName, bool autoDeck = true)
			where TDeserlizeObject : class;
	}
}
