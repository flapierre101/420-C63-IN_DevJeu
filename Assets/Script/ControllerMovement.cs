using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour
{
    public float Speed;

    private int _secret;

    public int MyProperty { get; set; }


    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var direction = new Vector3(horizontal, vertical, 0);

        if (direction.magnitude > 1)
            direction = direction.normalized;

        transform.position += direction * Speed * Time.deltaTime;
    }
}
