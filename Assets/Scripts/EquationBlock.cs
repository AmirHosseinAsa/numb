using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquationBlock : MonoBehaviour {

   public Text text;

   float speed;
   Manager manager;
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

   public void ChangeText(string equation)
   {
       text.text = equation;
   }

   public void ChangeBackGroundColor(Color c)
   {
       GetComponentInChildren<Text>().color = c;
   }

}
