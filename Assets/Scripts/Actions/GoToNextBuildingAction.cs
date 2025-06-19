using WorldEcon.World;

namespace WorldEcon.Actions
{
    public class GoToNextBuildingAction : AbstractAction
    {
        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState("CitizenWaiting", 1);
            WorldEnvironment.Instance.AddCitizen(gameObject);
            beliefs.ModifyWorldState("atFinalBuilding", 1);
            return true;
        }        
    }
}
