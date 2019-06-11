using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    private PlayerScript PlayerScript;
    public GameObject Panel;

    public Texture[] rawImage;
    public RawImage sound;
    public Text BestScoreText;
    public Text ItemText;

    [HideInInspector]
    public bool checkSound;

    public GameObject StartGameAnim;

    [HideInInspector]
    public int bestScore;
    private int item;

    void Awake()
    {
        PlayerScript = FindObjectOfType<PlayerScript>();
        checkSound = true;
        bestScore = 0;
        item = 0;

        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null) {
            checkSound = data.isCheckSound;
            bestScore = data.bestScore;
            item = data.item;
        } else
        {
            SaveSystem.SaveScore(checkSound, bestScore, item);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (checkSound)
            sound.texture = rawImage[1];
        else
            sound.texture = rawImage[0];
        
        BestScoreText.text = bestScore.ToString();
        ItemText.text = "X\n" + item.ToString();
    }

    public void isStart() {
        StartCoroutine(_isDisable());
        StartGameAnim.GetComponent<Animator>().Play("StartGame");
        PlayerScript.isStart = true;
    }

    IEnumerator _isDisable() {
        yield return new WaitForSeconds(1);
        Panel.SetActive(false);
    }

    public void SoundOnOff()
    {
        if (checkSound)
        {
            sound.texture = rawImage[0];
            checkSound = false;
            SaveSystem.SaveScore(checkSound, bestScore, item);
        }
        else
        {
            sound.texture = rawImage[1];
            checkSound = true;
            SaveSystem.SaveScore(checkSound, bestScore, item);
        }
    }
}
