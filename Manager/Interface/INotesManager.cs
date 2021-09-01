using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Interface
{
    public interface INotesManager
    {
        string AddNotes(NotesModel notes);

        List<NotesModel> GetNotes(int id);
        bool MoveToTrash(int noteId, int userId);
        bool MoveToArchieve(int noteId, int userId);
        bool UnArchive(int noteId, int userId);
        bool RestoreFromTrash(int noteId, int userId);
        string PinAndUnpinNotes(int noteId, int userId);
        NotesModel UpdateNote(NotesUpdateModel updateNote);
        bool DeleteFromTrash(int noteId, int userId);

    }
}
