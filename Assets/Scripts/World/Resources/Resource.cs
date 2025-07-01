using UnityEngine;

namespace WorldEcon.World.Resources
{
    public class Resource : MonoBehaviour
    {
        [field: SerializeField] public ResourceData ResourceData { get; private set; }

        int currentInteractions = 0;

        public bool Interact()
        {
            if (currentInteractions >= ResourceData.maxInteractions) return false;

            currentInteractions += 1;

            return true;
        }

        public void EndInteraction()
        {
            if (currentInteractions <= 0) return;

            currentInteractions -= 1;
        }
    }
}


