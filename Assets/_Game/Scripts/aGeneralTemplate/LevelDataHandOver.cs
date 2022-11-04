using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralTemplate
{
    public class LevelDataHandOver : MonoBehaviour
    {
        [SerializeField]
        private Player player;

        [SerializeField]
        private Bus bus;

        [SerializeField]
        private PassengerMaterials passMats;

        private void Awake()
        {
            GameManager.Singleton.AssignPlayerInstance(player);

            if (bus != null)
            {
                GameManager.Singleton.AssignBus(bus);
            }

            if (passMats != null)
            {
                GameManager.Singleton.AssignPassMats(passMats);
            }
        }
    }
}
