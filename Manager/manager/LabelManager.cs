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
    public class LabelManager:ILabelManager
    {
        private readonly ILabelRepository repository;

        public LabelManager(ILabelRepository repository)
        {
            this.repository = repository;
        }
        public string CreateLabel(LabelModel label)
        {
            return this.repository.CreateLabel(label);
        }
        public string AddLabel(LabelModel label)
        {
            return this.repository.AddLabel(label);
        }
        public string RemoveLabel(int lableId)
        {
            return this.repository.RemoveLabel(lableId);
        }
        public string DeleteLabel(int labelId)
        {
            return this.repository.DeleteLabel(labelId);
        }
    }
}
