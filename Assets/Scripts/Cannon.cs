using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject knob;
    public Transform CannonTransform;

    public  Vector3 TargetDirection;
    public GameObject BulletPrefab;
    public Transform Muzzle;

    public float LaunchSpeed;

    
    
    // Start is called before the first frame update
    void Start()
    {
        TargetDirection = CannonTransform.transform.up;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CastARay();
        }
        
        
    }
    public void CastARay(){
        Ray screentoWorld=Camera.main.ScreenPointToRay(Input.mousePosition);


        Debug.DrawRay(screentoWorld.origin,screentoWorld.direction,Color.green);
        if(Physics.Raycast(screentoWorld,out RaycastHit hitInfo,100f,1<<LayerMask.NameToLayer("Interactable")))
        {
            if(hitInfo.collider){
                Vector3 PointOfCollision=hitInfo.point;
                knob.transform.position=PointOfCollision;
                TargetDirection=PointOfCollision-CannonTransform.position;
                StartCoroutine(FireBullet(TargetDirection));
                
               
                
            }
            
        }
    }
    public IEnumerator FireBullet(Vector3 shootdir)
    {
        Vector3 A=CannonTransform.up;
        Vector3 B=shootdir;
        float Speed=3f;
        float timer=0f;
        while(timer<=1)
        {
          CannonTransform.up=Vector3.Lerp(A,B,timer);
          timer += Time.deltaTime*Speed;
          yield return null;

        }
        var clonebullet=Instantiate(BulletPrefab,Muzzle.position,Quaternion.identity);
        var BulletRB =clonebullet.GetComponent<Rigidbody>();
        BulletRB.linearVelocity=shootdir.normalized*LaunchSpeed;
       
    }
    }
    

