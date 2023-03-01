using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public Teleporter teleporter;
    public PlayerMoveWithPoints playerMoveWithPoints;

    public GameObject phone;
    public GameObject map;

    // Function buttons
    public GameObject buttonEnter, buttonDialog, buttonMinigame, buttonParopharm, buttonAgitation;

    // Menu buttons
    public GameObject gameMenu, mainButtons, saveButtons, loadButtons;

    // Info bars
    public GameObject itemBar, timeBar, phoneBar, paropharmBar;


    public GameObject allSavesMenu;
    public GameObject bottomGroup;
    public static GameObject bottomGroupS;
    public GameObject mainContainer;

    public GameObject phoneButtons;
    public GameObject messagesMenu;
    public GameObject messageMenu;

    public GameObject miniGame;

    public Slider weekbar;
    public TextMeshProUGUI weektext;

    public int enterIndex;

    public Player _player;

    public List<GameObject> saves;

    public int currentMiniGame;
    public StateNPC curentStateNPC;

    public static bool startGameBool = true;

    public GameObject miniGameTrigger;




    private void OnEnable()
    {
        if (startGameBool)
            StartGame();
    }
    private void Start()
    {
        bottomGroupS = gameObject.GetComponent<UI>().bottomGroup;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            activateMenu(!gameMenu.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            activatePhone(!phone.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activateBottomGroup(!bottomGroup.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            activateMap();
        }
    }
    public void tp(int index)
    {
        teleporter.setVar(index);

        map.SetActive(false);
        phone.SetActive(false); 
    }
    public void activateMenu(bool b) => gameMenu.SetActive(b);
    public void closeMap(bool b) => map.SetActive(b);
    public void activatePhone(bool b) => phone.SetActive(b);
    public void activateBottomGroup(bool b) => bottomGroup.SetActive(b);

    public void activateMap()
    {
        map.SetActive(true);

        gameMenu.SetActive(false);
        phone.SetActive(false);
        bottomGroup.SetActive(false);
    }
    public void deActivateMap()
    {
        map.SetActive(false);
        phone.SetActive(true);
        bottomGroup.SetActive(true);
    }
    public void activateMessages()
    {
        messagesMenu.SetActive(true);
        phoneButtons.SetActive(false);
    }

    //activation of buttons designed for
    //various customer interactions with gaming systems.
    public void activeButtonEnter(bool bl, string textBtn = " ")
    {
        buttonEnter.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = textBtn;
        buttonEnter.SetActive(bl);
    }
    public void activeButtonDialog(bool bl) => buttonDialog.SetActive(bl);
    public void activeButtonMinigame(bool bl)  => buttonMinigame.SetActive(bl);
    public void activeButtonParapharm(bool bl) => buttonParopharm.SetActive(bl);
    public void activeButtonAgitation(bool bl) => buttonAgitation.SetActive(bl);

    //activates buttons for entering/exiting buildings
    public void tpButton()
    {
        tp(enterIndex);
        buttonEnter.SetActive(false);
    }
    public void ExitGame() => Application.Quit();

    public void PauseGame()
    {
        gameMenu.SetActive(!gameMenu.activeSelf);

        Time.timeScale = 1.0f;
    }
    public void OpenCloseSaveMenu()
    {
        mainButtons.SetActive(!mainButtons.activeSelf);
        saveButtons.SetActive(!saveButtons.activeSelf);
    }
    public void OpenCloseLoadMenu()
    {
        mainButtons.SetActive(!mainButtons.activeSelf);
        loadButtons.SetActive(!loadButtons.activeSelf);
    }
    public void CloseAllSaves()
    {
        allSavesMenu.SetActive(!allSavesMenu.activeSelf);
        loadButtons.SetActive(!loadButtons.activeSelf);
    }
    public void DeleteAllSaves()
    {
        PlayerPrefs.DeleteAll();

        string path = Application.dataPath + "/Saves";

        Directory.Delete(path, true);
    }
    public void AllSaves()
    {
        GetAllSavesInfo.SaveDB();
        gameObject.GetComponent<AddAllSavesToPanel>().CreateSaveBlocks(GetAllSavesInfo.keys, GetAllSavesInfo.dates, GetAllSavesInfo.count);

        allSavesMenu.SetActive(!allSavesMenu.activeSelf);
        loadButtons.SetActive(!loadButtons.activeSelf);
    }
    public void UpdateCurrentDay()
    {
        switch (GameTime.currentTime)
        {
            case 0:
                weekbar.value = 0;
                weektext.text += ", I часть дня.";
                return;
            case 1:
                weekbar.value = 1;
                weektext.text += ", II часть дня.";
                return;
            case 2:
                weekbar.value = 2;
                weektext.text += ", III часть дня.";
                return;
            case 3:
                weekbar.value = 3;
                weektext.text += ", IV часть дня.";
                return;
            case 4:
                weekbar.value = 4;
                weektext.text += ", V часть дня.";
                return;
            case 5:
                weekbar.value = 5;
                weektext.text += ", VI часть дня.";
                return;
            case 6:
                weekbar.value = 6;
                weektext.text += ", VII часть дня.";
                return;
            case 7:
                weekbar.value = 7;
                weektext.text += ", VIII часть дня.";
                return;
        }

    }
    public void SetTextDay()
    {
        switch (GameTime.currentDay)
        {
            case 0:
                weektext.text = "Понедельник";
                return;
            case 1:
                weektext.text = "Вторник";
                return;
            case 2:
                weektext.text = "Среда";
                return;
            case 3:
                weektext.text = "Четверг";
                return;
            case 4:
                weektext.text = "Пятница";
                return;
            case 5:
                weektext.text = "Суббота";
                return;
            case 6:
                weektext.text = "Воскресенье";
                return;
        }
    }

    public void SetActiveMainContainer(bool b) => mainContainer.SetActive(b);
    //public void StartDialog()
    //{
    //    //ConversationManager.Instance.StartConversation(Conversation);
    //    SetActiveMainContainer(false);
    //    activeButtonDialog(false);

    //    Debug.Log($"{gameObject.name} - 134");
    //    //Conversation = null;
    //}
    public void StartMinigame()
    {
        miniGame.SetActive(true);
        MiniGameController controller = miniGame.GetComponent<MiniGameController>();

        controller.parentNPC = curentStateNPC;
        controller.current = curentStateNPC.currentImg;

        controller.StartNewGame();
    }
    public void StartGame()
    {
        bottomGroup.SetActive(false);
        miniGameTrigger.SetActive(false);

        startGameBool = false;
    }
}