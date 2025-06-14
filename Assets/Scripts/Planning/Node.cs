using System.Collections.Generic;
using WorldEcon.Actions;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> states;
    public AbstractAction action;

    public Node(Node parent, float cost, Dictionary<string, int> states, AbstractAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.states = new Dictionary<string, int>(states);
        this.action = action;
    }
}