using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Movable whoToMove);
    public abstract void Undo(Movable whoToMove);


    protected void MovementExecution(Movable whoToMove, Vector3 direction)
    {
        bool cim = IsExecutable(whoToMove, direction, out Vector3 targetPos);
        if (cim)
        {
            whoToMove.ChangeTarget(targetPos);
            GameController.whoToMove = whoToMove;
            GameController.phase = GameController.Phases.Moving;
            whoToMove.AddCommand(this);
        }
    }
    protected void UndoExecution(Movable whoToMove, Vector3 direction)
    {
        bool IsActor = whoToMove.tag == "Actor";
        Vector3 targetPos = IsActor ? whoToMove.transform.position + direction * -10 : whoToMove.transform.position + direction * 10;
        whoToMove.ChangeTarget(targetPos);
        GameController.whoToMove = whoToMove;
        GameController.lastPhase= GameController.phase;
        GameController.phase = GameController.Phases.Moving;
        if (IsActor) whoToMove.GetComponent<Actor>().RemoveLastCommand();

    }
    private bool IsExecutable(Movable whoToMove, Vector3 direction, out Vector3 targetPos)
    {
        targetPos = (whoToMove.tag == "Actor") ? whoToMove.transform.position + direction * 10 : whoToMove.transform.position + direction * -10;
        return whoToMove.canItMove(targetPos);
    }
}
public class MoveForward : Command
{
    public override void Execute(Movable whoToMove)
    {
        MovementExecution(whoToMove, Vector3.forward);
    }

    public override void Undo(Movable whoToMove)
    {
        UndoExecution(whoToMove, Vector3.forward);
    }
}
public class MoveBackward : Command
{
    public override void Execute(Movable whoToMove)
    {
        MovementExecution(whoToMove, Vector3.back);

    }

    public override void Undo(Movable whoToMove)
    {
        UndoExecution(whoToMove, Vector3.back);
    }
}
public class MoveRight : Command
{
    public override void Execute(Movable whoToMove)
    {
        MovementExecution(whoToMove, Vector3.right);

    }
    public override void Undo(Movable whoToMove)
    {
        UndoExecution(whoToMove, Vector3.right);
    }

}
public class MoveLeft : Command
{
    public override void Execute(Movable whoToMove)
    {
        MovementExecution(whoToMove, Vector3.left);
    }
    public override void Undo(Movable whoToMove)
    {
        UndoExecution(whoToMove, Vector3.left);
    }
}




