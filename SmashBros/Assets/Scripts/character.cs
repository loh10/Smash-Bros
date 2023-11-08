using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    private expulsionPercentage _expulsionPercentage;
    private Rigidbody2D rb;
    private int powerOffset = 1;

    void Start()
    {
        _expulsionPercentage = GetComponent<expulsionPercentage>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 v = new Vector3(100, 100, 0);
            hitReceveid(1, v);
        }
    }



//il faut juste créer la fonction qui détécte la collision du hit et appelé ça
    private void sendHit(character ennemieHitted, Vector3 hitDirection)
    {
        ennemieHitted.hitReceveid(powerOffset, hitDirection);
    }

    public void hitReceveid(int _powerOffset, Vector3 hitDirection)
    {
        int hitPercentage = 0;
        switch (_powerOffset)
        {
            default:
            hitPercentage = Random.Range(1,3);
            break;

            case 1:
            hitPercentage = Random.Range(1,3);
            break;

            case 2:
            hitPercentage = Random.Range(4,7);
            break;
        }


        _expulsionPercentage.addCharacterExpulsionPercentage(hitPercentage);
        addForceByhit(_powerOffset, hitDirection);
    }

    private void addForceByhit(int _powerOffset, Vector3 hitDirection)
    {
        rb.AddForce(hitDirection * (((_powerOffset * _expulsionPercentage.getCharacterExpulsionPercentage()) / 20)), ForceMode2D.Force);
        Debug.Log(((_powerOffset * _expulsionPercentage.getCharacterExpulsionPercentage()) / 10));
    }

}
