using System.Collections.Generic;
using UnityEngine;
using WorldEcon.World.Resources;

namespace WorldEcon.Actions
{
    public class EatFoodAction : AbstractAction
    {
        ResourceData foodToEat;

        public override bool PrePerform()
        {
            foreach (KeyValuePair<ResourceData, int> heldInventoryItem in AssignedPerson.inventory.GetHeldInventory())
            {
                if (heldInventoryItem.Key.resourceTag == targetTag)
                {
                    foodToEat = heldInventoryItem.Key;
                    return true;
                }
            }
            return false;
        }

        public override bool PostPerform()
        {
            if (foodToEat == null) return false;
            AssignedPerson.inventory.RemoveItem(foodToEat, 1);            
            AssignedPerson.beliefs.RemoveState("hungry");
            if(!AssignedPerson.inventory.GetHeldInventory().ContainsKey(foodToEat)) AssignedPerson.beliefs.RemoveState("hasFood");
            AssignedPerson.ResetHungry();
            return true;
        }        
    }
}