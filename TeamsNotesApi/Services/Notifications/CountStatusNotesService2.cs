using TeamsNotesApi.Services.Interfaces;

namespace TeamsNotesApi.Services.Notifications
{
    public class CountStatusNotesService2 : ICountStatusNotesService2
    {
        string lstr_res = "OK";
        int lint_qt_pendiente = 0;
        int lint_qt_nuevo_total = 0;
        int lint_qt_nuevo_mail = 0;
        int lint_qt_nuevo_app = 0;
        int lint_qt_nuevo_alerta = 0;

        string lstr_titulo = "";
        string lstr_mensaje = "";     // BallonTip
        string lstr_IconText = "";    // Texto CTN incluido en NotifyIcon.Text
        public CountStatusNotesService2()
        {
            //connect to dll?
        }
        public string SelectCountNotes(int id_user)
        {
            if (true)       //CVM.RT.MiCTN.Notifica.lg_notifica
            {
                // lstr_res = CVM.CTN.RegDestino.BuscarAlertas(lint_qt_pendiente, lint_qt_nuevo_total, lint_qt_nuevo_mail, lint_qt_nuevo_app, lint_qt_nuevo_alerta)
               
                lint_qt_pendiente = 5;
                lint_qt_nuevo_total = 3;
                lint_qt_nuevo_mail = 1;
                lint_qt_nuevo_app = 1;
                lint_qt_nuevo_alerta = 1;

                // Notas Nuevas para informar en el BallonTip
                if (lint_qt_nuevo_total > 0)
                {
                    lstr_titulo = "Has recibido " + lint_qt_nuevo_total.ToString() + " notas en TeamNotes";
                    lstr_mensaje = "";

                    if (lint_qt_nuevo_app > 0)
                    {
                        lstr_mensaje += lint_qt_nuevo_app.ToString() + " nota/s de aplicación" + Environment.NewLine;

                        if (lint_qt_nuevo_mail > 0)
                        {
                            lstr_mensaje += lint_qt_nuevo_mail.ToString() + " mail/s" + Environment.NewLine;

                            if (lint_qt_nuevo_alerta > 0)
                            {
                                lstr_mensaje += lint_qt_nuevo_alerta.ToString() + " alerta/s" + Environment.NewLine;
                                lstr_mensaje += lint_qt_pendiente.ToString() + " nota/s en Total pendiente de leer" + Environment.NewLine;

                                return lstr_mensaje;
                            }
                        }
                    }
                }
            }

            return "";
        }
    }
}
