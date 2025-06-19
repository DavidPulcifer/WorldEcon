using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEcon.World
{
    public sealed class WorldEnvironment
    {
        static readonly WorldEnvironment instance = new WorldEnvironment();
        static WorldStates worldEnvironmentStates;
        static Queue<GameObject> citizens;
        static Queue<GameObject> resources;

        static WorldEnvironment()
        {
            worldEnvironmentStates = new WorldStates();
            citizens = new Queue<GameObject>();
            resources = new Queue<GameObject>();

            GameObject[] resourceObjects = GameObject.FindGameObjectsWithTag("Resource");
            foreach (GameObject resourceObject in resourceObjects) resources.Enqueue(resourceObject);

            if (resourceObjects.Length > 0) worldEnvironmentStates.ModifyWorldState("FreeResource", resourceObjects.Length);
            Debug.Log(resourceObjects.Length);
        }

        private WorldEnvironment()
        {

        }

        public void AddCitizen(GameObject citizen)
        {
            citizens.Enqueue(citizen);
        }

        public GameObject RemoveCitizen()
        {
            if (citizens.Count == 0) return null;
            return citizens.Dequeue();
        }

        public void AddResource(GameObject resource)
        {
            resources.Enqueue(resource);
        }

        public GameObject RemoveResource()
        {
            if (resources.Count == 0) return null;
            return resources.Dequeue();
        }

        public static WorldEnvironment Instance
        {
            get { return instance; }
        }

        public WorldStates GetWorldEnvironment()
        {
            return worldEnvironmentStates;
        }
    }
}