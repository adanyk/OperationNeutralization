using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject drawer;
    public GameObject origin;
    public GameObject destination;
    public bool itIsAMask;
    public bool gloves;
    public bool shoes;
    public float snapRange;
    public bool isOnAvatar = false;
    public bool isDragged = false;

    public Vector3 initialPosition;
    public Vector3 initialScale;

    private void Awake()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;
        snapRange = .5f;
    }

    private void Update()
    {
        if (isDragged && Input.GetMouseButtonUp(0))
        {
            Drop();
        }
        if (isDragged)
        {
            transform.position = new Vector3(Laser.laserEndPosition.x, Laser.laserEndPosition.y, transform.position.z);
        }
    }

    public void Drag()
    {
        isDragged = true;
        transform.position -= new Vector3(0, 0, .1f);
        if (gloves)
        {
            transform.localScale = new Vector3(initialScale.x * 2.5f, initialScale.y, initialScale.z);
        }
        else if (shoes)
        {
            transform.localScale = new Vector3(initialScale.x * 1.35f, initialScale.y, initialScale.z);
        }
    }

    private void Drop()
    {
        isDragged = false;
        transform.position += new Vector3(0, 0, .1f);
        var distance = Vector3.Distance(transform.position, destination.transform.position);
        Debug.Log(distance);
        if (distance < snapRange)
        {
            PutOn();
        }
        else
        {
            PutOff();
        }
    }

    private void PutOn()
    {
        if (itIsAMask)
        {
            Phaze1Control.PutOffOtherMask();
        }
        transform.position = destination.transform.position;
        isOnAvatar = true;
        Phaze1Control.IsAwatarFullyEquiped();
    }

    public void PutOff()
    {
        if (drawer.GetComponent<Drawer>().isOpen)
        {
            transform.position = origin.transform.position;
        }
        else
        {
            transform.position = initialPosition;
        }

        if (gloves || shoes)
        {
            transform.localScale = initialScale;
        }
        isOnAvatar = false;
    }
}
