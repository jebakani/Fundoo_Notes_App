﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ILabelRepository
    {
        string CreateLabel(LabelModel label);
        string AddLabel(LabelModel label);
        string RemoveLabel(int lableId);
        string DeleteLabel(int userId, string labelName);
        List<LabelModel> GetLabelByUserId(int userId);
        List<LabelModel> GetLabelByNoteId(int noteId);

    }
}
