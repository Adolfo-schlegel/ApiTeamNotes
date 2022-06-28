namespace TeamsNotesApi.Models.Notas
{
    public class RegistroNotas
    {
        public DateTime? dt_alta { get; set; }
        public string? dh_alta { get; set; }
        public string? ds_nota { get; set; }
        public string? ds_detalle { get; set; }        
        public string? ds_lectura_estado { get; set; }
        public int id_Reg_Destino { get; set; }
        public int ct_anexos { get; set; }
    }
}
