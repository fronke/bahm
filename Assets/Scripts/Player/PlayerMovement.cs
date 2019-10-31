using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public int playerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float speed = 20f;                 // How fast the tank moves forward and back.           

    private string movementVerticalAxisName;          // The name of the input axis for moving forward and back. 
    private string movementHorizontalAxisName;
    private string viewVerticalAxisName;          // The name of the input axis for moving forward and back. 
    private string viewHorizontalAxisName;
    private Rigidbody rigbody;              // Reference used to move the tank.
    private float movementVerticalInputValue;
    private float movementHorizontalInputValue;
    private float viewVerticalInputValue;
    private float viewHorizontalInputValue;

    private float joystickDeadZone = 0.2f;
    private SpriteRenderer viewfinderSprite;

    private void Awake()
    {
        rigbody = GetComponent<Rigidbody>();
        viewfinderSprite = transform.FindChild("Viewfinder").GetComponent<SpriteRenderer>();
    }


    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        rigbody.isKinematic = false;

        // Also reset the input values.
        movementVerticalInputValue = 0f;
        movementHorizontalInputValue = 0f;
        viewVerticalInputValue = 0f;
        viewHorizontalInputValue = 0f;
    }


    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        rigbody.isKinematic = true;
    }


    private void Start()
    {
        // The axes names are based on player number.
        movementVerticalAxisName = "Vertical" + playerNumber;
        movementHorizontalAxisName = "Horizontal" + playerNumber;
        viewVerticalAxisName = "VerticalView" + playerNumber;
        viewHorizontalAxisName = "HorizontalView" + playerNumber;
    }


    private void Update()
    {
        // Store the value of both input axes.        
        movementVerticalInputValue = Input.GetAxis(movementVerticalAxisName);
        movementHorizontalInputValue = Input.GetAxis(movementHorizontalAxisName);
        viewVerticalInputValue = Input.GetAxis(viewVerticalAxisName);
        viewHorizontalInputValue = Input.GetAxis(viewHorizontalAxisName);
    }



    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();

        Turn();
    }


    private void Move()
    {
        //Use the two store floats to create a new Vector2 variable movement.
        Vector3 movement = new Vector3(movementHorizontalInputValue, 0,movementVerticalInputValue) * speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        rigbody.MovePosition(rigbody.position + movement);
    }


    private void Turn()
    {
        // We are keeping the last rotation
        if (Mathf.Abs(viewHorizontalInputValue) > joystickDeadZone || Mathf.Abs(viewVerticalInputValue) > joystickDeadZone)
        {
            rigbody.transform.eulerAngles = new Vector3(0, Mathf.Atan2(viewHorizontalInputValue, viewVerticalInputValue) * 180 / Mathf.PI, 0);
            ShowViewfinder();
        }
        else
        {
            HideViewfinder();
        }
       
    }


    private void ShowViewfinder()
    {
        viewfinderSprite.enabled = true;
    }

    private void HideViewfinder()
    {
        viewfinderSprite.enabled = false;
    }
}
