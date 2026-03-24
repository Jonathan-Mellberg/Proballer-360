using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform eye;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        eye.LookAt(player);
    }
}
