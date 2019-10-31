using UnityEngine;
using UnityEngine.UI;


public class PlayerShooting : MonoBehaviour
{
    public int playerNumber = 1;
    public Rigidbody bullet;
    public Transform fireTransform;
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 0.75f;
    public int maxBullets = 10;

    private string fireButton;
    private float currentLaunchForce;
    private float chargeSpeed;
    private bool fired;
    private int leftBullets;


    private void OnEnable()
    {
        // When the player is turned on, reset the launch force and the UI
        currentLaunchForce = minLaunchForce;

        Reload();
    }


    private void Start()
    {
        // The fire axis is based on the player number.
        fireButton = "Fire" + playerNumber;

        // The rate that the launch force charges up is the range of possible forces by the max charge time.
        if (maxChargeTime > 0)
        {
            chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
        }
    }


    private void Update()
    {
        // If the max force has been exceeded and the bullet hasn't yet been launched...
        if (currentLaunchForce >= maxLaunchForce && !fired)
        {
            // ... use the max force and launch the shell.
            currentLaunchForce = maxLaunchForce;
            Fire();
        }
        // Otherwise, if the fire button has just started being pressed...
        else if (Input.GetButtonDown(fireButton))
        {
            // ... reset the fired flag and reset the launch force.
            fired = false;
            currentLaunchForce = minLaunchForce;

            if (maxChargeTime == 0)
            {
                currentLaunchForce = maxLaunchForce;
                Fire();
            }
        }
        // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
        else if (Input.GetButton(fireButton) && !fired)
        {
            // Increment the launch force and update the slider.
            currentLaunchForce += chargeSpeed * Time.deltaTime;
        }
        // Otherwise, if the fire button is released and the shell hasn't been launched yet...
        else if (Input.GetButtonUp(fireButton) && !fired)
        {
            // ... launch the shell.
            Fire();
        }
    }


    private void Fire()
    {
        // Set the fired flag so only Fire is only called once.
        fired = true;

        if (leftBullets > 0)
        {
            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance = Instantiate(bullet, fireTransform.position, Quaternion.Euler(0,0,0)) as Rigidbody;
       

            // Set the bullet's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = currentLaunchForce * fireTransform.up;

            leftBullets--;
        }

        // Reset the launch force.  This is a precaution in case of missing button events.
        currentLaunchForce = minLaunchForce;        
    }

    private void Reload()
    {
        leftBullets = maxBullets;
    }
}