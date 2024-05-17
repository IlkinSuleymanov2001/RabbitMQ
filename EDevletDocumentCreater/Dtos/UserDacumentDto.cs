using EDevletDocument.Common.Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDevletDocument.Common.Dtos
{
	public  class UserDacumentDto
	{

		public int Id { get; set; }
		public string FirstName { get; set; }

		public string LastName { get; set; }
		public string Email { get; set; }

		public Document? DocumentType { get; set; }
	}
}
