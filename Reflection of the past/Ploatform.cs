using System;
using UnityEngine;

public class Ploatform : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Vector3 origin;
    private AudioSource audioSource;
    
    [SerializeField] private float time;
    [SerializeField] private LayerMask whatIsPlayer;

    [SerializeField] private Color greenColor;
    [SerializeField] private Color redColor;

    public Light PointLight;
    [SerializeField] private bool isMoving;
    [SerializeField] private Vector3 realTarget;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        origin = transform.position;
        
        PointLight.color = redColor; 
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, realTarget, Time.deltaTime * time);
            
            if (Vector3.Distance(transform.position, realTarget) < 0.1f)
            {
                transform.position = realTarget;
                PointLight.color = redColor;
                
                Debug.Log("도착");
                
                isMoving = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((whatIsPlayer & (1 << other.gameObject.layer)) != 0)
        {
            isMoving = true;
            other.gameObject.transform.SetParent(transform);
            
            realTarget = GameManager.CurrentState == GameState.A ? target.position : origin;
            print(GameManager.CurrentState == GameState.A);    
                        
            PointLight.color = greenColor; 
            audioSource.Play(); 
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if ((whatIsPlayer & (1 << other.gameObject.layer)) != 0)
        {
            isMoving = false;
            other.gameObject.transform.SetParent(null);
            PointLight.color = redColor;
        }
    }
}