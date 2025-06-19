using UnityEngine;

namespace WorldEcon.Actions
{
    public class GoHomeAction : AbstractAction
    {
        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            Destroy(gameObject);
            return true;
        }
    }
}
