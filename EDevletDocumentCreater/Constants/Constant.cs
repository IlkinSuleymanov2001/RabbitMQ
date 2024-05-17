using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDevletDocument.Common.Constants
{
    public static class Constant

    {
        public const string DocExchange= "DocumentExchange";
        public const string CreateDocumentQueue = "CreateDocumentQueue";
        public const string QueueCreatedDocument = "QueueCreatedDocument";
        public const string ConnectionUrl = "amqp://guest:guest@localhost:5672";
    }
}
