using UnityEngine;

namespace WorldEcon.World.Resources
{
    [CreateAssetMenu(fileName = "Resource Data", menuName = "Resource Data", order = 51)]
    public class ResourceData : ScriptableObject
    {
        public string resourceTag;
        public string resourceQueue;
        public string resourceState;
        public bool isGatherable;
        public int maxInteractions = 1;
    }
}

