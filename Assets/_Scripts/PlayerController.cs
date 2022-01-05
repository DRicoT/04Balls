
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody; 
    [SerializeField] private float moveForce;

    private GameObject focalPoint;
    
    public bool hasPowerUp;
    [SerializeField] private float powerUpForce;
    [SerializeField] private float powerUpTime;

    [SerializeField] private GameObject[] powerUpIndicators;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(focalPoint.transform.forward * moveForce * forwardInput, ForceMode.Force);

        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.gameObject.transform.position = this.transform.position + (0.5f * Vector3.down);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDown());
        }else if (other.CompareTag("KillZone"))
        {
            SceneManager.LoadScene("Prototype 4");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayDirection = collision.gameObject.transform.position - this.transform.position;
            enemyRigidBody.AddForce(awayDirection * powerUpForce, ForceMode.Impulse); 
            
            //Debug.Log("El jugador ha chocado contra "+collision.gameObject+" teniendo el Power Up a "+hasPowerUp);
        }
        
    }

    /// <summary>
    /// For every Power Indicator, activates it, wait powerUpTime, and desactivates it
    /// </summary>
    /// <returns></returns>
    IEnumerator PowerUpCountDown()
    {
        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(powerUpTime/powerUpIndicators.Length);
            indicator.gameObject.SetActive(false);
        }
        hasPowerUp = false;

    }
}
