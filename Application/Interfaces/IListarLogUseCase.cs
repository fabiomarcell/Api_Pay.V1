using Domain.Requests;
using Domain.Responses;

namespace Application.Interfaces
{
    public interface IListarLogUseCase
    {
        Task<LogsResponse> ExecuteAsync();
    }
}
