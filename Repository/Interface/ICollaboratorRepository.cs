using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICollaboratorRepository
    {
        string AddCollaborator(CollaboratorModel collaborator);
        string RemoveCollaborator(int collaboratorId);
        List<CollaboratorModel> GetCollaborator(int NoteId);

    }
}
