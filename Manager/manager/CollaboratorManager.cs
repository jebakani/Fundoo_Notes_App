using Manager.Interface;
using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Manager
{
    public class CollaboratorManager:ICollaboratorManager
    {

        private readonly ICollaboratorRepository repository;
        public CollaboratorManager(ICollaboratorRepository repository)
        {
            this.repository = repository;
        }

        public string AddCollaborator(CollaboratorModel collaborator)
        {
           return this.repository.AddCollaborator(collaborator);
        }
        public string RemoveCollaborator(int collaboratorId)
        {
            return this.repository.RemoveCollaborator(collaboratorId);
        }
        public List<CollaboratorModel> GetCollaborator(int NoteId)
        {
            return this.repository.GetCollaborator(NoteId);
        }
    }
}
