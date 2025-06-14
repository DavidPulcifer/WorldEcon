using UnityEngine;
using WorldEcon.World;

namespace WorldEcon.Actions
{
    public class GetCitizenAction : AbstractAction
    {
        public override bool PrePerform()
        {
            target = WorldEnvironment.Instance.RemoveCitizen();            
            if (target == null) return false;
            return true;
        }

        public override bool PostPerform()
        {
            return true;
        }
    }
}