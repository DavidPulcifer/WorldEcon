using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using WorldEcon.Actions;
using WorldEcon.Planning;
using WorldEcon.World;

namespace WorldEcon.Entities
{
    //GAgent
    public class Person : MonoBehaviour
    {
        [SerializeField] int getTiredMin = 500;
        [SerializeField] int getTiredMax = 600;
        [SerializeField] int needReliefMin = 200;
        [SerializeField] int needReliefMax = 300;
        [SerializeField] int needFoodMin = 150;
        [SerializeField] int needFoodMax = 200;
        [SerializeField] int searchForFoodTime = 60;

        public List<AbstractAction> actions = new List<AbstractAction>();
        public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
        public Inventory inventory = new Inventory();
        public WorldStates beliefs = new WorldStates();
        float livingWell = 0;

        EntityPlanner planner;
        Queue<AbstractAction> actionQueue;
        public AbstractAction currentAction;
        SubGoal currentGoal;
        bool invoked = false;
        Vector3 destination = Vector3.zero;
        Vector3 exit = Vector3.zero;

        SubGoal restedGoal = new SubGoal("rested", 1, false);
        SubGoal reliefGoal = new SubGoal("relief", 1, false);
        SubGoal fedGoal = new SubGoal("fed", 1, false);
        SubGoal secureGoal = new SubGoal("secure", 1, false);
        SubGoal testGoal = new SubGoal("test", 1, false);

        public void Awake()
        {
            AbstractAction[] assignedActions = GetComponents<AbstractAction>();
            foreach (AbstractAction action in assignedActions)
            {
                actions.Add(action);
            }

            goals.Add(restedGoal, 1);
            goals.Add(reliefGoal, 1);
            goals.Add(fedGoal, 1);
            goals.Add(secureGoal, 3);
            // goals.Add(testGoal, 5);

            Invoke("GetTired", Random.Range(getTiredMin, getTiredMax));
            Invoke("NeedRelief", Random.Range(needReliefMin, needReliefMax));
            Invoke("GetHungry", Random.Range(needFoodMin, needFoodMax));
            Invoke("SearchForFood", searchForFoodTime);
        }

        void SearchForFood()
        {            
            if (!beliefs.HasWorldState("hungry"))
            {
                Invoke("SearchForFood", searchForFoodTime);
                return;
            }

            if (GameObject.FindGameObjectWithTag("Food") != null)
            {
                beliefs.SetWorldState("seeFood", 1);
            }
            else if (beliefs.HasWorldState("seeFood"))
            {
                beliefs.RemoveState("seeFood");
            }
            Invoke("SearchForFood", searchForFoodTime);
        }

        void GetTired()
        {
            beliefs.ModifyWorldState("exhausted", 1);
            livingWell -= beliefs.GetStateValue("exhausted");
            goals[restedGoal] += 1;
            Invoke("GetTired", Random.Range(getTiredMin, getTiredMax));
        }

        public void ResetRested()
        {
            goals[restedGoal] = 0;
        }

        void NeedRelief()
        {
            beliefs.ModifyWorldState("busting", 1);
            livingWell -= beliefs.GetStateValue("busting");
            goals[reliefGoal] += 1;
            Invoke("NeedRelief", Random.Range(needReliefMin, needReliefMax));
        }

        public void ResetRelief()
        {
            goals[reliefGoal] = 0;
        }

        void GetHungry()
        {
            beliefs.ModifyWorldState("hungry", 1);
            livingWell -= beliefs.GetStateValue("hungry");
            goals[fedGoal] += 1;
            Invoke("GetHungry", Random.Range(needFoodMin, needFoodMax));
        }

        public void ResetHungry()
        {
            goals[fedGoal] = 0;
        }

        void CompleteAction()
        {
            livingWell += currentAction.GetRawLivingWellReward();
            currentAction.running = false;            
            currentAction.PostPerform();
            currentAction.agent.SetDestination(exit);
            invoked = false;
        }

        void LateUpdate()
        {            
            //If Person is in the middle of a running action, let it finish
            if (currentAction != null && currentAction.running)
            {
                float distanceToTarget = Vector3.Distance(destination, transform.position);
                if (distanceToTarget < 4f)
                {
                    if (!invoked)
                    {
                        Invoke("CompleteAction", currentAction.duration);
                        invoked = true;
                    }
                }
                return;
            }

            //Initialize the Planner and Action Queue if either don't exist.
            if (planner == null || actionQueue == null)
            {
                planner = new EntityPlanner();
                var sortedGoals = from goal in goals orderby goal.Value descending select goal;
                foreach (KeyValuePair<SubGoal, int> goal in sortedGoals)
                {
                    actionQueue = planner.Plan(actions, goal.Key.subGoal, beliefs);
                    if (actionQueue != null)
                    {
                        currentGoal = goal.Key;
                        break;
                    }
                }
            }

            //The action queue exists, but there are no actions in it.
            if (actionQueue != null && actionQueue.Count == 0)
            {
                if (currentGoal.Remove)
                {
                    goals.Remove(currentGoal);
                }
                planner = null;
            }

            //The action queue exists and there are actions in it.
            if (actionQueue != null && actionQueue.Count > 0)
            {
                currentAction = actionQueue.Dequeue();
                if (currentAction.PrePerform())
                {
                    if (currentAction.target == null && currentAction.targetTag != "") currentAction.target = GameObject.FindWithTag(currentAction.targetTag);

                    if (currentAction.target != null && inventory.MeetsInventoryRequirements(currentAction.inventoryRequired))
                    {
                        currentAction.running = true;

                        destination = currentAction.target.transform.position;
                        Vector3 randomOffset = new Vector3(0, 0, Random.Range(-8, 8));
                        exit = destination + new Vector3(4, 0, 0) + randomOffset;
                        Transform destinationObject = currentAction.target.transform.Find("Destination");
                        if (destinationObject != null) destination = destinationObject.position + randomOffset;
                        Transform exitObject = currentAction.target.transform.Find("Exit");
                        if (exitObject != null) exit = exitObject.position + randomOffset;

                        currentAction.agent.SetDestination(destination);
                    }
                    else if (inventory.MeetsInventoryRequirements(currentAction.inventoryRequired))
                    {
                        destination = transform.position;
                        currentAction.running = true;
                    }
                }
                else
                {
                    actionQueue = null;
                }
            }
        }
    }
}