using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareManager : MonoBehaviour
{
    public void Share() {
        string gameUrl = "https://gcipres.netlify.app/games/catch-the-ball/";
        #if UNITY_ANDROID
            gameUrl = "https://play.google.com/store/apps/details?id=com.ipappslab.catchtheball";
        #elif UNITY_IOS
            gameUrl = "https://apps.apple.com/app/idXXXXXXXXX";
        #endif

        string shareText = 
            $"Boom! 💥 {GameManager.score} points. Dare you? Play now on Catch the Ball 🔥.\n" +
            $"👉 {gameUrl}";

        new NativeShare()
            .SetSubject("Catch the ball")
            .SetText(shareText)
            .SetUrl(gameUrl)
            .SetCallback((result, target) => Debug.Log(shareText))
            .Share();
    }
}
