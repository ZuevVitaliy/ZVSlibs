using System;
using System.Collections.Generic;
using System.Text;

namespace ZVS.UndoRedo
{
    public interface IUndoRedoCommand
    {
        void Do();
        void Undo();
        void Redo();
        void Save();
    }
}
