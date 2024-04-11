using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float timer = 0f;

	public void OnTriggerStay2D(Collider2D collision)
	{
        if (collision.gameObject.layer == 7)
        {
            timer += Time.deltaTime;

            if (timer > GameManager.instance.timeUntilGameOver)
            {
                GameManager.instance.GameOver();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 7)
		{
			timer = 0f;
		}
	}
}
