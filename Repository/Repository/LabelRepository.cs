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
                var labels = this.userContext.Label.Where(x => x.LabelName.Equals(label.LabelName) && x.UserId==label.UserId).SingleOrDefault();
                if(labels==null)
                {
                    label.NoteId = null;
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
                CreateLabel(label);
                label.NoteId = noteId;
                label.LabelId = 0;
                var noteLabel = this.userContext.Label.Where(x => x.LabelName.Equals(label.LabelName) && x.NoteId == label.NoteId).SingleOrDefault();
                if (noteLabel == null)
                {
                    this.userContext.Label.Add(label);
                    this.userContext.SaveChanges();
                    return ("Label is added");
                }
                return "Label already exists";
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
        public string DeleteLabel(int lableId)
        {
            try
            {

                var noteLabel = this.userContext.Label.Where(x => x.LabelId == lableId).SingleOrDefault();
                var deleteLabel = this.userContext.Label.Where(x => x.LabelName.Equals(noteLabel.LabelName)).ToList();
                if (noteLabel != null)
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
    }
}
