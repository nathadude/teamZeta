
using UnityEngine;
using mixpanel;
using System.Collections;


public class PlayerDamage : MonoBehaviour
{
    public bool invicible;
    public BoolSO GameOver;
    public Animator PlayerAC;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            PlayerPrefsManager.IncreaseScore();
            if (invicible) return;

            // Mixpanel logging
            Value props = new Value();
            props["CauseOfDeath"] = collision.gameObject.name;
            props["LevelSegment"] = collision.transform.parent.name;
            Mixpanel.Track("Death", props);

            AudioManager.instance.Play("Death");
            AudioManager.instance.CrossfadeMusic("EndScreen", 0.5f);
            GameOver.value = true;
            PlayerAC.SetTrigger("Death");

            foreach(CapsuleCollider2D coll in GetComponents<CapsuleCollider2D>())
            {
                coll.enabled = false;
            }
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }
} 