using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject[] sword;
    [SerializeField] private Vector3[] boxOffset;
    [SerializeField] private Vector3[] boxSize;
    [SerializeField] private LayerMask layerToHit;

    private LookDirection lookDir;
    private PlayerAnimator playerAnim;
    public bool hasSword = false;
    void Start()
    {
        playerAnim = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        if(hasSword == false)
        {
            return;
        }
        SwingSword();
    }
    
    public void EnableSword()
    {
        hasSword = true;
    }
    private void SwingSword()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            playerAnim.SetAnimPlayerState(PlayerState.attacking);

            lookDir = playerAnim.GetDirection();
            
            int index = (int)lookDir;

            TurnSwordOff();

            sword[index].SetActive(true);
            
            Invoke("TurnSwordOff", 0.2f);

            RaycastHit2D hits2D = Physics2D.BoxCast(transform.position + boxOffset[index], boxSize[index], 0, Vector2.zero, 0, layerToHit);

            if (hits2D.collider != null)
            {
               hits2D.collider.GetComponent<EnemyHealth>().ChangeHealth(-1);
            }           
        }
    }

    private void TurnSwordOff()
    {
        for (int i = 0 ; i < sword.Length ; i++)
        sword[i].SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        if(sword.Length != boxOffset.Length && sword.Length != boxSize.Length)
        {
            return;
        }
        for (int i = 0; i < sword.Length; i++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(transform.position + boxOffset[i], boxSize[i]);
        }
    }
}
