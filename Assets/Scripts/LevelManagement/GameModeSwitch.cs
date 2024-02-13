using UnityEngine;


public class GameModeSwitch : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerInputDetector playerInputDetector;
    [SerializeField] private PlayerAnimatorHandler playerAnimator;
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
        playerAnimator.GameOver += BackToMainMenu;
    }


    private void OnDisable()
    {
        playerAnimator.GameOver -= BackToMainMenu;
    }


    private void SetEnabledFromGroup(GameObject[] objectGroup, bool value)
    {
        foreach(GameObject group in objectGroup)
            group.SetActive(value);
    }


    private void Transition()
    {
        player.position = new Vector3(0, 0, 0);
        SetEnabledFromGroup(showedGroupInMenu, true);
        playerAnimator.ResetAnimator();
        cameraSwitcher.Switch();
        loader.HideScreen();
    }


    public void BackToMainMenu()
    {
        loader.ShowScreen();
        SetEnabledFromGroup(showedGroupInGame, false);
        playerInputDetector.enabled = false;
        Timer.StartTimer(timeTransition, Transition);
    }


    public void HideMenu()
    {
        cameraSwitcher.Switch();
        SetEnabledFromGroup(showedGroupInMenu, false);
        SetEnabledFromGroup(showedGroupInGame, true);
        playerInputDetector.enabled = true;
    }
}