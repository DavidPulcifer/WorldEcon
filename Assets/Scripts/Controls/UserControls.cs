using UnityEngine;
using UnityEngine.EventSystems;
using Unity.AI.Navigation;
using WorldEcon.World;
using WorldEcon.World.Resources;

namespace WorldEcon.Controls
{
    //WInterface
    public class UserControls : MonoBehaviour
    {
        [SerializeField] NavMeshSurface surface;
        [SerializeField] GameObject[] resourcePrefabs;
        GameObject newResourcePrefab;
        GameObject focusObj;
        ResourceData focusObjectData;
        Vector3 goalPos;
        Vector3 clickOffset = Vector3.zero;
        bool offsetCalc = false;
        bool deleteResource = false;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out hit)) return;

                offsetCalc = false;
                clickOffset = Vector3.zero;

                Resource resource = hit.transform.gameObject.GetComponent<Resource>();

                if (resource != null)
                {
                    focusObj = hit.transform.gameObject;
                    focusObjectData = resource.ResourceData;
                }
                else if (newResourcePrefab != null)
                {
                    goalPos = hit.point;
                    focusObj = Instantiate(newResourcePrefab, goalPos, newResourcePrefab.transform.rotation);
                    focusObjectData = focusObj.GetComponent<Resource>().ResourceData;
                }

                if (focusObj) focusObj.GetComponent<Collider>().enabled = false;

            }
            else if (IsMouseReleased())
            {
                if (deleteResource)
                {
                    WorldEnvironment.Instance.GetResourceQueue(focusObjectData.resourceQueue).RemoveResource(focusObj);
                    WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState(focusObjectData.resourceState, -1);
                    Destroy(focusObj);
                }
                else
                {
                    WorldEnvironment.Instance.GetResourceQueue(focusObjectData.resourceQueue).AddResource(focusObj);
                    WorldEnvironment.Instance.GetWorldEnvironment().ModifyWorldState(focusObjectData.resourceState, 1);
                    focusObj.GetComponent<Collider>().enabled = true;
                }

                surface.BuildNavMesh();
                focusObj = null;
            }
            else if (IsDraggingMouse())
            {
                int layerMask = 1 << 6;
                RaycastHit hitMove;
                Ray rayMove = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(rayMove, out hitMove, Mathf.Infinity, layerMask)) return;

                if (!offsetCalc)
                {
                    clickOffset = hitMove.point - focusObj.transform.position;
                    offsetCalc = true;
                }

                goalPos = hitMove.point - clickOffset;
                focusObj.transform.position = goalPos;
            }

            if (focusObj && Input.GetKeyDown(KeyCode.Comma)) focusObj.transform.Rotate(0, 90, 0);
            else if (focusObj && Input.GetKeyDown(KeyCode.Period)) focusObj.transform.Rotate(0, -90, 0);
        }

        private bool IsMouseReleased()
        {
            return focusObj && Input.GetMouseButtonUp(0);
        }

        private bool IsDraggingMouse()
        {
            return focusObj && Input.GetMouseButton(0);
        }

        public void MouseOnHoverTrash()
        {
            deleteResource = true;
        }

        public void MouseOutHoverTrash()
        {
            deleteResource = false;
        }

        public void ActivateToilet()
        {
            newResourcePrefab = resourcePrefabs[0];
        }

        public void ActivateResource(GameObject resourcePrefab)
        {
            newResourcePrefab = resourcePrefab;
        }
    }
}