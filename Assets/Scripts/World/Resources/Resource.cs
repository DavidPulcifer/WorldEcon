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
            if (ResourceData.prefabToSpawn != null)
            {
                int numToSpawn = ResourceData.minSpawn < ResourceData.maxSpawn ? ResourceData.minSpawn : Random.Range(ResourceData.minSpawn, ResourceData.maxSpawn);
                for (int i = 0; i < numToSpawn; i++)
                {
                    Vector3 randomOffset = new Vector3(Random.Range(-16, 16), 0, Random.Range(-16, 16));
                    Instantiate(ResourceData.prefabToSpawn, transform.position + randomOffset, Quaternion.identity);
                }
            }
            
            if (ResourceData.isGatherable)
            {
                Destroy(gameObject, 1f);
                return;
            }
            
            if (currentInteractions <= 0) return;

            currentInteractions -= 1;
        }
    }
}


