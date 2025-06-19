using WorldEcon.World;

namespace WorldEcon.Actions
{
    public class GoToResourceAction : AbstractAction
    {
        public override bool PrePerform()
        {
            target = inventory.FindItemWithTag("Resource");
            if (target == null) return false;
            return true;
        }

        public override bool PostPerform()
        {
            WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState("HandlingCitizen", 1);
            WorldEnvironment.Instance.AddResource(target);
            inventory.RemoveItem(target);
            WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState("FreeResource", 1);
            return true;
        }
    }
}