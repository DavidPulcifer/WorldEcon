using WorldEcon.World.Resources;

namespace WorldEcon.Entities
{
    [System.Serializable]
    public class InventoryItem
    {
        public ResourceData resourceData;
        public int quantity;
    }
}