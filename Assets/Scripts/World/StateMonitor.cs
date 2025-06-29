using UnityEngine;
using WorldEcon.Actions;
using WorldEcon.Entities;

namespace WorldEcon.World
{
    public class StateMonitor : MonoBehaviour
    {
        public string state;
        public float stateStatus;
        public float stateDecayRate;
        public WorldStates beliefs;
        public GameObject resourcePrefab;
        public string queueName;
        public string worldState;
        public AbstractAction action;

        bool stateFound = false;
        float initialStateStatus;

        void Awake()
        {
            beliefs = GetComponent<Person>().beliefs;
            initialStateStatus = stateStatus;
        }

        void LateUpdate()
        {
            if (action.running)
            {
                stateFound = false;
                stateStatus = initialStateStatus;
            }

            if (!stateFound && beliefs.HasWorldState(state)) stateFound = true;

            if (stateFound)
            {
                stateStatus -= stateDecayRate * Time.deltaTime;
                if (stateStatus <= 0)
                {
                    Vector3 location = new Vector3(transform.position.x, resourcePrefab.transform.position.y, transform.position.z);
                    GameObject spawnedPrefab = Instantiate(resourcePrefab, location, resourcePrefab.transform.rotation);
                    stateFound = false;
                    stateStatus = initialStateStatus;
                    beliefs.RemoveWorldState(state);
                    WorldEnvironment.Instance.GetResourceQueue(queueName).AddResource(spawnedPrefab);
                    WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState(worldState, 1);
                }
            }
        }
    }
}