using System.Collections.Generic;
using UnityEngine;

namespace WorldEcon.World
{
    public class WorldStates
    {
        public Dictionary<string, int> worldStates;

        public WorldStates()
        {
            worldStates = new Dictionary<string, int>();
        }

        public int GetStateValue(string key)
        {
            if (!HasWorldState(key)) return -1;

            return worldStates[key];
        }

        public bool HasWorldState(string key)
        {
            return worldStates.ContainsKey(key);
        }

        public void ModifyWorldState(string key, int increment)
        {
            if (!HasWorldState(key) && increment > 0) AddState(key, increment);
            else if (HasWorldState(key))
            {
                worldStates[key] += increment;
                if (worldStates[key] <= 0) RemoveState(key);
            }
        }

        public void RemoveState(string key)
        {
            if (HasWorldState(key)) worldStates.Remove(key);
            // if (!HasWorldState(key)) Debug.Log($"{key} not found in states.");
        }

        public void SetWorldState(string key, int value)
        {
            if (HasWorldState(key)) worldStates[key] = value;
            else AddState(key, value);
        }

        public Dictionary<string, int> GetStates()
        {
            return worldStates;
        }

        void AddState(string key, int value)
        {
            if (HasWorldState(key)) return;

            worldStates.Add(key, value);
        }
    }
}