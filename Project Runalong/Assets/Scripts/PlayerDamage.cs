
using UnityEngine;
using mixpanel;

public class PlayerDamage : MonoBehaviour
{
    public bool invicible;
    public BoolSO GameOver;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            if (invicible) return;
           
            // Mixpanel logging
            Value props = new Value();
            props["CauseOfDeath"] = collision.gameObject.name;
            props["LevelSegment"] = collision.transform.parent.name;
            Mixpanel.Track("Death", props);
            
            GameOver.value = true;
            Destroy(gameObject);
        }
    }
}
