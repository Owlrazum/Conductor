using System.Collections;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;

using GeneralTemplate;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(MeshRenderer))]
public class Passenger : MonoBehaviour
{
    [SerializeField]
    private Transform ticketIndicator;

    private NavMeshAgent agent;
    private MeshRenderer rend;

    private Bus occupiedBus;
    private Seat occupiedSeat;
    private Ticket ticket;

    private Vector3 sitPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        agent.autoBraking = true;

        rend = GetComponent<MeshRenderer>();

        ticket = new Ticket();
    }

    public void GoToSit(Seat seat, Bus bus)
    {
        if (occupiedSeat != null)
        {
            occupiedSeat.BecomeFree();
        }
        occupiedSeat = seat;

        occupiedBus = bus;

        agent.enabled = true;
        sitPosition = seat.GetPositionToStartSitting();
        agent.destination = sitPosition;
        transform.parent = seat.transform;
        StartCoroutine(GoingToSeatCoroutine());
    }

    private IEnumerator GoingToSeatCoroutine()
    {
        while (agent.remainingDistance > 0.1f || agent.pathPending)
        {
            yield return null;
            if (!agent.enabled)
            {
                while (!agent.enabled)
                {
                    yield return null;
                }
            }
        }
        Sit();
    }

    private void Sit()
    {
        transform.position = sitPosition;
        agent.enabled = false;
        occupiedBus.PassengerReady();
    }

    public void ChangeMaterialForPreparation(Material mat)
    {
        rend.material = mat;
    }

    public void SearchForFreeSeat(float time, float startSlowTime)
    {
        StartCoroutine(SlowDownCoroutine(time, startSlowTime));
    }

    private IEnumerator SlowDownCoroutine(float time, float startSlowTime)
    {
        yield return new WaitForSeconds(startSlowTime);
        float prevSpeed = agent.speed;
        agent.speed = 0.5f;
        yield return new WaitForSeconds(time);
        agent.speed = prevSpeed;
    }

    public bool CheckTicket()
    {
        return ticket.IsValid;
    }

    public Ticket GiveTicketAndStop()
    {
        agent.enabled = false;
        return ticket;
    }

    public void ReceiveTicket(Ticket newTicket)
    {
        ticket = newTicket;
        agent.enabled = true;
        agent.destination = sitPosition;
        ticketIndicator.gameObject.SetActive(true);
    }
}
