using UnityEngine;

namespace WorldEcon.Actions
{
    public class Rest : AbstractAction
    {
        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {            
            AssignedPerson.beliefs.RemoveWorldState("exhausted");
            return true;
        }
    }
}
