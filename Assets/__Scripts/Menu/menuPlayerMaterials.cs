using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuPlayerMaterials : MonoBehaviour
{
    public Material[] materials1;
    public Material[] materials2;

    public addMaterialsToPlayerInMenu addMaterialsToPlayerInMenuInstance;

    private int currentMaterialIndex = 0;

    public void NextMaterial()
    {
        
        currentMaterialIndex++;
        LogWriter.WriteLog("NextMaterial() called: " + currentMaterialIndex);
        if (currentMaterialIndex >= materials1.Length)
        {
            currentMaterialIndex = 0;
        }
        addMaterialsToPlayerInMenuInstance.mat3 = materials1[currentMaterialIndex];
        addMaterialsToPlayerInMenuInstance.mat2 = materials2[currentMaterialIndex];
        addMaterialsToPlayerInMenuInstance.Paint();
    }
}
