using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpCharacter : MonoBehaviour
{
    public float FireRadius = 5f;
    public LayerMask FireUpMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LightUp();
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, FireRadius);
    }

    private void LightUp()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, FireRadius, FireUpMask);
        for (int i = 0; i < colliders.Length; i++)
        {

            EnemyManager enemy = colliders[i].GetComponent<EnemyManager>();
            // DP_CharacterStats characterStats = colliders[i].transform.GetComponent<DP_CharacterStats>();
            if (enemy != null)
            {
                enemy.isFired = true;
            }
        }
    }
}
