using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Interface
{
    public interface ICollaboratorManager
    {
        string AddCollaborator(CollaboratorModel collaborator);
        string RemoveCollaborator(int collaboratorId);
        List<CollaboratorModel> GetCollaborator(int NoteId);


    }
}
