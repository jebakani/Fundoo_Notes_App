using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controller
{
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
        public IActionResult GetNotes(int id)
        {
            try
            {
                //getting the result as the list of Notes model
                List<NotesModel> result = this.manager.GetNotes(id);

                if (result!=null)
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

        /// <summary>
        /// Api to delete the note and move to trash
        /// </summary>
        /// <param name="noteId">note id which is unique</param>
        /// <param name="userId">user id that is foreign key</param>
        /// <returns>returns the action </returns>
        [HttpPut]
        [Route("api/MoveToTrash")]
        public IActionResult MoveToTrash(int noteId,int userId)
        {
            try
            {
                bool result = this.manager.MoveToTrash( noteId, userId);

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

    }
}
