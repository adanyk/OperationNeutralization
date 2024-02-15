using UnityEngine;

public class Selection : MonoBehaviour
{
    public static Transform highlight;
    public static bool isObjectPicked = false;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isObjectPicked = false;
        }
        if (!isObjectPicked && Input.GetMouseButtonDown(0) && highlight != null)
        {
            HandleSelection();
        }
    }

    private void FixedUpdate()
    {
        if (!isObjectPicked && Physics.Raycast(Laser.laserRay, out var raycastHit))
        {
            Highlight(raycastHit);
        }
    }

    private void HandleSelection()
    {
        if (highlight.gameObject.TryGetComponent<Drawer>(out var drawer))
        {
            HandleDrawer(drawer);
        }
        else if (highlight.gameObject.TryGetComponent<DragAndDrop>(out var item))
        {
            isObjectPicked = true;
            item.Drag();
        }
    }

    private void HandleDrawer(Drawer drawer)
    {
        if (drawer.isOpen)
        {
            drawer.Close();
        }
        else
        {
            drawer.Open();
        }
    }

    private void Highlight(RaycastHit raycastHit)
    {
        var newHighlight = raycastHit.transform;
        if (newHighlight != highlight)
        {
            DisablePreviousHighlight();
            highlight = newHighlight;
        }

        if (highlight.CompareTag("Selectable"))
        {
            if (highlight.gameObject.GetComponent<Outline>() == null)
            {
                var outline = highlight.gameObject.AddComponent<Outline>();
                outline.enabled = true;
                outline.OutlineMode = Outline.Mode.OutlineVisible;
                highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
            }
            else
            {
                highlight.gameObject.GetComponent<Outline>().enabled = true;
            }
        }
        else
        {
            highlight = null;
        }
    }

    private void DisablePreviousHighlight()
    {
        if (highlight != null && highlight.gameObject.TryGetComponent(out Outline outline))
        {
            outline.enabled = false;
        }
    }
}
