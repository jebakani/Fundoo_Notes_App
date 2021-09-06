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

    }
}
