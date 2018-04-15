using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript: MonoBehaviour
{

    public void GoBack()
    {

        SceneManager.LoadScene(0);
    }

    public void GoLevel1()
    {

        SceneManager.LoadScene(2);
    }

    public void GoLevel2()
    {

        SceneManager.LoadScene(3);
    }

}
