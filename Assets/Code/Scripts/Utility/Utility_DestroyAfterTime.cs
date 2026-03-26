using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class Utility_DestroyAfterTime : MonoBehaviour
    {

        [SerializeField] float timeToDestroy = 5;

        private void Awake()
        {
            Destroy(gameObject, timeToDestroy);
        }
    }
}