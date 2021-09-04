using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controller
{
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorManager manager;
        public CollaboratorController(ICollaboratorManager manager)
        {
            this.manager = manager;
        }
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

        [HttpPost]
        [Route("api/RemoveCollaborator")]
        public IActionResult RemoveCollaborator([FromBody] List<int> collaborator)
        {
            try
            {
                string result = this.manager.RemoveCollaborator(collaborator);

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
    }
}

