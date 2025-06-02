using Infrastructure.Interfaces;

namespace Infrastructure.Services.Provedores
{
    public static class PagamentoFactory
    {
        public static IProvedorStrategy Criar(string nomeProvedor)
        {
            return nomeProvedor.ToLower() switch
            {
                "683df3d10a1c6bbfca2cd5d1" => new Provedor1Strategy(),
                "683df3d10a1c6bbfca2cd5d2" => new Provedor2Strategy(),
                _ => throw new ArgumentException("Provedor inválido."),
            };
        }
    }
}
