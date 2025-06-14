namespace SistemaReservasLabs.Models.Entities;

public class Laboratorio
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Capacidade { get; set; }
    public string Localizacao { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public bool EmManutencao { get; set; } = false;
    public int CoordenadorLaboratorioId { get; set; }
    public Usuario? CoordenadorLaboratorio { get; set; }

    public ICollection<LaboratorioTecnico>? Tecnicos { get; set; } = new List<LaboratorioTecnico>();
    public ICollection<Reserva>? Reservas { get; set; } 
}
