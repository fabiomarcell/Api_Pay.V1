using Infrastructure.Interfaces;

namespace Infrastructure.Services.Provedores
{
    public static class PagamentoFactory
    {
        public static IProvedorStrategy Criar(string nomeProvedor)
        {
            return nomeProvedor.ToLower() switch
            {
                "provedor 1" => new Provedor1Strategy(),
                "provedor 2" => new Provedor2Strategy(),
                _ => throw new ArgumentException("Provedor inválido."),
            };
        }
    }
}
