using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDevletDocumentCreater.DtoEvents
{
	public  class OrderCreateEvent:EventBase
	{
		public DateTime CreateTime { get; set; }
        public string ProductName { get; set; }
    }
}
