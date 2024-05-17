

namespace EDevletDocument.Common.Service.Abstracts
{
	public  interface IMessageQueueBusService<T> where T: class ,IMessageQueueBusService<T> 
	{

		public T CreateQueue(string queueName);

	}
}
