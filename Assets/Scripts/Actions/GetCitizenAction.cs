using UnityEngine;
using WorldEcon.Entities;
using WorldEcon.World;

namespace WorldEcon.Actions
{
    public class GetCitizenAction : AbstractAction
    {
        GameObject resource;
        public override bool PrePerform()
        {
            target = WorldEnvironment.Instance.GetResourceQueue("citizens").RemoveResource();
            if (target == null) return false;
            resource = WorldEnvironment.Instance.GetResourceQueue("resources").RemoveResource();
            if (resource != null) inventory.AddItem(resource);
            else
            {
                WorldEnvironment.Instance.GetResourceQueue("citizens").AddResource(target);
                target = null;
                return false;
            }
            WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState("FreeResource", -1);
            return true;
        }

        public override bool PostPerform()
        {
            WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState("CitizenWaiting", -1);
            if (target) target.GetComponent<Person>().inventory.AddItem(resource);
            return true;
        }
    }
}