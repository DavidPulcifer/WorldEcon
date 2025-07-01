using UnityEngine;
using WorldEcon.World;

namespace WorldEcon.Actions
{
    /// <summary>
    /// Base data container for GOAP actions.  
    /// Define preconditions/effects here; runtime behavior via a wrapper.
    /// </summary>
    [CreateAssetMenu(fileName = "New Action", menuName = "Action")]
    public class ActionScriptableObj : ScriptableObject
    {
        [Header("Identification")]
        [Tooltip("Unique name shown in logs and UI.")]
        public string actionName = "New Action";

        [Tooltip("Planner cost (lower = more desirable).")]
        public float cost = 1.0f;

        [Tooltip("How long, in seconds, this action takes to complete.")]
        public float duration = 0;

        [Tooltip("Optional icon for debugging/UI.")]
        public Sprite icon;

        [Header("World State Preconditions")]
        [Tooltip("Key–value pairs that must be true before execution.")]
        public WorldState[] preConditions;

        [Header("World State Effects")]
        [Tooltip("Key–value pairs that will be applied on completion.")]
        public WorldState[] afterEffects;

        public GameObject target;
        public string targetTag;
    }
}