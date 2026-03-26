using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tartarus
{
    public class WorldPathManager : MonoBehaviour
    {
        public static WorldPathManager instance;

        [Header ("Paths")]
        public List<List<Transform>> paths = new List<List<Transform>>();

        [Header("Path 1")]
        public List<Transform> path1 = new List<Transform>();
        public List<Transform> path2 = new List<Transform>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

            paths.Add(path1);
            paths.Add(path2);

        }

        public List<Transform> GetPath(int index)
        {
            return paths.ElementAt(index);
        }

    }
}
