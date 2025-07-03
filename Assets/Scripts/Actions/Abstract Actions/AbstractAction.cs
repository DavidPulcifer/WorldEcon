using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using WorldEcon.Entities;
using WorldEcon.World;
using WorldEcon.World.Resources;

namespace WorldEcon.Actions
{
    //GAction
    public abstract class AbstractAction : MonoBehaviour
    {
        public string actionName = "Action";
        public float cost = 1.0f;
        public ResourceData resourceData;
        public GameObject target;
        public string targetTag;
        public float duration = 0;
        public WorldState[] preConditions;
        public WorldState[] afterEffects;
        public NavMeshAgent agent;

        public Dictionary<string, int> preconditions;
        public Dictionary<string, int> effects;

        // public WorldStates agentBeliefs;

        // public Inventory inventory;
        // public WorldStates beliefs;

        public bool running = false;

        public Person AssignedPerson { get; private set; }

        public AbstractAction()
        {
            preconditions = new Dictionary<string, int>();
            effects = new Dictionary<string, int>();
        }

        void Awake()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            AssignedPerson = gameObject.GetComponent<Person>();

            if (preconditions != null)
            {
                foreach (WorldState worldState in preConditions)
                {
                    preconditions.Add(worldState.key, worldState.value);
                }
            }

            if (afterEffects != null)
            {
                foreach (WorldState worldState in afterEffects)
                {
                    effects.Add(worldState.key, worldState.value);
                }
            }

            // inventory = person.inventory;
            // beliefs = person.beliefs;
        }

        public bool CanDoAction()
        {
            return true;
        }

        public bool CanDoAction(Dictionary<string, int> conditions)
        {
            foreach (KeyValuePair<string, int> precondition in preconditions)
            {
                if (!conditions.ContainsKey(precondition.Key)) return false;
            }
            return true;
        }

        public abstract bool PrePerform();
        public abstract bool PostPerform();
    }
}