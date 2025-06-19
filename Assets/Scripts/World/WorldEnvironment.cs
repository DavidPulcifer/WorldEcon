using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldEcon.World.Resources;

namespace WorldEcon.World
{
    public sealed class WorldEnvironment
    {
        static readonly WorldEnvironment instance = new WorldEnvironment();
        static WorldStates worldEnvironmentStates;
        static ResourceQueue citizens;
        static ResourceQueue resources;
        static ResourceQueue offices;
        static ResourceQueue toilets;
        static Dictionary<string, ResourceQueue> allResources = new Dictionary<string, ResourceQueue>();

        static WorldEnvironment()
        {
            worldEnvironmentStates = new WorldStates();
            citizens = new ResourceQueue("", "", worldEnvironmentStates);
            allResources.Add("citizens", citizens);
            resources = new ResourceQueue("Resource", "FreeResource", worldEnvironmentStates);
            allResources.Add("resources", resources);
            offices = new ResourceQueue("Office", "FreeOffice", worldEnvironmentStates);
            allResources.Add("offices", offices);
            toilets = new ResourceQueue("Toilet", "FreeToilet", worldEnvironmentStates);
            allResources.Add("toilets", toilets);

            Time.timeScale = 5;
        }

        public ResourceQueue GetResourceQueue(string type)
        {
            return allResources[type];
        }

        private WorldEnvironment()
        {

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