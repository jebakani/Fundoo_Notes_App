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
    public class NotesManager:INotesManager
    {
        private readonly INotesRepository repository;

        public NotesManager(INotesRepository repository)
        {
            this.repository = repository;
        }

        public string AddNotes(NotesModel notes)
        {
            return this.repository.AddNotes(notes);
        }

        public List<NotesModel> GetNotes(int id)
        {
            return this.repository.GetNotes(id);
        }

        public bool MoveToTrash(int noteId, int userId)
        {
            return this.repository.MoveToTrash(noteId, userId);
        }
        public bool RestoreFromTrash(int noteId, int userId)
        {
            return this.repository.RestoreFromTrash(noteId, userId);
        }
        public bool MoveToArchieve(int noteId, int userId)
        {
            return this.repository.MoveToArchieve(noteId, userId);
        }
        public bool UnArchive(int noteId, int userId)
        {
            return this.repository.UnArchive(noteId, userId);
        }
        public string PinAndUnpinNotes(int noteId, int userId)
        {
            return this.repository.PinAndUnpinNotes(noteId, userId);
        }
        public NotesModel UpdateNote(NotesUpdateModel updateNote)
        {
            return this.repository.UpdateNote(updateNote);
        }
    }
}
