using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MoveWT : AbstractMoveable
{
    protected override void CalcOffsetToClosestConnectionPoint()
    {
        float minDistance = float.MaxValue;
        GameObject nearestConveyor = null;

        foreach(Transform activeComponent in mainPanel.transform)
        {
            if(activeComponent.tag == "Conveyor" && activeComponent.GetComponent<AbstractMoveable>().isActive)
            {
                float curDistance = Vector3.Distance(Input.mousePosition, activeComponent.transform.position);
                if(curDistance < minDistance)
                {
                    minDistance = curDistance;
                    nearestConveyor = activeComponent.gameObject;
                }
            }
        }

        if(minDistance < snapRadius)
        {
            offsetY = nearestConveyor.transform.position.y - Input.mousePosition.y;
            offsetX = nearestConveyor.transform.position.x - Input.mousePosition.x;
        }
        if (Mathf.Abs(offsetX) > 50 || Mathf.Abs(offsetY) > 50 || minDistance >= snapRadius)
        {
            offsetX = 0; offsetY = 0;
        }
    }
}
