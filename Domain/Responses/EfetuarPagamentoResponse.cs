namespace Domain.Responses
{
    public record EfetuarPagamentoResponse(
        Guid Id,
        string Status,
        double OriginalAmount,
        string Currency, //BRL
        Guid CardId
    );

}
