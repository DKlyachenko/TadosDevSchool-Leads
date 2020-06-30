﻿namespace Leads.WebApi.Application.Areas.Api.User.Requests.ResetPassword
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Common.Queries.Criteria.Extensions;
    using Domain.Users.Objects.Entities;
    using global::Infrastructure.Queries.Builders.Abstractions;
    using Infrastructure.Exceptions;
    using Infrastructure.Messaging;
    using Infrastructure.Requests.Handlers;
    using Infrastructure.Security.Passwords;


    public class ResetPasswordRequestHandler : IAsyncApiRequestHandler<ResetPasswordRequest>
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IEmailMessageSender _emailMessageSender;


        public ResetPasswordRequestHandler(
            IQueryBuilder queryBuilder,
            IPasswordGenerator passwordGenerator,
            IEmailMessageSender emailMessageSender)
        {
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
            _passwordGenerator = passwordGenerator ?? throw new ArgumentNullException(nameof(passwordGenerator));
            _emailMessageSender = emailMessageSender ?? throw new ArgumentNullException(nameof(emailMessageSender));
        }


        public async Task ExecuteAsync(
            ResetPasswordRequest request, 
            CancellationToken cancellationToken = default)
        {
            var user = _queryBuilder.FindNotDeletedById<User>(request.Id);
            
            if (user == null)
                throw new ApiException(ErrorCodes.UserNotFound, "User not found");

            var password = _passwordGenerator.Generate();
            
            user.SetPassword(password);
            
            await _emailMessageSender.SendMessageAsync(
                user.Email,
                "Ваш пароль сброшен",
                $"Логин: {user.Email}{Environment.NewLine}Пароль: {password}");
        }
    }
}