using System.Collections.Generic;
using UnityEngine;
using WorldEcon.Actions;
using WorldEcon.World;

namespace WorldEcon.Planning
{
    public class EntityPlanner
    {
        public Queue<AbstractAction> Plan(List<AbstractAction> actions, Dictionary<string, int> goals, WorldStates beliefStates)
        {
            List<AbstractAction> usableActions = new List<AbstractAction>();
            foreach (AbstractAction action in actions)
            {
                if (action.CanDoAction()) usableActions.Add(action);
            }

            List<Node> leaves = new List<Node>();
            Node start = new Node(null, 0, WorldEnvironment.Instance.GetWorldEnvironment().GetWorldStates(), beliefStates.GetWorldStates(), null);

            bool success = BuildGraph(start, leaves, usableActions, goals);

            if (!success)
            {                
                return null;
            }

            Node cheapest = null;
            foreach (Node leaf in leaves)
            {
                if (cheapest == null) cheapest = leaf;
                else if (leaf.cost < cheapest.cost) cheapest = leaf;
            }

            List<AbstractAction> results = new List<AbstractAction>();
            Node n = cheapest;
            while (n != null)
            {
                if (n.action != null) results.Insert(0, n.action);
                n = n.parent;
            }

            Queue<AbstractAction> actionQueue = new Queue<AbstractAction>();
            foreach (AbstractAction action in results)
            {
                actionQueue.Enqueue(action);
            }
            Debug.Log("The plan is: ");
            foreach (AbstractAction loggedAction in actionQueue)
            {
                Debug.Log("Q: " + loggedAction.actionName);
            }

            return actionQueue;
        }

        bool BuildGraph(Node parent, List<Node> leaves, List<AbstractAction> usableActions, Dictionary<string, int> goals)
        {
            bool foundPath = false;

            foreach (AbstractAction action in usableActions)
            {
                if (action.CanDoAction(parent.states))
                {
                    Dictionary<string, int> currentState = new Dictionary<string, int>(parent.states);
                    foreach (KeyValuePair<string, int> effect in action.effects)
                    {
                        if (!currentState.ContainsKey(effect.Key)) currentState.Add(effect.Key, effect.Value);
                    }

                    Node node = new Node(parent, parent.cost + action.cost, currentState, action);

                    if (GoalAchieved(goals, currentState))
                    {
                        leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        List<AbstractAction> subset = ActionSubset(usableActions, action);
                        bool found = BuildGraph(node, leaves, subset, goals);
                        if (found) foundPath = true;
                    }
                }
            }

            return foundPath;
        }

        bool GoalAchieved(Dictionary<string, int> goals, Dictionary<string, int> states)
        {
            foreach (KeyValuePair<string, int> goal in goals)
            {
                if (!states.ContainsKey(goal.Key)) return false;
            }
            return true;
        }

        List<AbstractAction> ActionSubset(List<AbstractAction> actions, AbstractAction removeMe)
        {
            List<AbstractAction> subset = new List<AbstractAction>();
            foreach (AbstractAction action in actions)
            {
                if (!action.Equals(removeMe)) subset.Add(action);
            }
            return subset;
        }
    }
}