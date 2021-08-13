using System;

namespace Models
{
	public class Book : object
	{
		public Book() : base()
		{
		}

		public Int32 BookId { get; set; }

		public string BookName { get; set; }

		public string ISBN { get; set; }
	}
}
