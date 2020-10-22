using System;

namespace TodoAPI.Contexts
{
    public class CustomException : Exception
    {
        public CustomException(string error) : base(error)
        {
        }
    }
}