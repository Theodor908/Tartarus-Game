using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class EventTriggerBossFight : MonoBehaviour
    {
        [SerializeField] int bossID;
        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player"))
            {
                AICharacterBossManager boss = WorldAIManager.instance.GetBossByID(bossID);

                if (boss != null)
                {
                    boss.WakeBoss();
                }
 
            }

        }
    }
}