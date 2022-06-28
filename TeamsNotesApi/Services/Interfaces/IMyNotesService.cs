﻿using TeamsNotesApi.Models.Notas;

namespace TeamsNotesApi.Services.Interfaces
{
    public interface IMyNotesService
    {
        public object GetOpcionsUser();
        public object GetNotes(int id_user);
        public object GetFilterNotes(ConsuNotas model, int id_user);
        public object GetInfoNote(int idRegDestino);
        public object GetFileAnexo(int id_reg_anexo);
        public int UpdateStatusNote(int IdRegDestino);
    }
}