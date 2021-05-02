using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject firePoint;


    public Transform Target { get; set; }

    [SerializeField]//чтоб видеть в инспекторе
    private Transform rotaror;
    public Transform Rotaror { get => rotaror; set => rotaror = value; }

    [SerializeField]//дублировать чтоб в жопу не ебатся
    private Transform ghostRotator;
    public Transform GhostRotator { get => ghostRotator; set => ghostRotator = value; }

    [SerializeField]//скорость вращения
    private float rotationSpeed =300f;
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

    [SerializeField]
    private Transform gunBarrel;
    public Transform GunBarrel { get => gunBarrel; set => gunBarrel = value; }

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }

    public Quaternion DefaultRotation { get; set; }
    

    public GameObject effectToSpawn;

    public LayerMask layerMask;

    protected TurretState currState;

    private void Start()
    {
        DefaultRotation = rotaror.rotation;
        ChangeState(new TurretStateIdle());
    }

    private void Update()
    {
        currState.Update();
        //Debug.DrawRay(rotaror.position,gunBarrel.forward,Color.red);
    }

    public bool SeeChecker(Vector3 dir,Vector3 origin,string tag)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin,dir,out hit,Mathf.Infinity,layerMask))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.tag == tag)
            {
                return true;
            }
        }
        return false;
    }

    public void Shoot(int index)
    {
        Debug.Log(index);
        Quaternion headingDirection = Quaternion.FromToRotation(projectile.transform.forward, gunBarrel.forward);
        Instantiate(projectile, GunBarrel.position, headingDirection).GetComponent<ProjectileScript>().Direction = gunBarrel.forward;
    }


    public void ChangeState(TurretState newState)
    {
        if (newState != null)
        {
            newState.Exit();
        }
        this.currState = newState;
        newState.Enter(this);
    }

    private void OnTriggerEnter(Collider col)
    {
        currState.OnTriggerEnter(col);
    }

    private void OnTriggerExit(Collider col)
    {
        currState.OnTriggerExit(col);
    }
}
