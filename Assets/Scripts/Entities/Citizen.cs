using UnityEngine;

namespace WorldEcon.Entities
{
    public class Citizen : Person
    {
        new void Start()
        {
            base.Start();
            SubGoal s1 = new SubGoal("isWaiting", 1, true);
            goals.Add(s1, 3);
            SubGoal s2 = new SubGoal("isHandled", 1, true);
            goals.Add(s2, 5);
        }
    }
}