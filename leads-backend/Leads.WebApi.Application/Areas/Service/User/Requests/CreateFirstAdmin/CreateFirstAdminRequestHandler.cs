﻿namespace Leads.WebApi.Application.Areas.Service.User.Requests.CreateFirstAdmin
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Users.Enums;
    using Domain.Users.Exceptions;
    using Domain.Users.Objects.Entities;
    using Domain.Users.Services.Abstractions;
    using Infrastructure.Exceptions;
    using Infrastructure.Requests.Handlers;


    public class CreateFirstAdminRequestHandler : IAsyncApiRequestHandler<CreateFirstAdminRequest>
    {
        private readonly IUserService _userService;


        public CreateFirstAdminRequestHandler(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }


        public async Task ExecuteAsync(
            CreateFirstAdminRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var user = new User(request.Email.Trim(), request.Password, UserRoles.Administrator);

                await _userService.CreateAsync(user, cancellationToken);
            }
            catch (UserAlreadyExistsException)
            {
                throw new ApiException(ErrorCodes.UserAlreadyExists, "User with email already exists");
            }
        }
    }
}