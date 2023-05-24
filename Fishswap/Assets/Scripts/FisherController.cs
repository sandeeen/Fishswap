using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisherController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float distance = 100f;
    private GameObject fisher;
    GameObject bobber;


    private Camera mainCamera;
    private Vector3 targetPos;

    float mouseX;
    float mouseY;

    public bool frozen;

    private void Awake()
    {

    }

    void Start()
    {
        mainCamera = Camera.main;
        fisher = GameObject.Find("OLDMAN");
        bobber = GameObject.Find("bobber");
    }

    void Update()
    {
        if (frozen) { return; }
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (mouseX != 0f || mouseY != 0f)
        {
            RaycastOnMousePos();

        }

        UpdateFisherPos();

    }

    private void RaycastOnMousePos()
    {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            targetPos = hit.point;

        }
        else
        {

        }
    }

    private void UpdateFisherPos()
    {
        bobber.transform.position = Vector3.MoveTowards(bobber.transform.position, targetPos, moveSpeed * Time.deltaTime);
        bobber.transform.position = new Vector3(bobber.transform.position.x, 2.5f,bobber.transform.position.z);
        bobber.transform.LookAt(targetPos);
        bobber.transform.eulerAngles = new Vector3(0, bobber.transform.eulerAngles.y, 90);
        if (new Vector3(bobber.transform.position.x, 0, bobber.transform.position.z) == Vector3.zero)
        {
            return;
        }
        fisher.transform.position = new Vector3(bobber.transform.position.x,0,bobber.transform.position.z).normalized * distance;
        fisher.transform.LookAt(Vector3.zero);
        fisher.transform.eulerAngles = new Vector3(-90, fisher.transform.eulerAngles.y, 0);
    }

}
