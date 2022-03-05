using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange
{
    private static bool roadNow = false;

    /// <summary>
    /// Titleシーンに移行する
    /// </summary>
    public static void RoadTitle()
    {
        if (roadNow)
        {
            return;
        }
        roadNow = true;
        FadeController.StartFadeOut(Title);
    }   
    /// <summary>
    /// Gameシーンに移行する
    /// </summary>
    public static void LoadGame(string name = "BattleStageScene")
    {
        if (roadNow)
        {
            return;
        }
        roadNow = true;
        FadeController.StartFadeOut(() => Game(name));
    }
    /// <summary>
    /// Customizeシーンに移行する
    /// </summary>
    public static void LoadCustomize()
    {
        if (roadNow)
        {
            return;
        }
        roadNow = true;
        FadeController.StartFadeOut(Customize);
    }
    private static void Title()
    {
        roadNow = false;
        SceneManager.LoadScene("TitleScene");
    }
    private static void Game(string name)
    {
        roadNow = false;
        SceneManager.LoadScene(name);
    }
    private static void Customize()
    {
        roadNow = false;
        BulletPool.FullReset();
        EffectPool.FullReset();
        SceneManager.LoadScene("CustomizeScene");
    }
}
