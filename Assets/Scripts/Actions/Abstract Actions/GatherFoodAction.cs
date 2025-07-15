using UnityEngine;
using WorldEcon.World.Resources;

namespace WorldEcon.Actions
{
    public class GatherFoodAction : AbstractAction
    {
        GameObject[] resourceObjects;
        Resource resource;

        public override bool PrePerform()
        {
            resourceObjects = GameObject.FindGameObjectsWithTag(resourceData.resourceTag);
            if (resourceObjects.Length == 0 || resourceObjects == null) return false;
            foreach (GameObject resourceObject in resourceObjects)
            {
                resource = resourceObject.GetComponent<Resource>();
                if (resource == null) continue;
                if (resource.Interact())
                {
                    target = resourceObject;
                    break;
                }
            }
            if (target == null) return false;            
            return true;
        }

        public override bool PostPerform()
        {
            if (target == null) return false;
            AssignedPerson.inventory.AddItem(target.GetComponent<Resource>().ResourceData, 1);
            AssignedPerson.beliefs.ModifyWorldState("hasFood", 1);
            target.GetComponent<Resource>().EndInteraction();            
            return true;
        }        
    }
}