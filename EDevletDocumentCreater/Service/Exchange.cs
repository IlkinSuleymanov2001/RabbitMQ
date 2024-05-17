using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDevletDocument.Common.Service
{
	public  class Exchange
	{
		private string _exchangeType;
		public string ExchangeName { get; set; }
		public string? Type {
			get 
			{ 
				return _exchangeType;
			} set 
			{
				 FindSuitableExchangeType(value);
			}
		}


		private string FindSuitableExchangeType(string value )
		{

			switch (value.ToLower())
			{
				case ("direct"):
					_exchangeType = ExchangeType.Direct;
					break;
				case ("topic"):
					_exchangeType = ExchangeType.Topic;
					break;
				case ("headers"):
					_exchangeType = ExchangeType.Headers;
					break;
				case ("fanout"):
					_exchangeType = ExchangeType.Fanout;
					break;
				default:
					_exchangeType = "";
					break;

			}
			return _exchangeType;

		}
	}
}
