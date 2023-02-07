using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities;

public class ContaCorrente
{
    private string IdContaCorrente { get; set; }
    public string? Nome { get; set; }
    public int Numero { get; set; }
    public bool Ativo { get; set; }
    public TipoMovimento TipoMovimento { get; set; }
}