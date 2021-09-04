using Experimental.System.Messaging;
using Models;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CollaboratorRepository:ICollaboratorRepository
    {
        private readonly UserContext userContext;
        public CollaboratorRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public string AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                var ownerId = this.userContext.user.Where(u => u.id == (this.userContext.Notes
                       .Where(x => x.NotesId == collaborator.NoteId).Select(x => x.UserId).FirstOrDefault())&& u.Email==collaborator.EmailId).SingleOrDefault();
                if (ownerId==null)
                {
                    var result = this.userContext.Collaborators.Where(x => x.NoteId == collaborator.NoteId && x.EmailId == collaborator.EmailId).SingleOrDefault();
                    if (result == null)
                    {
                        this.userContext.Collaborators.Add(collaborator);
                        this.userContext.SaveChanges();
                        return "Collaborator is added";
                    }
                }
                return "email id already exist";
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string RemoveCollaborator(int collaboratorsId)
        {
            try
            {
                var collaborator = this.userContext.Collaborators.Where(x => x.ColId == collaboratorsId).SingleOrDefault();
                if (collaborator != null)
                {
                    this.userContext.Collaborators.Remove(collaborator);
                    this.userContext.SaveChanges();
                    return "Collaborator is deleted"
                }
                return "can't able to delete";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
