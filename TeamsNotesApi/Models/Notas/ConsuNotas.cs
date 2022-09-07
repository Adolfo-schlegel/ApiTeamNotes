namespace TeamsNotesApi.Models.Notas
{
    public class ConsuNotas
    {
        public string? fecha { get; set; } //deben ser 2; hasta y desde 
        public List<Grupo>? grupos { get; set; }
        public List<Destinatario>? destinatario { get; set; }
        
        //public string? medio { get; set; }        
        //public bool? myNotes { get; set; }
        public int? estado { get; set; }
        
    }

   


}
