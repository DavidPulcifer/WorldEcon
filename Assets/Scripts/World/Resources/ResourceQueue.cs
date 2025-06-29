using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WorldEcon.World.Resources
{
    public class ResourceQueue
    {
        public Queue<GameObject> resourceQueue = new Queue<GameObject>();
        public string tag;
        public string modState;

        public ResourceQueue(string tag, string modState, WorldStates worldStates)
        {
            this.tag = tag;
            this.modState = modState;
            if (tag != "")
            {
                GameObject[] resources = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject resource in resources)
                {
                    resourceQueue.Enqueue(resource);
                }
            }

            if (modState != "")
            {
                worldStates.ModifyWorldState(modState, resourceQueue.Count);
            }
        }

        public void AddResource(GameObject resource)
        {
            resourceQueue.Enqueue(resource);
        }

        public void RemoveResource(GameObject resource)
        {
            resourceQueue = new Queue<GameObject>(resourceQueue.Where(p => p != resource));
        }

        public GameObject RemoveResource()
        {
            if (resourceQueue.Count == 0) return null;
            return resourceQueue.Dequeue();
        }
    }
}