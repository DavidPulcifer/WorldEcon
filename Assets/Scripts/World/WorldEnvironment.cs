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
        static Queue<GameObject> offices;
        static Queue<GameObject> toilets;

        static WorldEnvironment()
        {
            worldEnvironmentStates = new WorldStates();
            citizens = new Queue<GameObject>();
            resources = new Queue<GameObject>();
            offices = new Queue<GameObject>();
            toilets = new Queue<GameObject>();

            GameObject[] resourceObjects = GameObject.FindGameObjectsWithTag("Resource");
            foreach (GameObject resourceObject in resourceObjects) resources.Enqueue(resourceObject);

            if (resourceObjects.Length > 0) worldEnvironmentStates.ModifyWorldState("FreeResource", resourceObjects.Length);

            GameObject[] officeObjects = GameObject.FindGameObjectsWithTag("Office");
            foreach (GameObject officeObject in officeObjects) offices.Enqueue(officeObject);

            if (officeObjects.Length > 0) worldEnvironmentStates.ModifyWorldState("FreeOffice", officeObjects.Length);

            GameObject[] toiletObjects = GameObject.FindGameObjectsWithTag("Toilet");
            foreach (GameObject toiletObject in toiletObjects) toilets.Enqueue(toiletObject);

            if (toiletObjects.Length > 0) worldEnvironmentStates.ModifyWorldState("FreeToilet", toiletObjects.Length);

            Time.timeScale = 5;
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

        public void AddOffice(GameObject office)
        {
            offices.Enqueue(office);
        }

        public GameObject RemoveOffice()
        {
            if (offices.Count == 0) return null;
            return offices.Dequeue();
        }

        public void AddToilet(GameObject toilet)
        {
            toilets.Enqueue(toilet);
        }

        public GameObject RemoveToilet()
        {
            if (toilets.Count == 0) return null;
            return toilets.Dequeue();
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