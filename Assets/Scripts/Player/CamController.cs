using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public bool canMoveMouse = true;
    [Min(0)] public float sensivity;
    [SerializeField] private Transform playerBody;
    //public FixedTouchField fixedTouchField;
    public Vector2 LockAxis;

    private float xRotation = 0f;
    private float yRotation = 0.0f;
    public float xRecoil; // Отдача по x
    public float yRecoil; // Отдача по y

    private float mouseX;
    private float mouseY;



    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    void Update()
    {
        if (canMoveMouse)
        {
            mouseX = Input.GetAxis("Mouse X") * sensivity + xRecoil;
            mouseY = Input.GetAxis("Mouse Y") * sensivity + yRecoil;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
