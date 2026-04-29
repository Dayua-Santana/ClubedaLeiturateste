namespace ClubedaLeiturateste.ConsoleApp.Infraestrutura
{
    using ClubedaLeiturateste.ConsoleApp.Dominio;

    public class RepositorioAmigo : RepositorioBase
    {
        public bool ExisteAmigoComMesmoNome(string nomeProcurado)
        {
            foreach (EntidadeBase entidade in registros)
            {
                if (entidade != null)
                {
                    Amigo amigo = (Amigo)entidade;
                    if (amigo.Nome.Equals(nomeProcurado, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
            return false;
        }
    }
}