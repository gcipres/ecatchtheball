using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareManager : MonoBehaviour
{
    public void Share() {
        Debug.Log("Share");
        GameManager.gameState = GameState.Share;
    }
}
