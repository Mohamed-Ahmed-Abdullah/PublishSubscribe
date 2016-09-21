using System;

namespace Repositories.Common
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}