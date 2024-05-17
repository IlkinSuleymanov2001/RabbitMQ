using EDevletDocument.Common.Constants;
using EDevletDocument.Common.Service.Abstracts;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EDevletDocument.Common.Service.Managers
{
    public class RabbitMQManager : IRabbitMQService<RabbitMQManager>

    {
        private static  string _connectionUrl = "amqp://guest:guest@localhost:5672";


        private IConnection _connection;
        private IModel _channel;
		private Exchange _exchange;


        public IModel Channel => _channel ?? GetChannel();
        public IConnection Connection => _connection ?? GetConnectionToMQ();



        public  RabbitMQManager(string connectionUrl = null)
        {
			_connectionUrl = connectionUrl ?? _connectionUrl;
			_connection = GetConnectionToMQ(_connectionUrl);
		}

		



		public RabbitMQManager BindExchangeToQueue(string routingKey,string creatQueueName, Exchange exchange)
		{

		
				Channel.QueueDeclare(creatQueueName);
				CreateExchnage(exchange);
				Channel.QueueBind(creatQueueName, exchange.ExchangeName, routingKey);
				return this;
		}


		public RabbitMQManager BindCurrentExchangeToQueue(string routingKey , string queueName)
		{


			if ( _exchange != null)
			{
				Channel.QueueDeclare(queueName);
				Channel.QueueBind(queueName, _exchange.ExchangeName, routingKey);
				return this;
			}
			throw new ArgumentException("doest not exists exchange");
			
		}

		public RabbitMQManager CreateExchnage(Exchange exchange)
		{
			Channel.ExchangeDeclare(exchange.ExchangeName, exchange.Type, false, false);
			_exchange = exchange;
			return this;
		}

		public RabbitMQManager CreateQueue(string queueName)
		{
			Channel.QueueDeclare(queueName);
			return this;
		}

		

		


		public void Publish<TM>(TM message, Exchange exchange, string routeKey)
		{
			if (message == null)
				return;

			var byteMessage = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

			var properties = Channel.CreateBasicProperties();
			properties.Persistent = true;
			Channel.BasicPublish(exchange.ExchangeName, routeKey, properties, byteMessage);


		}

		public TDeserlizeObject ConsumeToMQ<TDeserlizeObject>(string queueName, bool autoDeck = true)
			where TDeserlizeObject : class
		{

			var ConsumerEvent = new EventingBasicConsumer(Channel);
			TDeserlizeObject? metaData = null;
			ConsumerEvent.Received += (ch, ea) =>
			{
				metaData = JsonConvert.DeserializeObject<TDeserlizeObject>(Encoding.UTF8.GetString(ea.Body.ToArray()));

			};

			Channel.BasicConsume(ConsumerEvent, queueName,autoDeck, exclusive: true);
			return metaData;
		}

		private IConnection GetConnectionToMQ(string connectionUrl = null)
		{
			if (_connection == null)
			{

				ConnectionFactory connectioin = new ConnectionFactory
				{
					Uri = new Uri(connectionUrl ?? _connectionUrl)

				};
				_connection = connectioin.CreateConnection();

			}

			return _connection;
		}

		private IModel GetChannel()
		{
			if(_connection==null)
				_connection = GetConnectionToMQ();
			return _channel ?? (_channel = Connection.CreateModel());
		}

	}
}




