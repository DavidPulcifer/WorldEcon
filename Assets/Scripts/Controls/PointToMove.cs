using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace WorldEcon.Controls
{
    public class PointToMove : MonoBehaviour
    {
        NavMeshAgent agent;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {

        }

        void Update()
        {
            if (agent == null) return;
            Mouse mouse = Mouse.current;
            if (mouse.leftButton.wasPressedThisFrame)
            {
                Vector3 mousePosition = mouse.position.ReadValue();
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo))
                {
                    agent.SetDestination(hitInfo.point);
                }
            }
        }
    }
}
