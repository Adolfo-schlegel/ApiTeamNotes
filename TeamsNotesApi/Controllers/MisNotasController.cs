using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using TeamsNotesApi.Models;
using TeamsNotesApi.Models.Notas;
using TeamsNotesApi.Services.Interfaces;

namespace TeamsNotesApi.Controllers
{
    [Route("api/[controller]")]
    public class MisNotasController : Controller
    {
        private Reply oR;
        IMyNotesService _myNotesService;
        public MisNotasController(IMyNotesService myNotesService)
        {
            _myNotesService = myNotesService;
            oR = new Reply();
        }

        [HttpGet]
        [Route("MyNotes")]
        [Authorize]        
        public Reply NotasConsulta([FromQuery] int page, [FromQuery] int status)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                if (identity != null)
                {
                    int id_user = int.Parse(identity.Claims.First().Value);

                    oR.data = _myNotesService.GetNotes(id_user,status, page);

                    if (oR.data != null)
                    {
                        oR.result = 1;
                        oR.message = "OK";                        
                        return oR;
                    }
                }
            }
            catch (Exception ex)
            {
                return oR;
            }
            return null;
        }

        [HttpPost]
        [Route("MyNotesFilter")]
        [Authorize]
        public Reply NotasConsultaFiltrada([FromBody] ConsuNotas model, [FromQuery] int page)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                
                if (identity != null)
                {
                    int id_user = int.Parse(identity.Claims.First().Value);

                    oR.data = _myNotesService.GetFilterNotes(model, id_user, page);

                    if (oR.data != null)
                    {
                        oR.result = 1;
                        oR.message = "OK";
                        return oR;
                    }
                }
            }
            catch (Exception ex)
            {
                return oR;
            }
            return null;
        }

        [HttpGet]
        [Route("FillOptions")]
        [Authorize]
        public Reply FillOpcionsInputs()
        {
            try
            {
                oR.data = _myNotesService.GetOpcionsUser();

                if (oR.data != null)
                {
                    oR.result = 1;
                    oR.message = "OK";

                    return oR;
                }
            }
            catch (Exception ex)
            {
                oR.message = ex.ToString();
                return oR;
            }
            return null;
        }
        [HttpPost]
        [Route("GetInfoNotes")]
        [Authorize]
        public Reply GetInfoNotes([FromQuery] int id)
        {            
            try
            {
                oR.data = _myNotesService.GetInfoNote(id);
                if(oR.data != null)
                {
                    oR.result = 1;
                    oR.message = "OK";

                    return oR;
                }
            }
            catch(Exception ex)
            {
                oR.message = ex.Message.ToString();
                return oR;                  
            }
            return null;
        }

        //[HttpGet]
        //[Route("DownloadFileAnexo")]
        //public HttpResponseMessage GetFile([FromQuery] int id)
        //{
        //    HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        //    try
        //    {
        //        oR.data = _myNotesService.GetFileAnexo(id);
        //        string? filepath = oR.data.ToString();

        //        if(oR.data != null)
        //        {
        //            response.Content = new StreamContent(new FileStream(filepath, FileMode.Open, FileAccess.Read));
        //            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
        //            response.Content.Headers.ContentDisposition.FileName = Path.GetFileName(filepath);
        //            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf"); 
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        response.StatusCode.HasFlag(System.Net.HttpStatusCode.BadRequest);
        //    }

        //    return response;
        //}

        [HttpGet]
        [Route("DownloadFileAnexo")]
        public FileContentResult GetFile([FromQuery] int id)
        {
            var result = _myNotesService.GetFileAnexo(id);

            var pathfile = result.GetType().GetProperty("pathfile")?.GetValue(result, null);
            var type = result.GetType().GetProperty("contentype")?.GetValue(result, null);
            
            var FileBytes = System.IO.File.ReadAllBytes(pathfile.ToString());

            return File(FileBytes, "application/pdf", Path.GetFileName(pathfile.ToString()));
        }

        [HttpPut]
        [Route("UpdateStatusNote")]
        [Authorize]
        public Reply ChangeStatusNote([FromQuery] int id)
        {
            oR.result = _myNotesService.UpdateStatusNote(id);
            if(oR.result != 0)
            {
                oR.message = "OK";
                return oR;
            }
            return oR;
        }
    }
}
