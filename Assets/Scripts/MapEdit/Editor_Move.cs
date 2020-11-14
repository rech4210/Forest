using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor_Move : MonoBehaviour
{
    public float moveSpeed = 10;
    public float upDownSpeed = 10;
    public float rotationSpeed = 10;

    public float rotateLimitMin = 0;
    public float rotateLimitMax = 0;

    [HideInInspector] public float xMove, zMove;

    private GameObject _camera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _camera = Camera.main.gameObject;
    }

    void Update()
    {
        Move();

        Rotate();

        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetRotation();
        }

        if(Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Move()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");

        if (xMove != 0 || zMove != 0)
        {
            Vector3 moveValue = new Vector3(xMove, 0, zMove) * moveSpeed * Time.deltaTime;
            transform.Translate(moveValue);
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            Vector3 position = transform.position;
            position.y += upDownSpeed * Time.deltaTime;
            transform.position = position;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            Vector3 position = transform.position;
            position.y -= upDownSpeed * Time.deltaTime;
            transform.position = position;
        }
    }

    void Rotate()
    {
        float mouseInput_x = Input.GetAxisRaw("Mouse X");
        float mouseInput_y = Input.GetAxisRaw("Mouse Y");

        if (mouseInput_x != 0 || mouseInput_y != 0)
        {   // 카메라는 x축 회전 / 캐릭터는 y축 회전
            Vector3 rotationValue = new Vector3(-mouseInput_y, mouseInput_x, 0) * rotationSpeed;
            rotationValue.x = Mathf.Clamp(rotationValue.x, rotateLimitMin, rotateLimitMax);

            Vector3 rotation_eular = _camera.transform.rotation.eulerAngles;
            rotation_eular.x += rotationValue.x;
            _camera.transform.rotation = Quaternion.Euler(rotation_eular);

            rotation_eular = transform.rotation.eulerAngles;
            rotation_eular.y += rotationValue.y;
            transform.rotation = Quaternion.Euler(rotation_eular);
        }
    }

    void ResetRotation()
    {
        transform.rotation = new Quaternion();
        _camera.transform.rotation = new Quaternion();
    }
}
