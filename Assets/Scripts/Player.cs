using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float dashLength = 5.0f;
    [SerializeField] private float rotationSpeed = 300.0f;

    private bool isCrouched = false;
    private bool isDetected = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    void InputHandler()
    {
        MovementHandler();
        CrouchHandler();
        DashHandler();
        Debug.Log("axis sign: " + Mathf.Sign(Input.GetAxis("Vertical")));
    }

    void CrouchHandler()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouched = !isCrouched;
            float height = isCrouched ? 0.5f : 1;
            transform.localScale = new Vector3(1, height, 1);
        }
    }

    void DashHandler()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            float vAxis = Input.GetAxis("Vertical");
            float translation = vAxis + dashLength * Mathf.Sign(vAxis);
            transform.Translate(0, 0, translation);

        }
    }

    void MovementHandler()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);

    }
}
