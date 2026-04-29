namespace ClubedaLeiturateste.ConsoleApp.Infraestrutura
{
    using ClubedaLeiturateste.ConsoleApp.Dominio;

    public abstract class RepositorioBase
    {
        protected EntidadeBase?[] registros = new EntidadeBase[100];
        protected int contadorId = 1; // Para gerar IDs automáticos

        public void Cadastrar(EntidadeBase entidade)
        {
            entidade.Id = contadorId++;

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                {
                    registros[i] = entidade;
                    break;
                }
            }
        }
        public virtual bool Editar(int IdParaEditar, EntidadeBase novaEntidade)
        {
            EntidadeBase? registro = SelecionarPorId(IdParaEditar);
            if (registro == null)
                return false;
            // Aqui usamos aquele método que você implementou em Revista/Amigo!
            registro.AtualizarRegistro(novaEntidade);
            return true;
        }
        public bool Excluir(int IdParaExcluir)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] != null && registros[i]!.Id == IdParaExcluir)
                {
                    registros[i] = null;
                    return true;
                }
            }
            return false;

        }
        public EntidadeBase? SelecionarPorId(int id)
        {
            foreach (EntidadeBase? e in registros)
            {
                if (e != null && e.Id == id)
                    return e;
            }
            return null;
        }
        public EntidadeBase?[] SelecionarTodos()
        {
            return registros;
        }

    }
}