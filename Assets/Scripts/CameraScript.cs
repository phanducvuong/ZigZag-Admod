using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject Player;
    private Vector3 Offset;
    public float smoothy;
    private static CameraScript instance;
    public bool isDead = false;
    private Vector3 tempPosition;

    public static CameraScript Instance
    {

        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<CameraScript>();
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (!isDead)
        {
            Vector3 desiredPosition = Player.transform.position + Offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothy);
            tempPosition = smoothPosition;
            transform.position = smoothPosition;
        }
    }

    public void StopCamera()
    {
        transform.position = tempPosition;
    }
}
