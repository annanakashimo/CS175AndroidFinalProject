using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]public float attackCooldown;
    [SerializeField]public Transform firePoint;
    [SerializeField]public GameObject[] energyballs;
    public Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Z) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();
        
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        //pool fireballs
        energyballs[FindEnergyball()].transform.position = firePoint.position;
        energyballs[FindEnergyball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

    private int FindEnergyball()
    {
        for(int i = 0; i< energyballs.Length; i++){
            if(!energyballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
