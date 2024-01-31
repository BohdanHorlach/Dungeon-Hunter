using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class ExperienceCounter : MonoBehaviour
{
    [SerializeField] private CharacterCharacteristics player;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField, Min(0.1f)] private float speedTransition = 0.5f;


    private void Awake()
    {
        counter.SetText(player.Exp.ToString());
    }


    private void OnEnable()
    {
        player.ExpChanged += StartTransition;
    }


    private void OnDisable()
    {
        player.ExpChanged -= StartTransition;
    }


    private void StartTransition(int exp)
    {
        StartCoroutine(UpdateSlider(exp));
    }


    private IEnumerator UpdateSlider(int newExp)
    {

        float elapsed = 0.0f;
        int startExp = Convert.ToInt32(counter.text);

        while (elapsed < speedTransition)
        {
            float duration = elapsed / speedTransition;
            elapsed += Time.deltaTime;
            counter.SetText(Convert.ToInt32(Mathf.Lerp(startExp, newExp, duration)).ToString());
            yield return null;
        }

        counter.SetText(newExp.ToString());
    }
}