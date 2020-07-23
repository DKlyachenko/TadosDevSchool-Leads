﻿namespace Application.Events.Stores.Processors.Abstractions
{
    using Domain.Events.Stores.Processors.Abstractions;
    using Events.Abstractions;

    public interface IDomainToApplicationEventStoreProcessor : IDomainEventStoreProcessor<IApplicationEvent>
    {
    }
}
