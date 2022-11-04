using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownTrigger : MonoBehaviour
{
    [SerializeField]
    private float slowDownTime;

    [Header("Dev")]
    [Space]
    [SerializeField]
    private float startSlowDownTime;

    private void OnTriggerEnter(Collider other)
    {
        Passenger passenger = other.GetComponent<Passenger>();
        if (passenger != null)
        {
            passenger.SearchForFreeSeat(slowDownTime, startSlowDownTime);
        }
    }
}
