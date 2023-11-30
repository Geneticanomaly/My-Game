using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    private float moveInput;
    private float turnInput;
    private bool isCarGrounded;

    public float airDrag;
    public float groundDrag;

    private float normalDrag;
    public float modifiedDrag;

    public float alignToGroundTime = 20;

    public float fwdSpeed;
    public float revSpeed = 75;
    public float turnSpeed = 165;
    
    public LayerMask groundLayer;

    public Rigidbody sphereRB;
    public Rigidbody carRB;

    private PlayerStats playerStats;

    public bool speedBuff;

    private Vector3 playerSize;
    private Vector3 colliderSize;
    public BoxCollider carCollider;

    void Start()
    {
        // detach rigidbody from car
        sphereRB.transform.parent = null;
        carRB.transform.parent = null;

        playerSize = transform.localScale;
        colliderSize = carCollider.size;
        //Transform carColliderTransform = transform.Find("CarCollider");
        //carCollider = carColliderTransform.GetComponent<BoxCollider>();

        // Find the GameObject with the "PlayerStatsObject" tag.
        GameObject playerStatsObject = GameObject.FindGameObjectWithTag("Player");

        if (playerStatsObject != null)
        {
            playerStats = playerStatsObject.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                // Set the forwardSpeed to the value from PlayerStats.
                fwdSpeed = playerStats.speed;
            }
        }

        normalDrag = sphereRB.drag;
    }

    void Update()
    {

        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        moveInput *= moveInput > 0 ? fwdSpeed : revSpeed;

        // set cars position to sphere
        transform.position = sphereRB.transform.position;

        // set cars rotation
        float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        transform.Rotate(0, newRotation, 0, Space.World);
        
        /* float speed = sphereRB.velocity.magnitude;
        Debug.Log("My speed is: " + speed); */

        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);

        Quaternion toRotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);


        sphereRB.drag = isCarGrounded ? groundDrag : airDrag;

    }

    void FixedUpdate()
    {
        if (isCarGrounded)
        {
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        }
        else
        {
            sphereRB.AddForce(transform.up * -150f);
        }

        carRB.MoveRotation(transform.rotation);
    }


    public void EnableSpeedBuff(float amount)
    {
        speedBuff = true;

        fwdSpeed += amount;
        playerStats.speed += amount;

        // Increase player size
        /* if (!(transform.localScale != playerSize)) {
            transform.localScale += transform.localScale + new Vector3(.1f, .1f, .1f);
            carCollider.size += carCollider.size + new Vector3(.1f, 1.2f, .2f);
        } */
        

        // Decrease player size
        /* if (!(transform.localScale != playerSize)) {
            transform.localScale += transform.localScale + new Vector3(-1.4f, -1.4f, -1.4f);
            carCollider.size -= carCollider.size - new Vector3(1.4f, 1.4f, 2.8f);
        } */

        StartCoroutine(DisableSpeedBuff(amount));
    }

    private IEnumerator DisableSpeedBuff(float amount)
    {
        yield return new WaitForSeconds(10.0f);

        fwdSpeed -= amount;
        playerStats.speed -= amount;

        // Reset playerSize and Collider
        /* transform.localScale = playerSize;
        carCollider.size = colliderSize; */

        speedBuff = false;
    }

    public void EnableIncreaseSizeBuff() {
    // Increase player size
    if (!(transform.localScale != playerSize)) {
        transform.localScale += transform.localScale + new Vector3(.1f, .1f, .1f);
        carCollider.size += carCollider.size + new Vector3(.1f, 1.2f, .2f);
    }

    StartCoroutine(DisableIncreaseSizeBuff());
    }

    private IEnumerator DisableIncreaseSizeBuff() {

        yield return new WaitForSeconds(15.0f);

        // Reset playerSize and Collider
        transform.localScale = playerSize;
        carCollider.size = colliderSize;
    }

    public void EnableDecreaseSizeBuff() {
        // Decrease player size
        if (!(transform.localScale != playerSize)) {
            transform.localScale += transform.localScale + new Vector3(-1.4f, -1.4f, -1.4f);
            carCollider.size -= carCollider.size - new Vector3(1.4f, 1.4f, 2.8f);
        }

        StartCoroutine(DisableDecreaseSizeBuff());
    }

    private IEnumerator DisableDecreaseSizeBuff() {

        yield return new WaitForSeconds(15.0f);

        // Reset playerSize and Collider
        transform.localScale = playerSize;
        carCollider.size = colliderSize;
    }

}
