using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class Guide : Movable
{
    [SerializeField] InputHandler _inputHandler;
    [SerializeField] GameController _gameController;
    public List<Command> _guidePath;
    int _currentGuideStep = 0;
    private void Awake()
    {
        _guidePath = new List<Command>();
    }
    private void Update()
    {
        if (GameController.phase == GameController.Phases.Guiding)
        {
            if (_currentGuideStep == _gameController.CurrentLevel().stepCount)
            {
                GameController.phase = GameController.Phases.Listening;
                _gameController.ChangeMovingCharacter();
            }
            else PickACommand().Execute(this);
        }
        else if (GameController.phase == GameController.Phases.Reverting)
        {
            Revert();
        }
    }

    private Command PickACommand()
    {
        int a = Random.Range(0, 4);
        return _inputHandler.ReturnCommand(a);
    }
    public override void AddCommand(Command c)
    {
        _guidePath.Add(c);
        _currentGuideStep++;
    }

    public override void ClearCommands()
    {
        _currentGuideStep = 0;       
        _guidePath.Clear();
    }
    //Revertemedin!
    public void Revert()
    {
        for (int i = _guidePath.Count-1; i >=0 ; i--)
        {
            _guidePath[i].Undo(this);
        }
    }


}
