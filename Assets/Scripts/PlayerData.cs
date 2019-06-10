using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool isCheckSound;
    public int bestScore;
    public int item;

    public PlayerData(bool sound, int socre, int items) {
        isCheckSound = sound;
        bestScore = socre;
        item = items;
    }
}
