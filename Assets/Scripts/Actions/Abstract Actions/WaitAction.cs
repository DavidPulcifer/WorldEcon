using UnityEngine;

namespace WorldEcon.Actions
{
    public class WaitAction : AbstractAction
    {
        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {            
            return true;
        }

        public override float GetCost()
        {
            return cost;
        }

        public override float GetLivingWellReward()
        {
            return livingWellReward;
        }
    }
}
