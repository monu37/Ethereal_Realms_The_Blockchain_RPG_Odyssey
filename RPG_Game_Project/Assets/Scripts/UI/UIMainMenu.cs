using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public static UIMainMenu instance;

    [SerializeField] GameObject ContinueGameBtn;
    [SerializeField] UIFadeScreen FadeScreen;
    [SerializeField] GameObject MainPanel, ProfilePanel;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ContinueGameBtn.SetActive(false);
    }

    public void openmainpanel()
    {
        MainPanel.SetActive(true);
        ProfilePanel.SetActive(false);
    }
    public void openprofilepanel()
    {
        MainPanel.SetActive(false);
        ProfilePanel.SetActive(true);
    }

    public void checkforcontinuegame()
    {
        if (SaveManager.instance.IsHaveSaveData() == false)
        {
            ContinueGameBtn.SetActive(false);
        }
    }

    public void continuegame()
    {
        StartCoroutine(loadscenefadeeffect(1.5f));

    }

    public void newgame()
    {
        SaveManager.instance.deletesavedata();

        StartCoroutine(loadscenefadeeffect(1.5f));


    }
    public void exitgame()
    {
        print("Exit game");
    }

    void loadgameplay()
    {
        SceneManager.LoadScene("GamePlay");
    }

    IEnumerator loadscenefadeeffect(float _delay)
    {
        FadeScreen.fadeout();

        yield return new WaitForSeconds(_delay);

        loadgameplay();
    }
}
