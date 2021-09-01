using Models;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class NotesRespository : INotesRepository
    {
        private readonly UserContext userContext;
        public NotesRespository(UserContext userContext)
        {
            this.userContext = userContext;
        }
        public string AddNotes(NotesModel notes)
        {
            try
            {
                if (notes != null)
                {
                    //// add the data to the data base using user context 
                    this.userContext.Add(notes);
                    //// save the change in data base
                    this.userContext.SaveChanges();
                    return "Notes Added Succesfully";
                }

                return "Notes didn't get added";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<NotesModel> GetNotes(int id)
        {
            try
            {
                //checking the result using linq query user id has the notes 
                //if user id has n number of notes then push 
                var notes = this.userContext.Notes.Where(note => note.UserId == id && note.Trash == false && note.Archieve==false).ToList();
                return notes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool MoveToTrash(int noteId, int userId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.NotesId == noteId).FirstOrDefault();
                if (notes != null)
                {
                    notes.Trash = true;
                    this.userContext.Update(notes);
                    this.userContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool RestoreFromTrash(int noteId, int userId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.NotesId == noteId && note.Trash==true).FirstOrDefault();
                if (notes != null)
                {
                    notes.Trash = false;
                    this.userContext.Update(notes);
                    this.userContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool MoveToArchieve(int noteId, int userId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.NotesId == noteId && note.Trash==false).FirstOrDefault();
                if (notes != null)
                {
                    notes.Archieve = true;
                    this.userContext.Update(notes);
                    this.userContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool UnArchive(int noteId, int userId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.NotesId == noteId && note.Trash == false).FirstOrDefault();
                if (notes != null)
                {
                    notes.Archieve = false;
                    this.userContext.Update(notes);
                    this.userContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool PinNotes(int noteId, int userId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.NotesId == noteId && note.Trash == false && note.Pin==false).FirstOrDefault();
                if (notes != null)
                {
                    notes.Pin = true;
                    this.userContext.Update(notes);
                    this.userContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}