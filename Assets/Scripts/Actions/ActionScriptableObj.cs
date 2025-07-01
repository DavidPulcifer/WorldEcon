using UnityEngine;
using WorldEcon.World;

namespace WorldEcon.Actions
{
    [CreateAssetMenu(fileName = "New Action", menuName = "Action")]
    public class ActionScriptableObj : ScriptableObject
    {
        public string actionName = "Action";
        public float cost = 1.0f;
        public GameObject target;
        public string targetTag;
        public float duration = 0;
        public WorldState[] preConditions;
        public WorldState[] afterEffects;        
    }
}