using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controller
{
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager manager;
        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }
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
        [HttpGet]
        [Route("api/GetLabelForUser")]
        public IActionResult GetLabelForUser(int userId)
        {
            try
            {
                var result = this.manager.GetLabelByUserId(userId);

                if (result.Count>0)
                { 
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new { Status = true, Message = "Labels are retrived" ,Data=result});
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
        [HttpPut]
        [Route("api/UpdateLabel")]
        public IActionResult UpdateLabel([FromBody]LabelModel label)
        {
            try
            {
                var result = this.manager.EditLabel(label);

                if (result.Equals("Label is updated"))
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message =result});
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
    }
}
