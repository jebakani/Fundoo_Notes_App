using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Interface
{
    public interface ILabelManager
    {
        string CreateLabel(LabelModel label);
        string AddLabel(LabelModel label);
        string RemoveLabel(int lableId);
    }
}
