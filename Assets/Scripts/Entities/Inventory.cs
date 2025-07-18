using System.Collections.Generic;
using UnityEngine;
using WorldEcon.World.Resources;

namespace WorldEcon.Entities
{
    public class Inventory
    {
        List<GameObject> externalInventoryItems = new List<GameObject>();
        Dictionary<ResourceData, int> heldInventoryItems = new Dictionary<ResourceData, int>();

        public List<GameObject> GetExternalInventoryObjects()
        {
            return externalInventoryItems;
        }

        public Dictionary<ResourceData, int> GetHeldInventory()
        {
            return heldInventoryItems;
        }

        public bool IsInInventory(GameObject gameObject)
        {
            return externalInventoryItems.Contains(gameObject);
        }

        public bool IsInInventory(ResourceData resourceData)
        {
            return heldInventoryItems.ContainsKey(resourceData);
        }

        public bool MeetsInventoryRequirements(InventoryItem[] inventoryItems)
        {
            if (inventoryItems == null) return true;
            
            foreach (InventoryItem inventoryItem in inventoryItems)
            {
                if (!heldInventoryItems.ContainsKey(inventoryItem.resourceData)) return false;

                if (heldInventoryItems[inventoryItem.resourceData] < inventoryItem.quantity) return false;
            }
            return true;
        }

        public void AddItem(GameObject item)
        {
            externalInventoryItems.Add(item);
        }

        public void AddItem(ResourceData resourceData, int number = 1)
        {
            if (number <= 0)
            {
                Debug.Log($"Attempting to add negative value of {resourceData}");
                return;
            }
            if (heldInventoryItems.ContainsKey(resourceData)) heldInventoryItems[resourceData] += number;
            else heldInventoryItems[resourceData] = number;
        }

        public GameObject FindItemWithTag(string tag)
        {
            foreach (GameObject item in externalInventoryItems)
            {
                if (item == null) break;
                if (item.tag == tag) return item;
            }
            return null;
        }

        public void RemoveItem(GameObject itemToRemove)
        {
            int indexToRemove = -1;
            foreach (GameObject item in externalInventoryItems)
            {
                indexToRemove++;
                if (item == itemToRemove) break;
            }

            if (indexToRemove >= -1) externalInventoryItems.RemoveAt(indexToRemove);
        }

        public void RemoveItem(ResourceData resourceData, int number = 1)
        {
            if (number <= 0)
            {
                Debug.Log($"Attempting to remove negative value of {resourceData}");
                return;
            }

            if (!heldInventoryItems.ContainsKey(resourceData))
            {
                Debug.Log($"Cannot remove {resourceData}, not in inventory");
                return;
            }

            if (heldInventoryItems.ContainsKey(resourceData) && heldInventoryItems[resourceData] <= 0)
            {
                Debug.Log($"Cannot remove {resourceData}, not in inventory");
                heldInventoryItems.Remove(resourceData);
                return;
            }

            heldInventoryItems[resourceData] -= 1;

            if (heldInventoryItems[resourceData] <= 0) heldInventoryItems.Remove(resourceData);
        }
    }
}