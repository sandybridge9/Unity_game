using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float range = 100f;
    public LineRenderer laserLineRenderer;
    public float laserWidth = 0.1f;

    public Camera cam;
    public GameObject tipOfStaff;
    public ParticleSystem fireFlash;
    public ParticleSystem plasmaFlash;
    public GameObject fireHitEffect;
    public GameObject plasmaHitEffect;
    Player player;
    bool isLaserOn = false;

    float nextTimeToFire1 = 0f;
    float nextTimeToFire2 = 0f;

    void Start()
    {
        player = GetComponent<Player>();
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.startWidth = laserWidth;
        laserLineRenderer.endWidth = laserWidth;
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaserFromTargetPosition(tipOfStaff.transform.position, cam.transform.forward, range);
        if(Input.GetKeyDown(KeyCode.V))
        {
            if(isLaserOn == true)
            {
                isLaserOn = false;
            }
            else
            {
                isLaserOn = true;
            }
        }
        if(isLaserOn == true)
        {
            laserLineRenderer.enabled = true;
        }
        else
        {
            laserLineRenderer.enabled = false;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            ShootFire(); 
        }
        if (Input.GetButtonDown("Fire2"))
        {
            ShootPlasma();
        }
    }

    void ShootFire()
    {
        RaycastHit hit;
        if (Time.time >= nextTimeToFire1)
        {
            fireFlash.Play();
            if (Physics.Raycast(tipOfStaff.transform.position, cam.transform.forward, out hit, range))
            {
                Player player2 = hit.transform.GetComponent<Player>();
                if(player2 == null)
                {
                    Debug.Log(hit.transform.name);

                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if(enemy != null)
                    {
                        if(enemy.type == "Knight")
                        {
                            Debug.Log("Attack is highly ineffective against a hell knight");
                        }
                        else
                        {
                            enemy.TakeDamage(player.fireDamage);
                        }
                    
                    }
                    GameObject effect = Instantiate(fireHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(effect, 1f);
                }
            }
            nextTimeToFire1 = Time.time + 1f / player.rateOfFire1;
        }

    }

    void ShootPlasma()
    {
        
        RaycastHit hit;
        if (Time.time >= nextTimeToFire2)
        {
            plasmaFlash.Play();
            if (Physics.Raycast(tipOfStaff.transform.position, cam.transform.forward, out hit, range))
            {
                Player player2 = hit.transform.GetComponent<Player>();
                if (player2 == null)
                {
                    Debug.Log(hit.transform.name);

                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        if (enemy.type == "Knight")
                        {
                            enemy.TakeDamage(player.plasmaDamage * 2);
                        }
                        else
                        {
                            enemy.TakeDamage(player.plasmaDamage);
                        }
                    }
                    GameObject effect = Instantiate(plasmaHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(effect, 1f);
                }
            }
            nextTimeToFire2 = Time.time + 1f / player.rateOfFire2;
        }
    }

    void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
        }

        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);
    }

    public float getFireCD()
    {
        float r = nextTimeToFire1 - Time.time;
        if (r < 0)
        {
            r = 0;
        }
        return r;
    }

    public float getShockCD()
    {
        float r = nextTimeToFire2 - Time.time;
        if(r < 0)
        {
            r = 0;
        }
        return r;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CoinPickup")
        {
            player.AddGold();
            Destroy(other.gameObject);
        }
        else if(other.tag == "HpPickup")
        {
            player.Heal();
            Destroy(other.gameObject);
        }
    }
}
