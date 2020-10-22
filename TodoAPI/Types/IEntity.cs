using System;

namespace TodoAPI.Types
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}