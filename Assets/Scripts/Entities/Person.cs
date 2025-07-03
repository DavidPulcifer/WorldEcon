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

        public void Awake()
        {
            AbstractAction[] assignedActions = GetComponents<AbstractAction>();
            foreach (AbstractAction action in assignedActions)
            {
                actions.Add(action);
            }
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
            //Person has an action and the action is in progress
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
                var sortedGoals = from entry in goals orderby entry.Value descending select entry;
                foreach (KeyValuePair<SubGoal, int> subGoal in sortedGoals)
                {
                    actionQueue = planner.Plan(actions, subGoal.Key.subGoals, beliefs);
                    if (actionQueue != null)
                    {
                        currentGoal = subGoal.Key;
                        break;
                    }
                }
            }

            //The action queue exists, but there are not actions in it.
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