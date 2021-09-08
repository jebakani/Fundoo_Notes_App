// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Repository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using global::Repository.Context;
    using global::Repository.Interface;

    /// <summary>
    /// User repository class that execute the query and connect with label database
    /// </summary>
    public class LabelRepository : ILabelRepository
    {
        /// <summary>
        /// create the object for user context
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// getting user context object through constructor
        /// Initializes a new instance of the <see cref="LabelRepository"/> class
        /// </summary>
        /// <param name="userContext">user context object that has connection with database Context</param>
        public LabelRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        /// <summary>
        /// the method to create the Label
        /// </summary>
        /// <param name="label">Label model object with id,name,user id and note Id</param>
        /// <returns>success or fail message</returns>
        public string CreateLabel(LabelModel label)
        {
            try
            {
                var labels = this.userContext.Label.Where(x => x.LabelName.Equals(label.LabelName) && x.UserId == label.UserId && x.NoteId == label.NoteId).SingleOrDefault();
                if (labels == null)
                { 
                    this.userContext.Label.Add(label);
                    this.userContext.SaveChanges();
                    return "Label is added";
                }

                return "label already Exists";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// the method to Add the Label
        /// </summary>
        /// <param name="label">Label model object with id,name,user id and note Id</param>
        /// <returns>success or fail message</returns>
        /// <summary>
        /// the method to Add the Label
        /// </summary>
        /// <param name="label">Label model object with id,name,user id and note Id</param>
        /// <returns>success or fail message</returns>
        public string AddLabel(LabelModel label)
        {
            try
            {
                var labels = this.userContext.Label.Where(x => x.LabelName.Equals(label.LabelName) && x.UserId == label.UserId && x.NoteId == label.NoteId).SingleOrDefault();
                if (labels == null)
                {
                    this.CreateLabel(label);
                    label.NoteId = null;
                    label.LabelId = 0;
                    return this.CreateLabel(label);
                }
                return "Label already exists";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// the method to remove the Label
        /// </summary>
        /// <param name="lableId">Label id that is unique</param>
        /// <returns>success or fail message</returns>
        public string RemoveLabel(int lableId)
        {
            try
            {
                var noteLabel = this.userContext.Label.Where(x => x.LabelId == lableId).SingleOrDefault();
                if (noteLabel != null)
                {
                    this.userContext.Label.Remove(noteLabel);
                    this.userContext.SaveChanges();
                    return "Label is removed";
                }

                return "Remove label failed";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// the method to delete the Label
        /// </summary>
        /// <param name="userId">user id as integer</param>
        /// <param name="labelName">label name as string</param>
        /// <returns>success or fail message</returns>
        public string DeleteLabel(int userId, string labelName)
        {
            try
            {
                var deleteLabel = this.userContext.Label.Where(x => x.LabelName.Equals(labelName) && x.UserId == userId).ToList();
                if (deleteLabel != null)
                {
                    this.userContext.Label.RemoveRange(deleteLabel);
                    this.userContext.SaveChanges();
                    return "Label is deleted";
                }

                return "Delete label failed";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// the method to get the Label
        /// </summary>
        /// <param name="userId">user id as integer</param>
        /// <returns>List of label in label model</returns>
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

        /// <summary>
        /// the method to get the Label
        /// </summary>
        /// <param name="noteId">note id as integer</param>
        /// <returns>List of label in label model</returns>
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

        /// <summary>
        /// the method to edit the Label
        /// </summary>
        /// <param name="label">Label model object with id,name,user id and note Id</param>
        /// <returns>success or fail message</returns>
        public string EditLabel(LabelModel label)
        {
            try
            {
                var oldName = this.userContext.Label.Find(label.LabelId).LabelName;
                var updateLabel = this.userContext.Label.Where(x => x.LabelName.Equals(oldName) && x.UserId == label.UserId).ToList();
                if (updateLabel == null)
                {
                    var checkLabelName = this.userContext.Label.Where(x => x.LabelName.Equals(label.LabelName) && x.UserId == label.UserId).FirstOrDefault();
                    foreach (var l in updateLabel)
                    {
                        l.LabelName = label.LabelName;
                        this.userContext.Label.Update(l);
                    }
                    if (checkLabelName != null)
                    {
                        this.userContext.Label.Remove(this.userContext.Label.Find(label.LabelId));
                        this.userContext.SaveChanges();
                        return "Merge the " + oldName + " label with the " + label.LabelName + " label? All notes labeled with " + oldName + " will be labeled with " + label.LabelName + " and the " + oldName + " label will be deleted.";
                    }
                    this.userContext.SaveChanges();
                    return "Label is updated";
                }
                return "No Label available";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
