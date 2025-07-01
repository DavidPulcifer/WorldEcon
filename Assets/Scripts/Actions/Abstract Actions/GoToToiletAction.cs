using UnityEngine;
using WorldEcon.Entities;
using WorldEcon.World;

namespace WorldEcon.Actions
{
    public class GoToToiletAction : AbstractAction
    {
        public override bool PrePerform()
        {
            target = GameObject.FindGameObjectWithTag("Toilet");
            if (target == null) return false;
            AssignedPerson.inventory.AddItem(target);
            return true;
        }

        public override bool PostPerform()
        {
            WorldEnvironment.Instance.GetResourceQueue("toilets").AddResource(target);
            AssignedPerson.inventory.RemoveItem(target);
            WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState("FreeToilet", 1);
            AssignedPerson.beliefs.RemoveWorldState("busting");
            return true;
        }
    }
}