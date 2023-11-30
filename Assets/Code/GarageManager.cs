using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GarageManager : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] CinemachineVirtualCamera garageCamera;

    private Vector3 closedDoorPosition;

    void Start()
    {
        OnEnable();
        closedDoorPosition = GameObject.FindWithTag("Door").transform.position;
        /* closedDoorPosition = door.transform.position; */
    }

    private void OnEnable()
    {
        CameraSwitch.Register(playerCamera);
        CameraSwitch.Register(garageCamera);
        CameraSwitch.SwitchCamera(playerCamera);
    }

    private void OnDisable()
    {
        CameraSwitch.Unregister(playerCamera);
        CameraSwitch.Unregister(garageCamera);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // The player has entered the garage area
            /* GameManager.Instance.EnterGarage(); */
            Debug.Log("I AM IN THE GARAGE");
            Debug.Log("Currency:" + PlayerStats.Instance.currency);
            GameManager.Instance.inGarage = true;
            if (CameraSwitch.IsActiveCamera(playerCamera))
            {
                ShopManager.Instance.showhidePanel();
                CameraSwitch.SwitchCamera(garageCamera);
            }
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // The player has left the garage area
            GameManager.Instance.ExitGarage();
            if (CameraSwitch.IsActiveCamera(garageCamera))
            {
                ShopManager.Instance.showhidePanel();
                CameraSwitch.SwitchCamera(playerCamera);
            }
        }
        GameManager.Instance.inGarage = false;
    }

    public void OpenGarage()
    {
        GameObject door = GameObject.FindWithTag("Door");
        Vector3 newPosition = new Vector3(0f ,-9.7f, 0f);
        door.transform.position = newPosition;
    }

    public void CloseGarage()
    {
        GameObject door = GameObject.FindWithTag("Door");
        door.transform.position = closedDoorPosition;
    }
}
