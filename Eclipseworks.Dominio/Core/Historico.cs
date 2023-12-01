using Eclipseworks.Dominio.Core.Base;

namespace Eclipseworks.Dominio.Core;

public class Historico : Entity
{
    public DateTime Data { get; set; } = DateTime.Now;
    public long UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public string Operacao  { get; set; }
    public long TarefaId { get; set; }
    public string Campo  { get; set; }
    public string ValorAntigo  { get; set; }
    public string NovoValor  { get; set; }

}