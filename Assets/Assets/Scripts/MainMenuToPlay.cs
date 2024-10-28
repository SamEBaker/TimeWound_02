using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuToPlay : MonoBehaviour
{
    public GameObject howtoPlayScreen;
    [SerializeField]
    private float WaitTime;
    IEnumerator HowtoPlayScreen(float WaitTime)
    {
        howtoPlayScreen.SetActive(true);
        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Onplay()
    {
        StartCoroutine(HowtoPlayScreen(WaitTime));
    }
}
