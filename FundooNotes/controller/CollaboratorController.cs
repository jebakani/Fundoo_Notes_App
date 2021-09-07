// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorController.cs" company="Bridgelabz">
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
    using Microsoft.AspNetCore.Mvc;
    using Models;

    /// <summary>
    /// Collaborator controller class that extends ControllerBase
    /// </summary>
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        /// <summary>
        /// Declaring the object for the Controller manager interface
        /// </summary>
        private readonly ICollaboratorManager manager;

        /// <summary>
        /// Collaborator Controller constructor through which the manager object is assigned
        /// Initializes a new instance of the <see cref="CollaboratorController"/> class
        /// </summary>
        /// <param name="manager">manager object is passed as parameter</param>
        public CollaboratorController(ICollaboratorManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Controller method to Add new collaborator
        /// </summary>
        /// <param name="collaborator">collaborator model that contains id,name notesId</param>
        /// <returns>return the IAction result such as bad request, Ok etc.,</returns>
        [HttpPost]
        [Route("api/AddCollaborator")]
        public IActionResult AddCollaborator([FromBody] CollaboratorModel collaborator)
        {
            try
            {
                string result = this.manager.AddCollaborator(collaborator);

                if (result.Equals("Collaborator is added"))
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
        /// Controller method to Delete the collaborator
        /// </summary>
        /// <param name="collaboratorId">collaborator id as integer value is passed</param>
        /// <returns>action result as response model</returns>
        [HttpPost]
        [Route("api/RemoveCollaborator")]
        public IActionResult RemoveCollaborator(int collaboratorId)
        {
            try
            {
                string result = this.manager.RemoveCollaborator(collaboratorId);

                if (result.Equals("Collaborator is added"))
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
        /// Controller method to get all collaborator for particular notes
        /// </summary>
        /// <param name="noteId">notes id passed as integer</param>
        /// <returns>the action result as ok or bad request</returns>        /// <returns>action result as response model</returns>
        [HttpGet]
        [Route("api/GetCollaborator")]
        public IActionResult GetCollaborator(int noteId)
        {
            try
            {
                var result = this.manager.GetCollaborator(noteId);

                if (result.Count > 0)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new { Status = true, Message = "collaborator retrived", Data = result });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Can't able to retrive the collaborator" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}