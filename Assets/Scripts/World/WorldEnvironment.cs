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
        static Dictionary<string, ResourceQueue> allResources = new Dictionary<string, ResourceQueue>();

        static WorldEnvironment()
        {
            worldEnvironmentStates = new WorldStates();
            Time.timeScale = 2;
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