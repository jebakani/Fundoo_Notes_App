using Microsoft.AspNetCore.Http;
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

        List<NotesModel> GetNotes(int userId);
        List<NotesModel> GetArchive(int userId);
        List<NotesModel> GetRemainder(int userId);
        List<NotesModel> GetTrash(int userId);


        string MoveToTrash(int noteId);
        string MoveToArchieve(int noteId);
        bool UnArchive(int noteId);
        bool RestoreFromTrash(int noteId);
        string PinAndUnpinNotes(int noteId);
        NotesModel UpdateNote(NotesUpdateModel updateNote);
        bool DeleteFromTrash(int noteId);
        bool UpdateColor(int noteId, string color);
        bool UpdateRemainder(int noteId, string remainder);
        bool DeleteRemainder(int noteId);
        bool EmptyTrash(int userId);
        bool AddImage(int noteId, IFormFile image);

    }
}
