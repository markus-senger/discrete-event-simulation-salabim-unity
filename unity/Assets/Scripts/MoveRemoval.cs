using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRemoval : AbstractMoveable
{
    protected override void CalcOffsetToClosestConnectionPoint()
    {
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
