using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luces
{
    public class Drone : MonoBehaviour
    {
        [SerializeField] private int health = 100;
        [SerializeField] private float speed = 8.0f;
        [SerializeField] private float dashLength = 5.0f;
        [SerializeField] private float rotationSpeed = 300.0f;
        [SerializeField] private float floatOffset = 0.1f;
        [SerializeField] private KeyCode fireKeyCode;
        [SerializeField] private KeyCode crouchKeyCode;
        [SerializeField] private KeyCode dashKeyCode;
        [SerializeField] private string vAxisName = "Vertical";
        [SerializeField] private string hAxisName = "Horizontal";


        private bool isCrouched = false;
        private bool isDetected = false;

        private float timer = 0f;


        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            AirFloat();
            InputHandler();
        }

        void InputHandler()
        {
            MovementHandler();
            //ShootHandler();
        }

        void ShootHandler()
        {
            if (Input.GetKeyDown(fireKeyCode))
            {
            }
        }

        void AirFloat()
        {
        }

        void MovementHandler()
        {
            // Get the horizontal and vertical axis.
            // By default they are mapped to the arrow keys.
            // The value is in the range -1 to 1
            float translation = Input.GetAxis(vAxisName) * speed;
            float rotation = Input.GetAxis(hAxisName) * rotationSpeed;

            // Make it move 10 meters per second instead of 10 meters per frame...
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            // Air float effect
            timer += Time.deltaTime;
            float finalYPosition = Mathf.Lerp(-floatOffset, floatOffset, Mathf.PingPong(timer, 1));

            // Move translation along the object's z-axis
            transform.Translate(0, finalYPosition, translation);

            // Rotate around our y-axis
            transform.Rotate(0, rotation, 0);

        }
    }
}