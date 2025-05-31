namespace Domain.Responses
{
    public record EfetuarPagamentoResponse(
        string Id,
        string Status,
        string OriginalAmount,
        string Currency, //BRL
        string CardId
    );

}
