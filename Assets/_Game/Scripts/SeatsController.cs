using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatsController : MonoBehaviour
{
    [SerializeField]
    private Transform seatsParent;

    private List<Seat> seats;

    private List<int> freeSeats;

    private void Awake()
    {
        InitializeSeats(seatsParent);
    }

    public void InitializeSeats(Transform seatsParent)
    {
        var seatsArray = seatsParent.GetComponentsInChildren<Seat>();
        seats = new List<Seat>(seatsArray);

        freeSeats = new List<int>();
        for (int i = 0; i < seats.Count; i++)
        {
            seats[i].AssignSeatsManager(this);
            seats[i].AssignIndex(i);
            freeSeats.Add(i);
        }
    }

    public Seat GetFreeSeat()
    {
        int rnd = Random.Range(0, freeSeats.Count);
        int seatIndex = freeSeats[rnd];
        Seat toReturn = seats[seatIndex];
        freeSeats.RemoveAt(rnd);
        return toReturn;
    }

    public void SeatBecameFree(int seatIndex)
    {
        freeSeats.Add(seatIndex);
    }
}
