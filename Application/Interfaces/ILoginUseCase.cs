﻿using Domain.Requests;
using Domain.Responses;

namespace Application.Interfaces
{
    public interface ILoginUseCase
    {
        Task<LoginResponse> ExecuteAsync(LoginRequest request);
    }
}
