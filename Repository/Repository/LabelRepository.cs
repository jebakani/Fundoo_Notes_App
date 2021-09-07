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
    public class LabelRepository : ILabelRepository
    {
        private readonly UserContext userContext;
        public LabelRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public string CreateLabel(LabelModel label)
        {
            try
            {
                var labels = this.userContext.Label.Where(x => x.LabelName.Equals(label.LabelName) && x.UserId==label.UserId && x.NoteId==label.NoteId).SingleOrDefault();
                if(labels==null)
                { 
                    this.userContext.Label.Add(label);
                    this.userContext.SaveChanges();
                    return ("Label is added");
                }
                return ("label already Exists");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string AddLabel(LabelModel label)
        {
            try
            {
                var noteId = label.NoteId;
                label.NoteId = null;
                CreateLabel(label);
                label.NoteId = noteId;
                label.LabelId = 0;
                return (CreateLabel(label));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string RemoveLabel(int lableId)
        {
            try
            {
              
                var noteLabel = this.userContext.Label.Where(x =>x.LabelId==lableId).SingleOrDefault();
                if (noteLabel != null)
                {
                    this.userContext.Label.Remove(noteLabel);
                    this.userContext.SaveChanges();
                    return ("Label is removed");
                }
                return "Remove label failed";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string DeleteLabel(int userId, string labelName)
        {
            try
            {
                var deleteLabel = this.userContext.Label.Where(x => x.LabelName.Equals(labelName)&& x.UserId==userId).ToList();
                if (deleteLabel != null)
                {
                    this.userContext.Label.RemoveRange(deleteLabel);
                    this.userContext.SaveChanges();
                    return ("Label is deleted");
                }
                return "Delete label failed";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<LabelModel> GetLabelByUserId(int userId)
        {
            try
            {
                var label = this.userContext.Label.Where(x => x.UserId == userId && x.NoteId == null).ToList();
                return label;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<LabelModel> GetLabelByNoteId(int noteId)
        {
            try
            {
                var label = this.userContext.Label.Where(x => x.NoteId == noteId).ToList();
                return label;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EditLabel(LabelModel label)
        {
            try
            {

                var updateLabel = this.userContext.Label.Where(x => x.LabelName.Equals((this.userContext.Label.Find(label.LabelId)).LabelName) && x.UserId == label.UserId).ToList();
                foreach(var l in updateLabel)
                {
                    l.LabelName = label.LabelName;
                    this.userContext.Label.Update(l);
                    this.userContext.SaveChanges();
                   
                }
                return ("Label is updated");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
