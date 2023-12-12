using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Actor _actor;
    [SerializeField] Guide _guide;
    [SerializeField] Level[] _levels;
    Phases _lastPhase;
    public int currentlevel = 0;
    public bool isChecked = false;
    public enum Phases
    {
        Guiding,
        Listening,
        Moving,
        Checking,
        Reverting
    }
    public static Phases lastPhase;
    public static Phases phase;
    public static Movable whoToMove;
    private void FixedUpdate()
    {
        if (phase == Phases.Moving)
        {
            Move();
        }
        if (phase == Phases.Checking)
        {
            bool check = true;
            for (int i = 0; i < CurrentLevel().stepCount; i++)
            {
                if (_actor._oldCommands[i] != _guide._guidePath[i])
                {
                    check = false;
                }
            }
            if (check)
            {
                print("aynen");
                NewLevel();

            }
            else
            {
                print("tekrar düþün");
                phase = Phases.Listening;
            }
        }
    }

    private void NewLevel()
    {
        _guide.ClearCommands();
        _actor.ClearCommands();
        ChangeMovingCharacter();
        phase = Phases.Guiding;
        isChecked = false;
        currentlevel++;
    }

    public void Move()
    {
        whoToMove.transform.position = Vector3.MoveTowards(whoToMove.transform.position, whoToMove.targetPos, 0.3f * CurrentLevel().stepSpeed);
        if (whoToMove.IsInPosition())
        {
            ReturnToLastPhase();
        }
    }

    private void ReturnToLastPhase()
    {
        phase = _lastPhase;
    }

    public void ChangeMovingCharacter()
    {
        whoToMove = (whoToMove == _actor) ? _guide : _actor;
    }
    public Level CurrentLevel()
    {
        return _levels[currentlevel];
    }


}
