using UnityEngine;
using WorldEcon.World;

namespace WorldEcon.Actions
{
    public class GoToToiletAction : AbstractAction
    {
        public override bool PrePerform()
        {
            target = WorldEnvironment.Instance.GetResourceQueue("toilets").RemoveResource();
            if (target == null) return false;
            inventory.AddItem(target);
            WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState("FreeToilet", -1);
            return true;
        }

        public override bool PostPerform()
        {
            WorldEnvironment.Instance.GetResourceQueue("toilets").AddResource(target);
            inventory.RemoveItem(target);
            WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState("FreeToilet", 1);
            beliefs.RemoveWorldState("busting");
            return true;
        }
    }
}