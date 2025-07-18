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
        public List<AbstractAction> actions = new List<AbstractAction>();
        public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
        public Inventory inventory = new Inventory();
        public WorldStates beliefs = new WorldStates();

        EntityPlanner planner;
        Queue<AbstractAction> actionQueue;
        public AbstractAction currentAction;
        SubGoal currentGoal;
        bool invoked = false;
        Vector3 destination = Vector3.zero;
        Vector3 exit = Vector3.zero;

        SubGoal s1 = new SubGoal("rested", 1, false);
        SubGoal s2 = new SubGoal("relief", 1, false);
        SubGoal s3 = new SubGoal("fed", 1, false);

        public void Awake()
        {
            AbstractAction[] assignedActions = GetComponents<AbstractAction>();
            foreach (AbstractAction action in assignedActions)
            {
                actions.Add(action);
            }

            goals.Add(s1, 1);
            goals.Add(s2, 1);
            goals.Add(s3, 1);

            SubGoal s4 = new SubGoal("secure", 1, false);
            goals.Add(s4, 3);

            Invoke("GetTired", Random.Range(40, 60));
            Invoke("NeedRelief", Random.Range(20, 30));
            Invoke("GetHungry", Random.Range(30, 40));
        }

        void GetTired()
        {
            beliefs.ModifyWorldState("exhausted", 1);
            goals[s1] += 1;
            Invoke("GetTired", Random.Range(40, 60));
        }

        public void ResetRested()
        {
            goals[s1] = 0;
        }

        void NeedRelief()
        {
            beliefs.ModifyWorldState("busting", 1);
            goals[s2] += 1;
            Invoke("NeedRelief", Random.Range(20, 30));
        }

        public void ResetRelief()
        {
            goals[s2] = 0;
        }

        void GetHungry()
        {
            beliefs.ModifyWorldState("hungry", 1);
            goals[s3] += 1;
            Invoke("GetHungry", Random.Range(30, 40));
        }

        public void ResetHungry()
        {
            goals[s3] = 0;
        }

        void CompleteAction()
        {
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

                    //TODO: Fix this section so it can detect when the goal needs a held inventory item instead of a gameobject.
                    if (currentAction.target != null)
                    {
                        currentAction.running = true;

                        destination = currentAction.target.transform.position;
                        exit = destination + new Vector3(4, 0, 0);
                        Transform destinationObject = currentAction.target.transform.Find("Destination");
                        if (destinationObject != null) destination = destinationObject.position;
                        Transform exitObject = currentAction.target.transform.Find("Exit");
                        if (exitObject != null) exit = exitObject.position;

                        currentAction.agent.SetDestination(destination);
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