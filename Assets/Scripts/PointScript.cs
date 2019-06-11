using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour
{

    public GameObject Player;
    private Vector3 Offset;
    public float smoothy;
    private Vector3 tempPosition;

    // Start is called before the first frame update
    void Start()
    {
        Offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = Player.transform.position + Offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothy);
        tempPosition = smoothPosition;
        transform.position = smoothPosition;
    }
}
