using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] GameController _gameController;
    private Command[] _executables;
    private Command fwd, bck, lft, rgt;
    [SerializeField] Actor _actor;

    void Awake()
    {
        fwd = new MoveForward();
        bck = new MoveBackward();
        lft = new MoveLeft();
        rgt = new MoveRight();
        _executables = new Command[4] { fwd, bck, rgt, lft };

    }
    private void Start()
    {
        GameController.phase = GameController.Phases.Guiding;
    }
    private void Update()
    {
        if (GameController.phase == GameController.Phases.Listening)
        {
            HandleInput();
        }
    }
    void HandleInput()
    {
        if (_actor.CommandCount() < _gameController.CurrentLevel().stepCount)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                fwd.Execute(_actor);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                bck.Execute(_actor);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                lft.Execute(_actor);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rgt.Execute(_actor);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                GameController.phase = GameController.Phases.Reverting;
            }
        }
        else if (!_gameController.isChecked)
        {
            GameController.phase = GameController.Phases.Checking;
            _gameController.isChecked = true;
        }
        if (Input.GetKeyDown(KeyCode.Backspace) && _actor.CommandCount() != 0)
        {
            _actor.Undo();
            _gameController.isChecked = false;
        }
    }
    public Command ReturnCommand(int number)
    {
        return _executables[number];
    }

}
