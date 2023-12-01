using Eclipseworks.Dominio.Core.Base;

namespace Eclipseworks.Dominio.Core;

public class Comentario : Entity
{
    public string Texto { get; set; }
    public long TarefaId { get; set; }
    public Tarefa Tarefa { get; set; }

}