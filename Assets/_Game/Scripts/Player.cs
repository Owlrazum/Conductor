using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GeneralTemplate;
using System;

namespace GeneralTemplate
{
    [RequireComponent(typeof(MoveMobile))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float ticketGivingTime;

        private MoveMobile movementComponent;

        private bool isGivingTicket;
        private Passenger interactingWithPassenger;
        private Ticket ticketToValidate;

        private void Start()
        {
            movementComponent = GetComponent<MoveMobile>();
        }

        public void ProcessGameEnd(GameResult result)
        {
            throw new NotImplementedException();
        }

        public void UpdateMovementInput(float inputX, float inputZ)
        {
            movementComponent.UpdateMoveDirection(inputX, inputZ);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isGivingTicket)
            {
                return;
            }
            Passenger passenger = other.GetComponent<Passenger>();
            if (passenger != null)
            {
                bool isValidTicket = passenger.CheckTicket();
                if (!isValidTicket)
                {
                    interactingWithPassenger = passenger;
                    ticketToValidate = interactingWithPassenger.GiveTicketAndStop();
                    StartCoroutine(PrepareTicketCoroutine());
                }
            }
        }

        private IEnumerator PrepareTicketCoroutine()
        {
            isGivingTicket = true;
            movementComponent.StopMoving();
            yield return new WaitForSeconds(ticketGivingTime);
            isGivingTicket = false;
            movementComponent.ResumeMoving();

            ticketToValidate.IsValid = true;
            interactingWithPassenger.ReceiveTicket(ticketToValidate);
        }
    }
}

