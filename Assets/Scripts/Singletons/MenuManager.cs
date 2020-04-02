using UnityEngine;

public enum MenuStates
{
    Main,
    Game,
    Pause,
    LevelSelect
}

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField]
    MenuStates startState;

    MenuStates currentMenuState;

    // (Optional) Prevent non-singleton constructor use.
    protected MenuManager() { }

    private void Awake()
    {
        ChangeMenuState(startState);
    }

    void ChangeMenuState(MenuStates newMenuState)
    {
        switch(newMenuState)
        {
            case MenuStates.Main:
            case MenuStates.LevelSelect:
            case MenuStates.Game:
                SetAllMenusTo(false);

                SetMenuTo(newMenuState, true);

                currentMenuState = newMenuState;
                break;
            case MenuStates.Pause:
                if(currentMenuState == MenuStates.Game)
                {
                    SetMenuTo(newMenuState, true);

                    currentMenuState = newMenuState;
                }
                else if(currentMenuState == MenuStates.Pause)
                {
                    SetMenuTo(newMenuState, false);

                    currentMenuState = MenuStates.Game;
                }
                break;
        }
    }

    void SetAllMenusTo(bool isActive)
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(isActive);
        }
    }

    void SetMenuTo(MenuStates menu, bool isActive)
    {
        if((int)menu < transform.childCount)
        {
            transform.GetChild((int)menu).gameObject.SetActive(isActive);
        }
    }

    public void OnGoToMain()
    {
        ChangeMenuState(MenuStates.Main);
    }

    public void OnGoToGame()
    {
        ChangeMenuState(MenuStates.Game);
    }

    public void OnGoToLevelSelect()
    {
        ChangeMenuState(MenuStates.LevelSelect);
    }

    public void OnPause()
    {
        ChangeMenuState(MenuStates.Pause);
    }
}