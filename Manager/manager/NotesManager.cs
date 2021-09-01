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
    }
}
