using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GeneralTemplate;

public class StoppingPlatform : MonoBehaviour
{
    [SerializeField]
    private PathPoint busPosition;

    [SerializeField]
    private Transform turnPointsParent;

    [SerializeField]
    private Transform passengersParent;

    private List<Passenger> waitingPassengers;

    private void Awake()
    {
        var array = passengersParent.GetComponentsInChildren<Passenger>();
        waitingPassengers = new List<Passenger>(array);
    }

    public List<Passenger> GetWaitingPassengers()
    {
        return waitingPassengers;
    }

    public List<PathPoint> GetPath()
    {
        List<PathPoint> toReturn;
        if (turnPointsParent.childCount > 0)
        {
            var array = turnPointsParent.GetComponentsInChildren<PathPoint>();
            toReturn = new List<PathPoint>(array);
        }
        else
        {
            toReturn = new List<PathPoint>();
        }
        toReturn.Add(busPosition);
        return toReturn;
    }

    public void BecomePrepared()
    {
        for (int i = 0; i < waitingPassengers.Count; i++)
        {
            Material mat = GameManager.Singleton.GetMaterialForPassenger();
            waitingPassengers[i].ChangeMaterialForPreparation(mat);;
        }
    }
}
