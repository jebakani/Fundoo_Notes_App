using Models;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class NotesRespository:INotesRepository
    {
        private readonly UserContext userContext;
        public NotesRespository(UserContext userContext)
        {
            this.userContext = userContext;
        }
        public string AddNotes(NotesModel notes)
        {
            try
            {
                if (notes != null)
                {
                    //// add the data to the data base using user context 
                    this.userContext.Add(notes);
                    //// save the change in data base
                    this.userContext.SaveChanges();
                    return "Notes Added Succesfully";
                }

                return "Notes didn't get added";
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
