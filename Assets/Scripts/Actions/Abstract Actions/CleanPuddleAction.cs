using UnityEngine;
using WorldEcon.World;

namespace WorldEcon.Actions
{
    public class CleanPuddleAction : AbstractAction
    {
        public override bool PrePerform()
        {
            target = WorldEnvironment.Instance.GetResourceQueue("puddles").RemoveResource();
            if (target == null) return false;
            inventory.AddItem(target);
            WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState("FreePuddle", -1);
            return true;
        }

        public override bool PostPerform()
        {
            inventory.RemoveItem(target);
            Destroy(target);
            return true;
        }
    }
}