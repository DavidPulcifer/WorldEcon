using UnityEngine;
using WorldEcon.World.Resources;

namespace WorldEcon.Actions
{
    public class Rest : AbstractAction
    {
        GameObject[] resourceObjects;
        Resource resource;

        public override bool PrePerform()
        {
            resourceObjects = GameObject.FindGameObjectsWithTag(targetTag);
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
            AssignedPerson.inventory.AddItem(target);
            return true;
        }

        public override bool PostPerform()
        {
            if (target == null) return false;
            target.GetComponent<Resource>().EndInteraction();
            AssignedPerson.inventory.RemoveItem(target);
            target = null;
            AssignedPerson.beliefs.RemoveState("exhausted");
            AssignedPerson.ResetRested();
            return true;
        }
    }
}
