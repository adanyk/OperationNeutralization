using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float interactionDistance;
    public GameObject[] interactionTexts;
    public string doorOpenAnimationName, doorCloseAnimationName;
    public GameObject gun;
    public GameObject gauge;
    public GameObject phase2Control;

    private int measurement = 0;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance) && hit.collider.gameObject.CompareTag("door"))
        {
            SetInteractionTexts(true);
            Interact(hit);
        }
        else
        {
            SetInteractionTexts(false);
            gauge.GetComponent<Gauge>().DisplayMeasurement();
            measurement = 0;
        }
    }

    private void SetInteractionTexts(bool set)
    {
        interactionTexts[0].SetActive(set);
        interactionTexts[1].SetActive(set);
    }

    private void Interact(RaycastHit hit)
    {
        var obj = hit.collider.gameObject;
        if (Input.GetKeyDown(KeyCode.O) && obj.GetComponent<Door>().isDoorOnFire == false)
        {
            TryToOpenOrClose(hit);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Measure(obj);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Shoot(obj);
        }
    }

    private void TryToOpenOrClose(RaycastHit hit)
    {
        GameObject doorParent = hit.collider.transform.root.gameObject;
        Animator doorAnim = doorParent.GetComponent<Animator>();

        if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnimationName))
        {
            doorAnim.ResetTrigger("open");
            doorAnim.SetTrigger("close");
        }
        else
        {
            var obj = hit.collider.gameObject;
            if (CanOpenSafely(obj) == false)
            {
                obj.GetComponent<Door>().PutOnFire();
                phase2Control.GetComponent<Phase2Control>().LoseLife();
            }

            doorAnim.ResetTrigger("close");
            doorAnim.SetTrigger("open");
        }
    }

    private bool CanOpenSafely(GameObject obj)
    {
        return obj.GetComponent<Door>().isDoorNeutralized &&
            obj.GetComponent<Door>().hasBeenMeasuredAfterNeutralization &&
            measurement == 0;
    }

    private void Measure(GameObject obj)
    {
        measurement = obj.GetComponent<Door>().threatLevel;
        gauge.GetComponent<Gauge>().DisplayMeasurement(measurement);

        if (obj.GetComponent<Door>().isDoorNeutralized)
        {
            obj.GetComponent<Door>().hasBeenMeasuredAfterNeutralization = true;
        }
    }

    private void Shoot(GameObject obj)
    {
        obj.GetComponent<Door>().TryToNeutralizeDoor(gun.GetComponent<GunRegulation>().gunPower);
    }
}
