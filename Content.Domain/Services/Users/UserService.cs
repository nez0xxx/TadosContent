﻿namespace Content.Domain.Services.Users
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands.Contexts;
    using Entities;
    using Enums;
    using global::Commands.Abstractions;
    using Queries.Abstractions;

    public class UserService : IUserService
    {
        private readonly IAsyncCommandBuilder _asyncCommandBuilder;

        public UserService(IAsyncQueryBuilder asyncQueryBuilder, IAsyncCommandBuilder asyncCommandBuilder) 
        {
            _asyncCommandBuilder = asyncCommandBuilder ?? throw new ArgumentNullException(nameof(asyncCommandBuilder));
        }


        public async Task<User> CreateUserAsync(City city, string email, CancellationToken cancellationToken = default)
        {


            User user = new User(city, email);

            await _asyncCommandBuilder.CreateAsync(user, cancellationToken);

            return user;
        }
    }
}