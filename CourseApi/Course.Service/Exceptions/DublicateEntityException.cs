using System;
namespace Course.Service.Exceptions
{
	public class DublicateEntityException:Exception
	{
        public DublicateEntityException(string msg) : base(msg) { }
        public DublicateEntityException()
		{
		}
	}
}

