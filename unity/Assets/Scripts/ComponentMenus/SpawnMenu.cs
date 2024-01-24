using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMenu : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TMP_Text spawnFreqText;
    [SerializeField] private Slider spawnFreqSlider;

    private MoveSpawner moveSpawner;

    private void Awake()
    {
        closeButton.onClick.AddListener(() =>
        {
            DestroyImmediate(gameObject);
        });

        spawnFreqSlider.onValueChanged.AddListener(value =>
        {
            spawnFreqText.text = "Spawn Frequency:  < " + value.ToString("F1") + " >";
            moveSpawner.SetInfoText("< " + value.ToString("F1") + " >");
            moveSpawner.spawnFreq = value;
        });
    }

    public void Init(MoveSpawner moveSpawner, float spawnFreq)
    {
        this.moveSpawner = moveSpawner;
        spawnFreqSlider.onValueChanged.Invoke(spawnFreq);
        spawnFreqSlider.value = spawnFreq;
    }
}
