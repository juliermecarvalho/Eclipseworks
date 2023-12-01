using Eclipseworks.Dominio.Core.Base;

namespace Eclipseworks.Dominio.Core;

public class Tarefa : Entity
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataVencimento { get; set; }
    public DateTime? DataConclucao { get; set; }

    public Status Status  { get; set; }
    public Prioridade Prioridade  { get; private set; }
    public long ProjetoId { get; set; }
    public Projeto Projeto { get; set; }
    public long UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public IList<Comentario> Comentarios { get; set; } = new List<Comentario>();


    /// <summary>
    /// Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada.
    /// </summary>
    /// <param name="prioridade"></param>
    public void SetPrioridade(Prioridade prioridade)
    {
        if (Id == 0)
        {
            Prioridade = prioridade;
        }
    }

    public void SetStatus(Status status)
    {
        Status = status;

        if (Status == Status.Concluida)
        {
            DataConclucao = DateTime.Now;
        }
    }
}