using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveSpawner : AbstractMoveable
{
    [SerializeField] private GameObject componentMenu;
    [SerializeField] private TMP_Text infoText;

    public float spawnFreq { get; set; } = 1f;

    protected override void OpenComponentMenu()
    {
        componentReference.curComponentMenu = Instantiate(componentMenu, mainPanel.parent);
        componentReference.curComponentMenu.GetComponent<SpawnMenu>().Init(this, spawnFreq);
    }

    public void SetInfoText(string text)
    {
        infoText.text = text;
    }

    protected override void CalcOffsetToClosestConnectionPoint()
    {
        SetInfoText("< " + spawnFreq.ToString("F1") + " >");
        float minDistance = float.MaxValue;
        GameObject nearestConveyor = null;

        foreach (Transform activeComponent in mainPanel.transform)
        {
            if (activeComponent.tag == "Conveyor" && activeComponent.GetComponent<AbstractMoveable>().isActive)
            {
                float curDistance = Vector3.Distance(Input.mousePosition, activeComponent.transform.position);
                if (curDistance < minDistance)
                {
                    minDistance = curDistance;
                    nearestConveyor = activeComponent.gameObject;
                }
            }
        }

        if (minDistance < snapRadius)
        {
            transform.rotation = Quaternion.Euler(0, 0, (nearestConveyor.transform.rotation.eulerAngles.z == 0 || nearestConveyor.transform.rotation.eulerAngles.z == 180) ? 90f : 0f);
            offsetY = nearestConveyor.transform.position.y - Input.mousePosition.y;
            offsetX = nearestConveyor.transform.position.x - Input.mousePosition.x;
        }
        if (Mathf.Abs(offsetX) > 50 || Mathf.Abs(offsetY) > 50 || minDistance >= snapRadius)
        {
            offsetX = 0; offsetY = 0;
        }
    }
}
