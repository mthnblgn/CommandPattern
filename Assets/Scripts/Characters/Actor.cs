using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Actor : Movable
{
    public List<Command> _oldCommands;
    private void Awake()
    {
        _oldCommands = new List<Command>();
    }
    public void Undo()
    {
        _oldCommands.Last().Undo(this);
    }
    public override void  AddCommand(Command c)
    {
        _oldCommands.Add(c);
    }
    public int CommandCount()
    {
        return _oldCommands.Count;
    }

    public override void ClearCommands()
    {
        _oldCommands.Clear();
    }
    public void RemoveLastCommand()
    {
        if (_oldCommands.Count != 0)_oldCommands.RemoveAt(_oldCommands.Count - 1);
    }
}
