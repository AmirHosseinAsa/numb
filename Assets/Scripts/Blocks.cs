using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blocks : MonoBehaviour {

    public Box[] blocks;

    Manager manager;

    float speed;
    void Start()
    {
        manager = GetComponentInParent<Manager>();
        speed = manager.speed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        StartCoroutine(DestroyAfterPeriod(7));
    }

    IEnumerator DestroyAfterPeriod(int timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);

    }

    public void GenerateRandomNumbers(List<int> num)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].GetComponentInChildren<Text>().text = num[i].ToString();
            blocks[i].myNumber = num[i];
        }
    }

    public void ChangeBoxColors(List<Color> c,int indiceOfRightCol,Color RightColorColor)
    {
        for (int i = 0; i < blocks.Length; i++)
        {

            blocks[i].GetComponent<Image>().color = ((indiceOfRightCol == i) ? RightColorColor : c[i]);

        }
    }

}
