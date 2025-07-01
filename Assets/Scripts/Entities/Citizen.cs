using UnityEngine;

namespace WorldEcon.Entities
{
    public class Citizen : Person
    {
        new void Awake()
        {
            base.Awake();
            SubGoal s1 = new SubGoal("isWaiting", 1, true);
            goals.Add(s1, 3);
            SubGoal s2 = new SubGoal("isHandled", 1, true);
            goals.Add(s2, 5);
            SubGoal s3 = new SubGoal("isHome", 1, true);
            goals.Add(s3, 5);            
        }        
    }
}