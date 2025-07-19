using UnityEngine;

namespace WorldEcon.World.Resources
{
    [CreateAssetMenu(fileName = "Resource Data", menuName = "Resource Data", order = 51)]
    public class ResourceData : ScriptableObject
    {
        public string resourceTag;
        public bool isGatherable;
        public int maxInteractions = 1;

        [Header("Resource Spawner")]
        public GameObject prefabToSpawn;
        public int minSpawn;
        public int maxSpawn;
    }
}

