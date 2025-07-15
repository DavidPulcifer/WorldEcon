using UnityEngine;

namespace WorldEcon.Entities
{
    public class Villager : Person
    {
        new void Awake()
        {
            base.Awake();
            SubGoal s1 = new SubGoal("rested", 1, false);
            goals.Add(s1, 2);

            SubGoal s2 = new SubGoal("relief", 1, false);
            goals.Add(s2, 4);

            SubGoal s3 = new SubGoal("fed", 1, false);
            goals.Add(s3, 3);

            SubGoal s4 = new SubGoal("secure", 1, false);
            goals.Add(s4, 3);

            Invoke("GetTired", Random.Range(40, 60));
            Invoke("NeedRelief", Random.Range(20, 30));
            Invoke("GetHungry", Random.Range(30, 40));
        }

        void GetTired()
        {
            beliefs.ModifyWorldState("exhausted", 1);
            Invoke("GetTired", Random.Range(40, 60));
        }

        void NeedRelief()
        {
            beliefs.ModifyWorldState("busting", 1);
            Invoke("NeedRelief", Random.Range(20, 30));
        }
        
        void GetHungry()
        {
            beliefs.ModifyWorldState("hungry", 1);
            Invoke("GetHungry", Random.Range(30, 40));
        }
    }
}