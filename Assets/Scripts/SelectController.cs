using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.AI;

public class SelectController : MonoBehaviour
{
    public GameObject cube;
    public LayerMask layer, layerMask;
    private Camera _cam;
    private GameObject _cubeSelection;
    private RaycastHit _hit;

    public List<GameObject> players;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        Mouse mouse = Mouse.current;

        if (mouse.rightButton.wasPressedThisFrame && players.Count > 0)
        {
            Ray ray = _cam.ScreenPointToRay(mouse.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit agentTarget, 1000f, layer))
            {
                foreach (var element in players)
                    element.GetComponent<NavMeshAgent>().SetDestination(agentTarget.point);
            }
        }


        if (mouse.leftButton.wasPressedThisFrame)
        {
            foreach (var element in players)
                element.transform.GetChild(0).gameObject.SetActive(false);
            players.Clear();

            Ray ray = _cam.ScreenPointToRay(mouse.position.ReadValue());

            if (Physics.Raycast(ray, out _hit, 1000f, layer))
                _cubeSelection = Instantiate(cube, new Vector3(_hit.point.x, 1, _hit.point.z), Quaternion.identity);
        }

        if (_cubeSelection)
        {
            Ray ray = _cam.ScreenPointToRay(mouse.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer))
            {
                float xScale = (_hit.point.x - hitDrag.point.x) * -1;
                float zScale = _hit.point.z - hitDrag.point.z;

                if (xScale < 0.0f && zScale < 0.0f)
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0,180,0));
                else if (xScale < 0.0f)
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                else if (zScale < 0.0f)
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
                else
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                _cubeSelection.transform.localScale = new Vector3(Mathf.Abs(xScale), 1, Mathf.Abs(zScale));
            }
        }

        if (mouse.leftButton.wasReleasedThisFrame && _cubeSelection)
        {
            RaycastHit[] hits = Physics.BoxCastAll(
                _cubeSelection.transform.position,
                _cubeSelection.transform.localScale,
                Vector3.up,
                Quaternion.identity,
                0,
                layerMask);

            foreach (var element in hits)
            {
                players.Add(element.transform.gameObject);
                element.transform.GetChild(0).gameObject.SetActive(true);
            }

            Destroy(_cubeSelection);
        }

    }
}
