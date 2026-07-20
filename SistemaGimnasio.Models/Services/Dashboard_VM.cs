namespace SistemaGimnasio.Models.Services
{
    public class Dashboard_VM
    {
        public int TotalClientes { get; set; }
        public int ClientesActivos { get; set; }
        public int EntrenadoresActivos { get; set; }
        public int MembresiasActivas { get; set; }
        public int AsistenciasHoy { get; set; }
        public decimal PagosMes { get; set; }
    }
}
