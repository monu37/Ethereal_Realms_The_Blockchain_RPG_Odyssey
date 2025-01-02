using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour, ISaveManager
{

    [SerializeField] UIFadeScreen FadeScreen;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject EndText;
    [SerializeField] GameObject RestartBtn;

    [Space]
    [SerializeField] GameObject CharacterUI;
    [SerializeField] GameObject SkillUI;
    [SerializeField] GameObject CraftUI;
    [SerializeField] GameObject OptionsUI;
    [SerializeField] GameObject InGameUI;

    public UISkillTooltip SkillToolTip;
    public UiItemToolInfo ItemToolTip;
    public UIStatToolTip StatToolTip;
    public UiCraftPanel CraftPanel;


    //
    [SerializeField] UiVolumeSlider[] VolumeSettings;
    private void Awake()
    {
       
        FadeScreen.gameObject.SetActive(true);
    }

    private void Start()
    {
        //switchto(SkillUI);
        switchto(InGameUI);

        ItemToolTip.gameObject.SetActive(false);
        StatToolTip.gameObject.SetActive(false);
        //SkillToolTip.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (ShopScript.instance.IsShop && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.P))//character open from shop
        {
            switchtokeyboard(CharacterUI);
        }
       
        
        if (Input.GetKeyDown(KeyCode.C) )  //character
        {
            switchtokeyboard(CharacterUI);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            switchtokeyboard(CraftUI);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            switchtokeyboard(SkillUI);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            switchtokeyboard(OptionsUI);
        }
    }
    public void switchto(GameObject _menu)
    {


        for (int i = 0; i < transform.childCount; i++)
        {
            bool IsFadeScreen = transform.GetChild(i).GetComponent<UIFadeScreen>() != null;
            if (!IsFadeScreen)
            {
                transform.GetChild(i).gameObject.SetActive(false);

            }
        }

        if (_menu != null)
        {
            AudioManager.instance.playsfx(5, null);
            _menu.SetActive(true);
        }

        if (GameManager.instance != null)
        {
            if (_menu == InGameUI)
            {
                GameManager.instance.pausegame(false);
            }
            else
            {
                GameManager.instance.pausegame(true);
            }
        }

    }

    public void switchtokeyboard(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            checkingameui();

            return;
        }

        switchto(_menu);
    }

    void checkingameui()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(1).gameObject.activeSelf && transform.GetChild(i).GetComponent<UIFadeScreen>() == null)
            {
                return;
            }
        }

        switchto(InGameUI);
    }

    public void switchendscreen()
    {

        FadeScreen.fadeout();

        StartCoroutine(endscreencoroutine());
    }

    IEnumerator endscreencoroutine()
    {
        yield return new WaitForSeconds(1);

        GameOverPanel.SetActive(true);
        EndText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        RestartBtn.SetActive(true);

    }

    public void restartgamebutton() => GameManager.instance.restartgame();

    public void loaddata(GameData _data)
    {
        foreach (KeyValuePair<string, float> pair in _data.VolumeSetings)
        {
            foreach (UiVolumeSlider item in VolumeSettings)
            {
                if (item.Parameter == pair.Key)
                {
                    item.loadslider(pair.Value);
                }

            }
        }
    }

    public void savedata(ref GameData _data)
    {
        _data.VolumeSetings.Clear();

        foreach (UiVolumeSlider item in VolumeSettings)
        {
            _data.VolumeSetings.Add(item.Parameter, item.slider.value);
        }
    }
}
