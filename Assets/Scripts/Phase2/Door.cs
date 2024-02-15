using UnityEngine;

public class Door : MonoBehaviour
{
    public int threatLevel;
    public bool isDoorNeutralized = false;
    public bool isDoorOnFire = false;
    public bool hasBeenInitiallyMeasured = false;
    public bool hasBeenMeasuredAfterNeutralization = false;
    public bool tooStronglyShot = false;
    public bool tooWeaklyShot = false;


    void Start()
    {
        threatLevel = Random.Range(1, 10);
    }

    public void TryToNeutralizeDoor(int gunPower)
    {
        if (!isDoorNeutralized && !isDoorOnFire)
        {
            NeutralizeDoor(gunPower);
        }
    }

    private void NeutralizeDoor(int gunPower)
    {
        tooStronglyShot = gunPower > threatLevel;
        tooWeaklyShot = gunPower < threatLevel;
        isDoorNeutralized = true;
        threatLevel = 0;
        GetComponent<Renderer>().material.color += new Color(0, .196f, 0);
    }

    public void PutOnFire()
    {
        isDoorOnFire = true;
        transform.Find("Fire").gameObject.SetActive(true);
    }

    public bool IsDoorCorrectlyNeutralized()
    {
        return isDoorNeutralized && !tooStronglyShot && !tooWeaklyShot;
    }
}
