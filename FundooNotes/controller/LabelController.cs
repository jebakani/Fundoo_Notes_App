// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Bridgelabz">
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
    using Microsoft.AspNetCore.Mvc;
    using Models;

    /// <summary>
    /// Label Controller class to create the controller method that controls the label actions 
    /// </summary>
    public class LabelController : ControllerBase
    {
        /// <summary>
        /// Declaring the object for the Controller manager interface
        /// </summary>
        private readonly ILabelManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class
        /// </summary>
        /// <param name="manager">ILabelManager manager</param>
        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// the controller method to create the Label
        /// </summary>
        /// <param name="label">Label model object with id,name,user id and note Id</param>
        /// <returns>action result as response model</returns>
        [HttpPost]
        [Route("api/CreateLabel")]
        public IActionResult CreateLabel([FromBody] LabelModel label)
        {
            try
            {
                string result = this.manager.CreateLabel(label);

                if (result.Equals("Label is added"))
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
        /// the method to Add the Label
        /// </summary>
        /// <param name="label">Label model object with id,name,user id and note Id</param>
        /// <returns>action result as response model</returns>
        [HttpPost]
        [Route("api/AddLabel")]
        public IActionResult AddLabel([FromBody] LabelModel label)
        {
            try
            {
                string result = this.manager.AddLabel(label);

                if (result.Equals("Label is added"))
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
        /// the method to remove the Label
        /// </summary>
        /// <param name="labelId">Label id that is unique</param>
        /// <returns>action result as response model</returns>
        [HttpDelete]
        [Route("api/RemoveLabel")]
        public IActionResult RemoveLabel(int labelId)
        {
            try
            {
                string result = this.manager.RemoveLabel(labelId);

                if (result.Equals("Label is removed"))
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
        /// the method to delete the Label
        /// </summary>
        /// <param name="userId">user id as integer</param>
        /// <param name="labelName">label name as string</param>
        /// <returns>action result as response model</returns>
        [HttpDelete]
        [Route("api/DeleteLabel")]
        public IActionResult DeleteLabel(int userId, string labelName)
        {
            try
            {
                string result = this.manager.DeleteLabel(userId, labelName);

                if (result.Equals("Label is deleted"))
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
        /// the method to get the Label
        /// </summary>
        /// <param name="userId">user id as integer</param>
        /// <returns>action result as response model</returns>
        [HttpGet]
        [Route("api/GetLabelForUser")]
        public IActionResult GetLabelForUser(int userId)
        {
            try
            {
                var result = this.manager.GetLabelByUserId(userId);

                if (result.Count > 0)
                { 
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new { Status = true, Message = "Labels are retrived", Data = result });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No label available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// the method to get the Label
        /// </summary>
        /// <param name="noteId">note id as integer</param>
        /// <returns>action result as response model</returns>
        [HttpGet]
        [Route("api/GetLabelForNotes")]
        public IActionResult GetLabelForNotes(int noteId)
        {
            try
            {
                var result = this.manager.GetLabelByUserId(noteId);

                if (result.Count > 0)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new { Status = true, Message = "Labels are retrived", Data = result });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No label available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// the method to edit the Label
        /// </summary>
        /// <param name="label">Label model object with id,name,user id and note Id</param>
        /// <returns>action result as response model</returns>
        [HttpPut]
        [Route("api/UpdateLabel")]
        public IActionResult UpdateLabel([FromBody]LabelModel label)
        {
            try
            {
                var result = this.manager.EditLabel(label);

                if (!result.Equals("No Label available"))
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No label available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/GetNotesByLabel")]
        public IActionResult GetNotesByLabel(string labelName, int userId)
        {
            try
            {
                var result = this.manager.GetNotesByLabel(labelName, userId);

                if (result.Count > 0)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new{ Status = true, Message = "Notes available", Data=result });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No Notes available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
