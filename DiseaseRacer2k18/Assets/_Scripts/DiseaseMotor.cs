using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(Rigidbody))]
public class DiseaseMotor : MonoBehaviour
{

    public int playerId = 0; // The Rewired player id of this character

    public float moveSpeed = 300f;
    public float rotationRate = 100f;

    private Player player; // The Rewired Player
    private Rigidbody rb;
    private BaseDisease baseDiseaseRef;
    private Vector3 moveVector;
    private bool boost;

    //float, 0-1
    private float boostFuel = 0;
    private float boostRegenRate = 0.1f;

    public float BoostFuel
    {
        get { return boostFuel; }
    }

    void Start()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);

        // Get the character controller
        rb = GetComponent<Rigidbody>();

        baseDiseaseRef = GetComponent<BaseDisease>();
    }

    void FixedUpdate()
    {
        if (baseDiseaseRef.IsRacing)
        {
            GetInput();
            ProcessInput();

            if (boostFuel < 1f)
            {
                Mathf.Clamp01(boostFuel += boostRegenRate * Time.deltaTime);
            }
        }
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("steering"); // get input by name or action id
        moveVector.y = player.GetAxis("accellerate");
        boost = player.GetButtonDown("boost");
    }

    private void ProcessInput()
    {
        if (moveVector.x != 0.0f)
        {
            rb.AddTorque(0, moveVector.x * rotationRate * Time.deltaTime, 0);
        }

        if (moveVector.y != 0.0f)
        { 
            rb.AddForce(rb.transform.forward * moveVector.y * moveSpeed * Time.deltaTime);
        }

        // Process fire
        if (boost)
        {
            Debug.Log("ZOOM!");
        }
    }
}