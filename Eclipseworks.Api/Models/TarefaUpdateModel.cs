namespace Eclipseworks.Api.Models;

public class TarefaUpdateModel
{
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public string? Detalhes { get; set; }
    public int Status { get; set; }
    /// <summary>
    /// tenho que solicitar esse campo para saber quem � o usu�rio que est� criando a tarefa
    /// como n�o est� usando autentica��o, vou passar o id do usu�rio que est� criando a tarefa
    /// </summary>
    public long UsuarioId { get; set; }

}