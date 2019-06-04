using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileManagerScript : MonoBehaviour
{

    public GameObject[] TitlePrefab;

    public GameObject CurrentTile;

    private static TileManagerScript instance;

    public List<GameObject> listObj;

    private Stack<GameObject> leftTiles = new Stack<GameObject>();
    private Stack<GameObject> topTiles = new Stack<GameObject>();

    //GETER SETER
    public Stack<GameObject> LeftTiles
    {
        get { return leftTiles; }
        set { leftTiles = value; }
    }

    public Stack<GameObject> TopTiles
    {
        get { return topTiles; }
        set { topTiles = value; }
    }

    public static TileManagerScript Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<TileManagerScript>();

            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        listObj = new List<GameObject>();
        CreateTiles(100);

        int randomColor = Random.Range(0, 2);
        for (int i = 0; i < 100; i++)
        {
            SpawnTiles();
        }
    }

    public void CreateTiles(int amount)
    {

        for (int i = 0; i < amount; i++)
        {
            leftTiles.Push(Instantiate(TitlePrefab[0]));
            topTiles.Push(Instantiate(TitlePrefab[1]));
            topTiles.Peek().name = "TopTile";
            topTiles.Peek().SetActive(false);
            leftTiles.Peek().name = "LeftTile";
            leftTiles.Peek().SetActive(false);
        }
    }

    public void SpawnTiles()
    {
        int RandomIndex = Random.Range(0, 2);

        if (RandomIndex == 0)
        {
            GameObject tmp = leftTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = CurrentTile.transform.GetChild(0).transform.GetChild(RandomIndex).position;
            CurrentTile = tmp;
            listObj.Add(CurrentTile);
        }
        else if (RandomIndex == 1)
        {
            GameObject tmp = topTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = CurrentTile.transform.GetChild(0).transform.GetChild(RandomIndex).position;
            CurrentTile = tmp;
            listObj.Add(CurrentTile);
        }

        int spawnRandom = Random.Range(0, 10);
        if (spawnRandom == 0)
            CurrentTile.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("ZigZagScene");
    }
}
