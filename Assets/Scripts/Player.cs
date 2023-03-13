using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Manager manager;
    public GameObject starEffect;

    int numberTaken = 0;
    void OnTriggerEnter2D(Collider2D col)
    {
        numberTaken = col.GetComponent<Box>().myNumber;
        if (numberTaken == manager.rightAnswer)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject o = col.transform.parent.GetChild(i).gameObject;
                if (col.gameObject != o) o.SetActive(false);

            }
            manager.IncrementScore(col.gameObject);
            Destroy(Instantiate(starEffect, transform.position, Quaternion.identity), 1);
        }
        else manager.Lose(this.gameObject, transform.position);
    }

}
