using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform launchPoint;
    public Transform Target;

    [SerializeField]
    private float BallSpeed = 1000.0f;

    [SerializeField]
    public GameObject Ball;

    [SerializeField]
    public float countdown = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void fire()
    {
        GameObject SpawnBall = Instantiate(Ball, launchPoint.position, launchPoint.rotation);
        Rigidbody2D rbBall = SpawnBall.GetComponent<Rigidbody2D>();
        if (Ball != null)
        {
            Vector3 dirToTarget = Target.position - rbBall.position;
            dirToTarget.Normalize();

            rbBall.velocity = dirToTarget * BallSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            fire();
            countdown = 5f;
        }
    }
}
