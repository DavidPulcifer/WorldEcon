using System.Collections.Generic;
using UnityEngine;
using WorldEcon.Actions;
using WorldEcon.World;

namespace WorldEcon.Planning
{
    public class EntityPlanner
    {
        public Queue<AbstractAction> Plan(List<AbstractAction> actions, Dictionary<string, int> goal, WorldStates beliefStates)
        {
            List<AbstractAction> availableActions = new List<AbstractAction>();
            foreach (AbstractAction action in actions)
            {
                if (action.CanDoAction()) availableActions.Add(action);
            }

            List<Node> leaves = new List<Node>();
            Node start = new Node(null, 0, WorldEnvironment.Instance.GetWorldEnvironment().GetStates(), beliefStates.GetStates(), null);

            bool success = BuildGraph(start, leaves, availableActions, goal);

            if (!success) return null;

            Node cheapest = null;
            foreach (Node leaf in leaves)
            {
                if (cheapest == null) cheapest = leaf;
                else if (leaf.cost < cheapest.cost) cheapest = leaf;
            }

            List<AbstractAction> actionSequence = new List<AbstractAction>();
            Node n = cheapest;
            while (n != null)
            {
                if (n.action != null) actionSequence.Insert(0, n.action);
                n = n.parent;
            }

            Queue<AbstractAction> actionQueue = new Queue<AbstractAction>();
            foreach (AbstractAction action in actionSequence)
            {
                actionQueue.Enqueue(action);
            }
            // Debug.Log("The plan is: ");
            // foreach (AbstractAction loggedAction in actionQueue)
            // {
            //     Debug.Log("Q: " + loggedAction.actionName);
            // }

            return actionQueue;
        }

        bool BuildGraph(Node parent, List<Node> leaves, List<AbstractAction> availableActions, Dictionary<string, int> goal)
        {
            bool foundPath = false;

            foreach (AbstractAction action in availableActions)
            {
                if (action.CanDoAction(parent.states))
                {
                    Dictionary<string, int> currentStates = new Dictionary<string, int>(parent.states);
                    foreach (KeyValuePair<string, int> effect in action.effects)
                    {
                        if (!currentStates.ContainsKey(effect.Key)) currentStates.Add(effect.Key, effect.Value);
                    }

                    Node node = new Node(parent, parent.cost + action.cost, currentStates, action);

                    if (GoalAchieved(goal, currentStates))
                    {
                        leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        List<AbstractAction> subset = ActionSubset(availableActions, action);
                        bool found = BuildGraph(node, leaves, subset, goal);
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