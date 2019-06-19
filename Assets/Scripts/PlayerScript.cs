using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public Vector3 dir;
    public GameObject ps;
    public bool isDead;
    public GameObject RestartButton;

    [HideInInspector]
    public int score = 0;
    public Text ScoreText;

    public LayerMask whatIsGround;
    private bool isPlaying = false;
    public Transform ContactPoint;
    public Rigidbody rid;

    public Material[] material;
    private Material oldMaterial;
    private int color = 0;
    private int countPickup = 0;
    private int item = 0;
    private int tempItem;

    public float cubeSize = 0.2f;
    public int cubeInRow = 5;
    public float cubePivotDistance;
    Vector3 cubePivot;
    public int explosionRadius = 4;
    public int explosionForce = 500;
    public float explosionUpward = 0.4f;

    [HideInInspector]
    public bool isStart = false;

    private bool isCheckSound;
    public Animator GameOverAnim;
    private PlayerData data;


    void Start()
    {
        isDead = false;
        dir = Vector3.zero;
        cubePivotDistance = cubeSize * cubeInRow / 2;
        cubePivot = new Vector3(cubePivotDistance, cubePivotDistance, cubePivotDistance);

        data = SaveSystem.LoadPlayer();
        isCheckSound = data.isCheckSound;
        tempItem = data.item;
    }

    void Update()
    {
        if (isCheckSound != FindObjectOfType<UIController>().checkSound)
        {
            isCheckSound = FindObjectOfType<UIController>().checkSound;
        }

        if (!Grounded() && isPlaying)
        {
            RaycastHit hit;
            Ray downRay = new Ray(transform.position, -Vector3.up);

            if (!Physics.Raycast(downRay, out hit))
            {

                isDead = true;
                RestartButton.SetActive(true);
                CameraScript.Instance.isDead = true;
                CameraScript.Instance.StopCamera();
                GameOverAnim.SetTrigger("GameOver");
                
                isStart = false;
                int temp = item + tempItem;
                if (data.bestScore < score)
                    SaveSystem.SaveScore(isCheckSound, score, temp);
                else
                    SaveSystem.SaveScore(isCheckSound, data.bestScore, temp);
            }
        }

        if (isStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isCheckSound)
                    FindObjectOfType<AudioManager>().Play("Tab");
                
                isPlaying = true;
                score++;
                ScoreText.text = score.ToString();

                if (dir == Vector3.forward)
                    dir = Vector3.left;
                else
                    dir = Vector3.forward;
            }

            float amountToMove = speed * Time.deltaTime;
            transform.Translate(dir * amountToMove);

            if (isCheckSound)
                FindObjectOfType<AudioManager>().Play("EndGame");

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            countPickup++;
            score += 3;
            item++;
            ScoreText.text = score.ToString();
            other.gameObject.SetActive(false);

            for (int x = 0; x < cubeInRow; x++)
                for (int y = 0; y < cubeInRow; y++)
                    for (int z = 0; z < cubeInRow; z++)
                        CreatePs(other, x, y, z);

            Vector3 explosionPos = other.transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
            foreach (var item in colliders)
            {
                Rigidbody rb = item.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(explosionForce, other.transform.position, explosionRadius, explosionUpward);
            }

            if (isCheckSound)
                FindObjectOfType<AudioManager>().Play("ColliderPickup");

            if (countPickup == 5)
            {
                color = Random.Range(0, 7);
                oldMaterial = other.GetComponentInChildren<Renderer>().material;
                TileManagerScript.Instance.listObj.ForEach(e =>
                {
                    e.GetComponentInChildren<Renderer>().material.Lerp(oldMaterial, material[color], 0.8f);
                });

                if (isCheckSound)
                    FindObjectOfType<AudioManager>().Play("ChangeColor");
                countPickup = 0;
            }
        }
    }

    private void CreatePs(Collider other, int x, int y, int z)
    {
        GameObject piece = Instantiate(ps, other.gameObject.transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubePivot, Quaternion.identity);
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().useGravity = false;
        Destroy(piece.GetComponent<SphereCollider>());
        Destroy(piece, 0.75f);
    }

    private bool Grounded()
    {
        Collider[] Colliders = Physics.OverlapSphere(ContactPoint.position, .5f, whatIsGround);

        for (int i = 0; i < Colliders.Length; i++)
        {
            if (Colliders[i].gameObject != gameObject)
                return true;
        }
        return false;
    }
}
