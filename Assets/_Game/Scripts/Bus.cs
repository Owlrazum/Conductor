using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SeatsController))]
[RequireComponent(typeof(BusMover))]
public class Bus : MonoBehaviour
{
    [SerializeField]
    private Transform stoppingPlatformsParent;

    [Header("Parameters")]
    [SerializeField]
    private float waitTimeForPassengers;

    private Queue<StoppingPlatform> stoppingPlatforms;

    private SeatsController seatsController;
    private BusMover busMover;

    private StoppingPlatform startPlatform;

    private int newPassengerCount;

    private void Awake()
    {
        var array =
            stoppingPlatformsParent.GetComponentsInChildren<StoppingPlatform>();
        stoppingPlatforms = new Queue<StoppingPlatform>(array);

        seatsController = GetComponent<SeatsController>();
        busMover = GetComponent<BusMover>();
        busMover.AssignBusToMove(this);

        startPlatform = stoppingPlatforms.Dequeue();
        stoppingPlatforms.Enqueue(startPlatform);

        startPlatform.BecomePrepared();
    }

    public void StartMove()
    {
        FinishedMovingToPlatform(startPlatform);
    }

    public void FinishedMovingToPlatform(StoppingPlatform platform)
    {
        var passengers = platform.GetWaitingPassengers();
        foreach (Passenger p in passengers)
        {
            Seat freeSeat = seatsController.GetFreeSeat();
            p.GoToSit(freeSeat, this);
        }

        newPassengerCount = passengers.Count;

        StartCoroutine(WaitAndMoveToNextPlatform());
    }

    public void PassengerReady()
    {
        newPassengerCount--;
    }

    private IEnumerator WaitAndMoveToNextPlatform()
    {
        if (waitTimeForPassengers > 0)
        {
            yield return new WaitForSeconds(waitTimeForPassengers);
        }
        else
        {
            yield return new WaitUntil(() => newPassengerCount == 0);
        }
        StartMovingBusToNextStoppingPlatform();
    }

    public void StartMovingBusToNextStoppingPlatform()
    {
        StoppingPlatform nextPlatform = stoppingPlatforms.Dequeue();
        stoppingPlatforms.Enqueue(nextPlatform);

        nextPlatform.BecomePrepared();
        busMover.MoveToStoppingPlatform(nextPlatform);
    }
}
