// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Manager.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    /// <summary>
    /// Notes controller class that extends ControllerBase
    /// </summary>
    [Authorize]
    public class NotesController : ControllerBase
    {   
        /// <summary>
         /// Declaring the object for the notes manager interface
         /// </summary>
        private readonly INotesManager manager;

        /// <summary>
        /// Notes Controller constructor through which the manager object is assigned
        /// Initializes a new instance of the <see cref="NotesController"/> class
        /// </summary>
        /// <param name="manager">manager object is passed as parameter</param>
        public NotesController(INotesManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Method to add new notes
        /// </summary>
        /// <param name="notesData">Notes model object that has all the properties of notes</param>
        /// <returns>action result as response model</returns>
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

        /// <summary>
        /// method to get the all notes
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>action result as response model</returns>
        [HttpGet]
        [Route("api/GetNotes")]
        public IActionResult GetNotes(int userId)
        {
            try
            {
                ////getting the result as the list of Notes model
                List<NotesModel> result = this.manager.GetNotes(userId);

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
        /// method to get the notes that are archive
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>the action result as ok or bad request</returns> 
        [HttpGet]
        [Route("api/GetArchive")]
        public IActionResult GetArchive(int userId)
        {
            try
            {
                ////getting the result as the list of Notes model
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

        /// <summary>
        /// method to get the notes that are having remainder
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>the action result as ok or bad request</returns> 
        [HttpGet]
        [Route("api/GetRemainder")]
        public IActionResult GetRemainder(int userId)
        {
            try
            {
                ////getting the result as the list of Notes model
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

        /// <summary>
        /// method to get the notes that are trash
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>the action result as ok or bad request</returns> 
        [HttpGet]
        [Route("api/GetTrash")]
        public IActionResult GetTrash(int userId)
        {
            try
            {
                ////getting the result as the list of Notes model
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
        /// API to delete the note and move to trash
        /// </summary>
        /// <param name="noteId">note id which is unique</param>
        /// <returns>the action result as ok or bad request</returns> 
        [HttpPut]
        [Route("api/MoveToTrash")]
        public IActionResult MoveToTrash(int noteId)
        {
            try
            {
                string result = this.manager.MoveToTrash(noteId);

                if (!result.Equals("Move to trash unsuccessful"))
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

        /// <summary>
        /// Method to restore the notes from trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the action result as ok or bad request</returns> 
        [HttpPut]
        [Route("api/RestoreFromTrash")]
        public IActionResult RestoreFromTrash(int noteId)
        {
            try
            {
                bool result = this.manager.RestoreFromTrash(noteId);

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

        /// <summary>
        /// Method to move the notes to archive
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the action result as ok or bad request</returns> 
        [HttpPut]
        [Route("api/MoveToArchieve")]
        public IActionResult MoveToArchieve(int noteId)
        {
            try
            {
                string result = this.manager.MoveToArchieve(noteId);

                if (!result.Equals("Notes not available"))
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

        /// <summary>
        /// Method to un archive the notes
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the action result as ok or bad request</returns> 
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

        /// <summary>
        /// Method to pin or unpin the note
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the action result as ok or bad request</returns> 
        [HttpPut]
        [Route("api/PinAndUnpinNotes")]
        public IActionResult PinAndUnpinNotes(int noteId)
        {
            try
            {
                string result = this.manager.PinAndUnpinNotes(noteId);

                if (!result.Equals("Pinning Unsuccessfull"))
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

        /// <summary>
        /// Method to update the color in the note
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <param name="color">color as string value</param>
        /// <returns>the action result as ok or bad request</returns> 
        [HttpPut]
        [Route("api/UpdateColor")]
        public IActionResult UpdateColor(int noteId, string color)
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

        /// <summary>
        /// Method to move the notes to trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <param name="remainder">remainder value in terms of date and time</param>
        /// <returns>the action result as ok or bad request</returns> 
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

        /// <summary>
        /// Method to delete the remainder from notes
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the action result as ok or bad request</returns> 
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

        /// <summary>
        /// Method to update the notes
        /// </summary>
        /// <param name="updateNote">Notes model object that has all the properties of notes</param>
        /// <returns>the action result as ok or bad request</returns> 
        [HttpPut]
        [Route("api/UpdateNote")]
        public IActionResult UpdateNote([FromBody] NotesUpdateModel updateNote)
        {
            try
            {
                NotesModel result = this.manager.UpdateNote(updateNote);

                if (result != null)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new { Status = true, Message = "Note is updated", Data = result });
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

        /// <summary>
        /// Method to delete the notes from trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the action result as ok or bad request</returns> 
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

        /// <summary>
        /// Method to empty the trash
        /// </summary>
        /// <param name="userId">notes id in Integer</param>
        /// <returns>the action result as ok or bad request</returns> 
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

        /// <summary>
        /// Method add image to the note
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <param name="image">image as an i form </param>
        /// <returns>the action result as ok or bad request</returns> 
        [HttpPut]
        [Route("api/AddImage")]
        public IActionResult AddImage(int noteId, IFormFile image)
        {
            try
            {
                bool result = this.manager.AddImage(noteId, image);

                if (result)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "Image added successfully" });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Image added failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Method to delete the image in the notes
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the action result as ok or bad request</returns> 
        [HttpPut]
        [Route("api/DeleteImage")]
        public IActionResult DeleteImage(int noteId)
        {
            try
            {
                bool result = this.manager.DeleteImage(noteId);

                if (result)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "Image removed successfully" });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Image remove failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
