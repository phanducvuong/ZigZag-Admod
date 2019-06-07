using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject GameObject;
    private PlayerScript PlayerScript;
    public Animator anim;
    public Image Panel;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = FindObjectOfType<PlayerScript>();
    }

    public void StartGame()
    {
        anim.Play("StartGame", 0);
        StartCoroutine(Disaple());
    }

    IEnumerator Disaple() {
        yield return new WaitForSeconds(1);
        GameObject.SetActive(false);
        PlayerScript.isStart = true;
        Panel.enabled = false;
    }
}
