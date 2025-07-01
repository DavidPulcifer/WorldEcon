using System.Collections.Generic;
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
        [Header("Action Info")]
        [Tooltip("Unique name shown in logs and UI.")]
        public string actionName = "New Action";

        [Tooltip("Planner cost (lower = more desirable).")]
        public float cost = 1.0f;

        [Tooltip("How long, in seconds, this action takes to complete.")]
        public float duration = 0;

        [Tooltip("Optional icon for debugging/UI.")]
        public Sprite icon;

        [Header("Preconditions")]
        [Tooltip("Key–value pairs that must be true before execution.")]
        public List<WorldState> preConditions = new List<WorldState>();

        [Header("Effects")]
        [Tooltip("Key–value pairs that will be applied on completion.")]
        public List<WorldState> afterEffects = new List<WorldState>();

        [Header("Targeting (optional)")]
        [Tooltip("If you want a specific GameObject target.")]
        public GameObject target;

        [Tooltip("Alternatively, find a target by Tag at runtime.")]
        public string targetTag;

         /// <summary>
        /// Quick check before adding to plan (e.g. agent has tool).
        /// </summary>
        public virtual bool IsValid() => true;

        /// <summary>
        /// Check just before execution (e.g. still in range).
        /// </summary>
        public virtual bool CanExecute() => true;

        /// <summary>
        /// Called once when the action actually starts.
        /// </summary>
        public virtual void OnEnter() { }

        /// <summary>
        /// Called each tick while the action is running.
        /// </summary>
        public virtual void OnExecute() { }

        /// <summary>
        /// Called once when the action finishes or is aborted.
        /// </summary>
        public virtual void OnExit() { }
    }
}