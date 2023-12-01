
namespace Eclipseworks.Api.Models;

public class TarefaModel
{
    public long Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Detalhes { get; set; }
    public DateTime DataVencimento { get; set; }
    public int Status  { get; set; }
    public int Prioridade { get; private set; }
    public long ProjetoId { get; set; }
    /// <summary>
    /// tenho que solicitar esse campo para saber quem é o usuário que está criando a tarefa
    /// como não está usando autenticação, vou passar o id do usuário que está criando a tarefa
    /// </summary>
    public long UsuarioId { get; set; }

}