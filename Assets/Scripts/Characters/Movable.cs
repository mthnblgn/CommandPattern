using UnityEngine;
using static InputHandler;

public abstract class Movable : MonoBehaviour
{
    public Vector3 targetPos;
    public bool canItMove(Vector3 moveTo)
    {
        bool canItMove = true;
        if (moveTo.x < -20 || moveTo.x > 20 || moveTo.z < -20 || moveTo.z > 20)
        {
            canItMove = false;
        }
        return canItMove;
    }
    public void ChangeTarget(Vector3 t)
    {
        targetPos = t;
    }
    public bool IsInPosition()
    {
        bool a;
        a = (transform.position == targetPos)? true:false;
        return a;
    }
    public abstract void AddCommand(Command c);
    public abstract void ClearCommands();


}
