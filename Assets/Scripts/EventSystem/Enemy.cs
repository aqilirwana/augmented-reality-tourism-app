using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackInterval = 2.0f;
    private float attackTimer;

    private void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            attackTimer = 0f;
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Player playerScript = player.GetComponent<Player>();
            playerScript.TakeDamage(attackDamage);
        }
    }
}
