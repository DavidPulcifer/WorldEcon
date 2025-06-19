using UnityEngine;
using WorldEcon.World;

namespace WorldEcon.Actions
{
    public class ResearchAction : AbstractAction
    {
        public override bool PrePerform()
        {
            target = WorldEnvironment.Instance.GetResourceQueue("offices").RemoveResource();
            if (target == null) return false;
            inventory.AddItem(target);
            WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState("FreeOffice", -1);
            return true;
        }

        public override bool PostPerform()
        {
            WorldEnvironment.Instance.GetResourceQueue("offices").AddResource(target);
            inventory.RemoveItem(target);
            WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState("FreeOffice", 1);
            return true;
        }
    }
}