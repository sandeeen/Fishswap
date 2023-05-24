using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    private GameObject fish;


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
        fish = GameObject.Find("fish");
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

        UpdateFishPos();


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

    private void UpdateFishPos()
    {
        fish.transform.position = Vector3.MoveTowards(fish.transform.position, targetPos, moveSpeed * Time.deltaTime);
        fish.transform.position = new Vector3(fish.transform.position.x, 2.5f, fish.transform.position.z);
        fish.transform.LookAt(targetPos);
        fish.transform.eulerAngles = new Vector3(0, fish.transform.eulerAngles.y, 90);
    }

}
