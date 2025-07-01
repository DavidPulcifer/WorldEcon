using UnityEngine;

namespace WorldEcon.Entities
{
    public class Janitor : Person
    {
        new void Awake()
        {
            base.Awake();
            SubGoal s1 = new SubGoal("cleanPuddle", 1, false);
            goals.Add(s1, 1);

            SubGoal s2 = new SubGoal("rested", 1, false);
            goals.Add(s2, 3);

            SubGoal s3 = new SubGoal("relief", 1, false);
            goals.Add(s3, 2);

            Invoke("GetTired", Random.Range(10, 20));
            Invoke("NeedRelief", Random.Range(10, 20));
        }

        void GetTired()
        {
            beliefs.ModifyWorldState("exhausted", 1);
            Invoke("GetTired", Random.Range(10, 20));
        }

        void NeedRelief()
        {
            beliefs.ModifyWorldState("busting", 1);
            Invoke("NeedRelief", Random.Range(10, 20));
        }
    }
}