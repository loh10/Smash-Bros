using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expulsionPercentage : MonoBehaviour
{
    public float characterExpulsionPercentage;

    private void Start()
    {
        setCharacterExpulsionPercentage(0);
    }
    public float getCharacterExpulsionPercentage()
    {
        return characterExpulsionPercentage;
    }

    public void setCharacterExpulsionPercentage(float newPercentage)
    {
        characterExpulsionPercentage = newPercentage;
    }

    public void addCharacterExpulsionPercentage(float percentageToAdd)
    {
        characterExpulsionPercentage += percentageToAdd;

    }

}
