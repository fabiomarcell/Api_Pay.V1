using Domain.Requests;
using Domain.Responses;

namespace Application.Interfaces
{
    public interface IGerarLogUseCase
    {
        Task<bool> ExecuteAsync(string origem, string content, object obj = null);
    }
}
