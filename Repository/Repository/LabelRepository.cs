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
    }
}
