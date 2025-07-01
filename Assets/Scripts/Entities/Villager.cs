using UnityEngine;

namespace WorldEcon.Entities
{
    public class Villager : Person
    {
        new void Awake()
        {
            base.Awake();            
            SubGoal s1 = new SubGoal("rested", 1, false);
            goals.Add(s1, 3);

            SubGoal s2 = new SubGoal("relief", 1, false);
            goals.Add(s2, 2);

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