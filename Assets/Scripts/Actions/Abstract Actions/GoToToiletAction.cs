using UnityEngine;
using WorldEcon.World;
using WorldEcon.World.Resources;

namespace WorldEcon.Actions
{
    public class GoToToiletAction : AbstractAction
    {        
        GameObject[] toilets;
        Resource toiletResource;

        public override bool PrePerform()
        {
            toilets = GameObject.FindGameObjectsWithTag("Toilet");
            if (toilets.Length == 0 || toilets == null) return false;
            foreach (GameObject toilet in toilets)
            {
                toiletResource = toilet.GetComponent<Resource>();
                if (toiletResource == null) continue;
                if (toiletResource.Interact())
                {
                    target = toilet;
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
            AssignedPerson.beliefs.RemoveWorldState("busting");
            return true;
        }
    }
}