using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expulsionPercentage : MonoBehaviour
{
    private int characterExpulsionPercentage = 75;

    public int getCharacterExpulsionPercentage()
    {
        return characterExpulsionPercentage;
    }

    public void setCharacterExpulsionPercentage(int newPercentage)
    {
        characterExpulsionPercentage = newPercentage;
    }

    public void addCharacterExpulsionPercentage(int percentageToAdd)
    {
        characterExpulsionPercentage += percentageToAdd;
    }

}
