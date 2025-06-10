using System.ComponentModel.DataAnnotations;

namespace SistemaReservasLabs.DTOs.Laboratorio
{
    public class CriarLaboratorioDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Capacidade é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "A capacidade deve ser maior que zero")]
        public int Capacidade { get; set; }

        [Required(ErrorMessage = "O campo Localização é obrigatório")]
        public string Localizacao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Área é obrigatório")]
        public string Area { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo CoordenadorId é obrigatório")]
        public int CoordenadorId { get; set; }
    }
}
