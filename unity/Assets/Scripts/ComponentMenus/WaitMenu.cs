using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitMenu : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TMP_Text waitDurationText;
    [SerializeField] private Slider waitDurationSlider;

    private MoveWait moveWait;

    private void Awake()
    {
        closeButton.onClick.AddListener(() =>
        {
            DestroyImmediate(gameObject);
        });

        waitDurationSlider.onValueChanged.AddListener(value =>
        {
            waitDurationText.text = "Wait duration:  < " + value.ToString("F1") + " >";
            moveWait.SetInfoText("< " + value.ToString("F1") + " >");
            moveWait.waitDuration = value;
        });
    }

    public void Init(MoveWait moveWait, float spawnFreq)
    {
        this.moveWait = moveWait;
        waitDurationSlider.onValueChanged.Invoke(spawnFreq);
        waitDurationSlider.value = spawnFreq;
    }
}
