using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZVS.UndoRedo
{
    public class UndoRedoRepository<TCommand> where TCommand : IUndoRedoCommand
    {
        private readonly List<TCommand> _commands;
        private int _currentPosition;

        public UndoRedoRepository()
        {
            _commands = new List<TCommand>();
            _currentPosition = -1;
        }

        public bool CanUndo => _currentPosition >= 0;
        public bool CanRedo => _currentPosition < _commands.Count - 1;
        public int Count => _commands.Count;

        public void AddCommand(TCommand newCommand)
        {
            var passedCommands = _currentPosition + 1; // количество выполненных и не отмененных команд, а так же указывет на индекс первой отмененной команды
            for (int i = 0; i < _commands.Count - passedCommands; i++)
            {
                _commands.RemoveAt(passedCommands);
            }

            newCommand.Do();
            _commands.Add(newCommand);
        }

        public void Undo()
        {
            if (CanUndo)
            {
                _commands[_currentPosition--].Undo();
            }
        }

        public void Redo()
        {
            if (CanRedo)
            {
                _commands[_currentPosition++].Redo();
            }
        }

        public void Save()
        {
            for (int i = 0; i <= _currentPosition; i++)
            {
                _commands[i].Save();
            }

            Reset();
        }

        public void Cancel()
        {
            for (int i = _currentPosition; i >= 0; i--)
            {
                _commands[i].Undo();
            }

            Reset();
        }

        private void Reset()
        {
            _commands.Clear();
            _currentPosition = -1;
        }
    }
}
