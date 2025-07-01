using WorldEcon.World;

namespace WorldEcon.Actions
{
    public class HandleCitizenAction : AbstractAction
    {
        public override bool PrePerform()
        {
            target = inventory.FindItemWithTag("Resource");
            if (target == null) return false;
            return true;
        }

        public override bool PostPerform()
        {
            WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState("Handled", 1);
            beliefs.ModifyWorldState("isHandled", 1);
            inventory.RemoveItem(target);
            return true;
        }
    }
}