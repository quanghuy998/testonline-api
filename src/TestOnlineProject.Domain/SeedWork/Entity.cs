﻿namespace TestOnlineProject.Domain.SeedWork
{
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
    }
}
