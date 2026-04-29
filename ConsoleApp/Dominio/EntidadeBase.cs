using System.Security.Cryptography;
using System;
public abstract class EntidadeBase
{
    public string Id { get; set; }

    public EntidadeBase()
    {
        Id = Convert
            .ToHexString(RandomNumberGenerator.GetBytes(20))
            .ToLower()
            .Substring(0, 7);
    }

    public abstract string[] Validar();
    public abstract void AtualizarRegistro(EntidadeBase entidadeAtualizada);
}