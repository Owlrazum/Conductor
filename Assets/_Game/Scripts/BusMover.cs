using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using GeneralTemplate;

public class BusMover : MonoBehaviour
{
    [SerializeField]
    private Transform exterisorsParent;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float turnTime;

    private Transform positionPivotExteriors;
    private Transform rotationPivotExteriors;

    private StoppingPlatform moveToStoppingPlatform;
    private List<PathPoint> currentPath;
    private int currentIndex;

    private Bus movedBus;

    public void Start()
    {
        rotationPivotExteriors = exterisorsParent;
        positionPivotExteriors = exterisorsParent.GetChild(0);
    }

    public void AssignBusToMove(Bus bus)
    {
        movedBus = bus;
    }

    public void MoveToStoppingPlatform(StoppingPlatform stoppingPlatform)
    {
        print("Start moving to next platform" + stoppingPlatform);
        moveToStoppingPlatform = stoppingPlatform;
        currentPath = stoppingPlatform.GetPath();
        if (currentPath.Count == 1)
        {
            Vector3 finalPos = currentPath[0].transform.position;
            MoveToFinalPathPoint(finalPos, stoppingPlatform);
            currentPath = null;
            return;
        }
        currentIndex = -1;
        print("rec");
        MoveToNextPathPointRecursive();
    }

    private void MoveToNextPathPointRecursive()
    {
        currentIndex++;

        PathPoint targetPathPoint = currentPath[currentIndex];
        Vector3 targetPosition = targetPathPoint.transform.position;
        if (targetPathPoint.GetTurnType() == PointType.Final)
        {
            MoveToFinalPathPoint(targetPosition, moveToStoppingPlatform);
            return;
        }
        Vector3 delta = -1 * new Vector3(targetPosition.x, 0, targetPosition.z);
        Vector3 moveToPosition = positionPivotExteriors.position + delta;
        float timeToMove = delta.magnitude / moveSpeed;
        positionPivotExteriors.DOMove(moveToPosition, timeToMove).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            if (targetPathPoint.GetTurnType() == PointType.TurnToLeft)
            {
                TurnToLeft();
            }
            else if (targetPathPoint.GetTurnType() == PointType.TurnToRight)
            {
                TurnToRight();
            }
        });

        //Solution 1:

        // 2 point of rotation in scene in each turn;

        /* DOMove (moveToPosition, timeToMove).OnComplete(() =>
         * {
         *      rot.y = 0;
         *      
         *      Quaternion look = LookRotation(or, target);
         *      DORotate(look.euler, ).OnComplete(() =>
         *      {
         *      look = LookRotation(new Vector3(target, secondTarget));
         *          DORotate()
         *      });
         *      MoveToNextPathPointRecursive();
         * });
         * 
         * 
         */

        // Solution 2:

        // Beizer curve

        /*
         * 
         * 
         * 
         * 
         * 
         */

        // Solution 3

        // Parabola

        /*
         * 
         * 
         * 
         */ 

    }

    private void TurnToLeft()
    {
        Vector3 targetRotation = rotationPivotExteriors.rotation.eulerAngles;
        Vector3 pushedPos = transform.position;
        targetRotation.y += 90;

        rotationPivotExteriors.DORotate(targetRotation, turnTime).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            MoveToNextPathPointRecursive();
        });
    }

    private void TurnToRight()
    {
        Vector3 targetRotation = positionPivotExteriors.rotation.eulerAngles;
        targetRotation.y -= 90;
        rotationPivotExteriors.DORotate(targetRotation, turnTime).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            MoveToNextPathPointRecursive();
        });
    }

    private void MoveToFinalPathPoint(Vector3 targetPosition, StoppingPlatform stoppingPlatform)
    {
        Vector3 delta = -1 * new Vector3(targetPosition.x, 0, targetPosition.z);
        Vector3 moveToPosition = positionPivotExteriors.position + delta;
        float timeToMove = delta.magnitude / moveSpeed;
        positionPivotExteriors.DOMove(moveToPosition, timeToMove).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            movedBus.FinishedMovingToPlatform(stoppingPlatform);
        });
    }
}
