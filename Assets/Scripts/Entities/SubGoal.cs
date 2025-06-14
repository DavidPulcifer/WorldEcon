using System.Collections.Generic;

namespace WorldEcon.Entities
{
    public class SubGoal
    {
        public Dictionary<string, int> subGoals;
        public bool Remove;

        public SubGoal(string key, int value, bool remove)
        {
            subGoals = new Dictionary<string, int>();
            subGoals.Add(key, value);
            Remove = remove;
        }
    }
}
