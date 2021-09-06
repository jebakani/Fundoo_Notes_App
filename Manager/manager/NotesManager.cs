﻿using Manager.Interface;
using Microsoft.AspNetCore.Http;
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

        public List<NotesModel> GetNotes(int userId)
        {
            return this.repository.GetNotes(userId);
        }
        public List<NotesModel> GetArchive(int userId)
        {
            return this.repository.GetArchive(userId);
        }
        public List<NotesModel> GetRemainder(int userId)
        {
            return this.repository.GetRemainder(userId);
        }
        public List<NotesModel> GetTrash(int userId)
        {
            return this.repository.GetTrash(userId);
        }
        public string MoveToTrash(int noteId)
        {
            return this.repository.MoveToTrash(noteId);
        }
        public bool RestoreFromTrash(int noteId)
        {
            return this.repository.RestoreFromTrash(noteId);
        }
        public string MoveToArchieve(int noteId)
        {
            return this.repository.MoveToArchieve(noteId);
        }
        public bool UnArchive(int noteId)
        {
            return this.repository.UnArchive(noteId);
        }
        public string PinAndUnpinNotes(int noteId)
        {
            return this.repository.PinAndUnpinNotes(noteId);
        }
        public NotesModel UpdateNote(NotesUpdateModel updateNote)
        {
            return this.repository.UpdateNote(updateNote);
        }
       public bool DeleteFromTrash(int noteId)
        {
            return this.repository.DeleteFromTrash(noteId);
        }
        public bool UpdateColor(int noteId, string color)
        {
            return this.repository.UpdateColor(noteId, color);
        }
        public bool UpdateRemainder(int noteId, string remainder)
        {
            return this.repository.UpdateRemainder(noteId, remainder);
        }
        public bool DeleteRemainder(int noteId)
        {
            return this.repository.DeleteRemainder(noteId);
        }
        public bool EmptyTrash(int userId)
        {
            return this.repository.EmptyTrash(userId);
        }
        public bool AddImage(int noteId, IFormFile image)
        {
            return this.repository.AddImage(noteId, image);
        }
        public bool DeleteImage(int noteId)
        {
            return this.repository.DeleteImage(noteId);
        }
    }
}
