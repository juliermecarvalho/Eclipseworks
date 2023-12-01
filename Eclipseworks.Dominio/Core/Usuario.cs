

using Eclipseworks.Dominio.Core.Base;

namespace Eclipseworks.Dominio.Core;

public class Usuario : Entity
{
    public string Nome { get; set; }
    public Funcao Funcao { get; set; }
    public IList<Projeto> Projetos { get; set; } = new List<Projeto>();
    public IList<Historico> Historicos { get; set; } = new List<Historico>();
    public IList<Tarefa> Tarefas { get; set; } = new List<Tarefa>();

}