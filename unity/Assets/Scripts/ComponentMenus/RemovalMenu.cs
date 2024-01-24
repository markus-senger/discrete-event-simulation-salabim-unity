using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemovalMenu : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TMP_Text durationText;
    [SerializeField] private Slider durationSlider;

    private MoveRemoval moveRemoval;

    private void Awake()
    {
        closeButton.onClick.AddListener(() =>
        {
            DestroyImmediate(gameObject);
        });

        durationSlider.onValueChanged.AddListener(value =>
        {
            durationText.text = "Removal duration:  < " + value.ToString("F1") + " >";
            moveRemoval.SetInfoText("< " + value.ToString("F1") + " >");
            moveRemoval.removalDuration = value;
        });
    }

    public void Init(MoveRemoval moveRemoval, float spawnFreq)
    {
        this.moveRemoval = moveRemoval;
        durationSlider.onValueChanged.Invoke(spawnFreq);
        durationSlider.value = spawnFreq;
    }
}
