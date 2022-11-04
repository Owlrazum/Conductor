using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    [SerializeField, Tooltip("By default it is child at index 0")]
    private Transform startSitTransform;

    private SeatsController seatsManager;

    public int SeatIndex { get; private set; }

    private void Awake()
    {
        if (startSitTransform == null)
        {
            startSitTransform = transform.GetChild(0);
        }
    }

    public void AssignSeatsManager(SeatsController seatsManagerArg)
    {
        seatsManager = seatsManagerArg;
    }

    public void AssignIndex(int seatIndexArg)
    {
        SeatIndex = seatIndexArg;
    }

    public void BecomeFree()
    {
        seatsManager.SeatBecameFree(SeatIndex);
    }

    public Vector3 GetPositionToStartSitting()
    {
        return startSitTransform.position;
    }
}
