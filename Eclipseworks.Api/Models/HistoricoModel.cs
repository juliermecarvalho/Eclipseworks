namespace Eclipseworks.Api.Models;

public class HistoricoModel
{
    public DateTime Data { get; set; }
    public long UsuarioId { get; set; }
    public string NomeUsuario { get; set; }
    public string Operacao { get; set; }
    public string Campo { get; set; }
    public string ValorAntigo { get; set; }
    public string NovoValor { get; set; }

}