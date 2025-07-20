using System.Collections.Generic;
using WorldEcon.Actions;

public class Node
{
    public Node parent;
    public float cost;
    public float livingWellReward;
    public Dictionary<string, int> states;
    public AbstractAction action;

    public Node(Node parent, float cost, float livingWellReward, Dictionary<string, int> worldStates, AbstractAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.livingWellReward = livingWellReward;
        this.states = new Dictionary<string, int>(worldStates);
        this.action = action;
    }

    public Node(Node parent, float cost, float livingWellReward, Dictionary<string, int> worldStates, Dictionary<string, int> beliefStates, AbstractAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.livingWellReward = livingWellReward;
        states = new Dictionary<string, int>(worldStates);
        foreach (KeyValuePair<string, int> belief in beliefStates)
        {
            if (!states.ContainsKey(belief.Key)) states.Add(belief.Key, belief.Value);
        }
        this.action = action;
    }
}