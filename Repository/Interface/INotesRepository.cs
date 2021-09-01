﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface INotesRepository
    {
        string AddNotes(NotesModel notes);
        List<NotesModel> GetNotes(int id);
        bool DeleteNote(int noteId, int userId);
    }
}
