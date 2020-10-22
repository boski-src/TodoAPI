using TodoAPI.Contexts;

namespace TodoAPI.Types
{
    public class ServiceException : CustomException
    {
        public ServiceException(string error) : base(error)
        {
        }
    }
}