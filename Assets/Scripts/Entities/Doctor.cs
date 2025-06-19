using UnityEngine;

namespace WorldEcon.Entities
{
    public class Doctor : Person
    {
        new void Start()
        {
            base.Start();
            SubGoal s1 = new SubGoal("treatCitizen", 1, true);
            goals.Add(s1, 3);
        }
    }
}