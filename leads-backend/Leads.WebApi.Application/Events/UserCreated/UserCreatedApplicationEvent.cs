﻿namespace Leads.WebApi.Application.Events.UserCreated
{
    using global::Application.Events.Abstractions;

    public class UserCreatedApplicationEvent : IApplicationEvent
    {
        public UserCreatedApplicationEvent(string email, string password)
        {
            Email = email;
            Password = password;
        }



        public string Email { get; }

        public string Password { get; }
    }
}