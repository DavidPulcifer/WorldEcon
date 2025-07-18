using System.Collections.Generic;

namespace WorldEcon.Entities
{
    public class SubGoal
    {
        public Dictionary<string, int> subGoal;
        public bool Remove;

        public SubGoal(string key, int value, bool remove)
        {
            subGoal = new Dictionary<string, int>();
            subGoal.Add(key, value);
            Remove = remove;
        }
    }
}
