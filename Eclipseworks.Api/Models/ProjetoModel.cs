namespace Eclipseworks.Api.Models;

public class ProjetoModel
{
    public long Id { get; set; }

    public string Nome { get; set; }

    /// <summary>
    /// Id do usuário que criou o projeto
    /// O usuário que criou o projeto é o único que pode alterar o projeto
    /// O ideal seria pegar do usuario logado
    /// </summary>
    public long CriadorDoProjetoId { get; set; }

    //public IList<TarefaModel> Tarefas { get; set; }

}