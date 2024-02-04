using System.Collections;
using UnityEngine;


public class LevelLoader : MonoBehaviour
{
    [SerializeField] private WalkerDungeonGenerator generator;
    [SerializeField] private Animator animator;
    [SerializeField] private DungeonSettingsForGenereting settingsForGenereting;


    private bool isGenerated = false;


    private void OnEnable()
    {
        generator.LevelGenereted += LevelCreated;
    }


    private void OnDisable()
    {
        generator.LevelGenereted -= LevelCreated;
    }


    private IEnumerator ShowLoadedScreen()
    {
        animator.SetTrigger("ShowLoaded");
        yield return new WaitUntil(() => isGenerated == true);
        animator.SetTrigger("HideLoaded");
    }


    private void LevelCreated()
    {
        isGenerated = true;
    }


    public void StartGenerate()
    {
        isGenerated = false;
        StartCoroutine("ShowLoadedScreen");
        generator.Generate(settingsForGenereting);
    }
}