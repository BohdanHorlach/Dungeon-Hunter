using UnityEngine;


public class GameModeSwitch : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerInputDetector playerInputDetector;
    [SerializeField] private PlayerAnimatorHandler playerAnimator;
    [SerializeField] private HealthHandler playerHealthHandler;
    [SerializeField] private LevelLoader loader;
    [SerializeField] private CameraSwitcher cameraSwitcher;
    [SerializeField] private float timeTransition;
    [SerializeField] private GameObject[] showedGroupInMenu;
    [SerializeField] private GameObject[] showedGroupInGame;


    private void Start()
    {
        SetEnabledFromGroup(showedGroupInGame, false);
        playerInputDetector.enabled = false;
    }


    private void OnEnable()
    {
        loader.ShowLoaded += HideUIMenu;
    }


    private void OnDisable()
    {
        playerAnimator.GameOver -= BackToMainMenu;
        loader.ShowLoaded -= HideUIMenu;
    }


    private void SetEnabledFromGroup(GameObject[] objectGroup, bool value)
    {
        foreach(GameObject group in objectGroup)
            group.SetActive(value);
    }


    private void HideUIMenu()
    {
        SetEnabledFromGroup(showedGroupInMenu, false);
    }


    private void TransitionToMenu()
    {
        player.position = new Vector3(0, 0, 0);
        SetEnabledFromGroup(showedGroupInGame, false);
        SetEnabledFromGroup(showedGroupInMenu, true);
        playerHealthHandler.Revive();
        playerAnimator.ResetAnimator();
        loader.HideScreen();
    }


    public void BackToMainMenu()
    {
        loader.ShowScreen();
        playerInputDetector.enabled = false;
        cameraSwitcher.Switch();
        Timer.StartTimer(timeTransition, TransitionToMenu);
    }


    public void SwitchGameMode()
    {
        cameraSwitcher.Switch();
        SetEnabledFromGroup(showedGroupInGame, true);
        playerInputDetector.enabled = true;
    }
}