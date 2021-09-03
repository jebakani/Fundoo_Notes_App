using Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controller
{
   [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INotesManager manager;
        public NotesController(INotesManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/AddNotes")]
        public IActionResult AddNotes([FromBody] NotesModel notesData)
        {
            try
            {
                string result = this.manager.AddNotes(notesData);

                if (result.Equals("Notes Added Succesfully"))
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetNotes")]
        public IActionResult GetNotes(int userId)
        {
            try
            {
                //getting the result as the list of Notes model
                List<NotesModel> result = this.manager.GetNotes(userId);

                if (result.Count!=0)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new { Status = true, Message = "Data is available",Data=result });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Data is not available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/GetArchive")]
        public IActionResult GetArchive(int userId)
        {
            try
            {
                //getting the result as the list of Notes model
                List<NotesModel> result = this.manager.GetArchive(userId);

                if (result.Count != 0)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new { Status = true, Message = "Data is available", Data = result });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Data is not available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/GetRemainder")]
        public IActionResult GetRemainder(int userId)
        {
            try
            {
                //getting the result as the list of Notes model
                List<NotesModel> result = this.manager.GetRemainder(userId);

                if (result.Count != 0)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new { Status = true, Message = "Data is available", Data = result });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Data is not available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/GetTrash")]
        public IActionResult GetTrash(int userId)
        {
            try
            {
                //getting the result as the list of Notes model
                List<NotesModel> result = this.manager.GetTrash(userId);

                if (result.Count != 0)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new { Status = true, Message = "Data is available", Data = result });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Data is not available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Api to delete the note and move to trash
        /// </summary>
        /// <param name="noteId">note id which is unique</param>
        /// <param name="userId">user id that is foreign key</param>
        /// <returns>returns the action </returns>
        [HttpPut]
        [Route("api/MoveToTrash")]
        public IActionResult MoveToTrash(int noteId)
        {
            try
            {
                bool result = this.manager.MoveToTrash( noteId);

                if (result)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Notes moved to thrash" });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Can't able to delete" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/RestoreFromTrash")]
        public IActionResult RestoreFromTrash(int noteId)
        {
            try
            {
                bool result = this.manager.RestoreFromTrash(noteId );

                if (result)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Notes restore from trash" });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Can't able to restore" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/MoveToArchieve")]
        public IActionResult MoveToArchieve(int noteId)
        {
            try
            {
                bool result = this.manager.MoveToArchieve(noteId);

                if (result)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Notes moved to Archieve" });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Can't move to Archieve" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/UnArchive")]
        public IActionResult UnArchive(int noteId)
        {
            try
            {
                bool result = this.manager.UnArchive(noteId);

                if (result)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Notes is UnArchive" });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Can't move to UnArchieve" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/PinAndUnpinNotes")]
        public IActionResult PinAndUnpinNotes(int noteId)
        {
            try
            {
                string result = this.manager.PinAndUnpinNotes(noteId);

                if (!(result.Equals("Pinning Unsuccessfull")))
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message =result});
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result});
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/UpdateColor")]
        public IActionResult UpdateColor(int noteId,string color)
        {
            try
            {
                bool result = this.manager.UpdateColor(noteId, color);

                if (result)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Color is updated" });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Color is not updated" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/UpdateRemainder")]
        public IActionResult UpdateRemainder(int noteId,  string remainder)
        {
            try
            {
                bool result = this.manager.UpdateRemainder(noteId,  remainder);

                if (result)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "remainder is updated" });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "remainder is not updated" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/DeleteRemainder")]
        public IActionResult DeleteRemainder(int noteId)
        {
            try
            {
                bool result = this.manager.DeleteRemainder(noteId);

                if (result)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "remainder is Deleted" });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "remainder is not Deleted" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/UpdateNote")]
        public IActionResult UpdateNote([FromBody] NotesUpdateModel updateNote)
        {
            try
            {
                NotesModel result = this.manager.UpdateNote(updateNote);

                if (result!=null)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new { Status = true, Message = "Note is updated", Data=result });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note does not exist or note not updated" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("api/DeleteFromTrash")]
        public IActionResult DeleteFromTrash(int noteId)
        {
            try
            {
                bool result = this.manager.DeleteFromTrash(noteId);

                if (result)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "Note is deleted" });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Notes is not deleted" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("api/EmptyTrash")]
        public IActionResult EmptyTrash(int userId)
        {
            try
            {
                bool result = this.manager.EmptyTrash(userId);

                if (result)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "All notes get deleted" });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Notes are not deleted" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
