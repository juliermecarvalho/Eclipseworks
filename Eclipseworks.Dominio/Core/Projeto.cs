using Eclipseworks.Dominio.Core.Base;

namespace Eclipseworks.Dominio.Core;

public class Projeto : Entity
{
    public string Nome { get; set; }
    /// <summary>
    /// Id do usu�rio que criou o projeto
    /// O usu�rio que criou o projeto � o �nico que pode alterar o projeto
    /// O ideal seria pegar do usuario logado
    /// </summary>
    public long CriadorDoProjetoId { get; private set; }
    public Usuario CriadorDoProjeto { get; }
        
    public IList<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    public void SetCriadorDoProjeto(long usuarioId)
    {
        if (Id == 0)
        {
            CriadorDoProjetoId = usuarioId;
        }
    }


}