namespace Eclipseworks.Api.Models;

public class TarefaUpdateModel
{
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public string? Detalhes { get; set; }
    public int Status { get; set; }
    /// <summary>
    /// tenho que solicitar esse campo para saber quem é o usuário que está criando a tarefa
    /// como não está usando autenticação, vou passar o id do usuário que está criando a tarefa
    /// </summary>
    public long UsuarioId { get; set; }

}