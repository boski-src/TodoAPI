using System;

namespace TodoAPI.Types
{
    public class Created
    {
        public Guid Id { get; set; }

        public Created(Guid id)
        {
            Id = id;
        }
    }
}