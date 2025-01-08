using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class ObjectUpDown : MonoBehaviour
{
    [ProButton]
    public void MoveToFirstSibling()
    {
        if (transform.parent == null)
        {
            Debug.LogWarning("This object does not have a parent.");
            return;
        }
        transform.SetSiblingIndex(0);
    }

    [ProButton]
    public void MoveToLastSibling()
    {
        if (transform.parent == null)
        {
            Debug.LogWarning("This object does not have a parent.");
            return;
        }
        transform.SetSiblingIndex(transform.parent.childCount - 1);
    }

    [ProButton]
    public void MoveUpInHierarchy()
    {
        if (transform.parent == null)
        {
            Debug.LogWarning("This object does not have a parent.");
            return;
        }

        int currentIndex = transform.GetSiblingIndex();
        if (currentIndex > 0)
        {
            transform.SetSiblingIndex(currentIndex - 1);
        }
    }

    [ProButton]
    public void MoveDownInHierarchy()
    {
        if (transform.parent == null)
        {
            Debug.LogWarning("This object does not have a parent.");
            return;
        }

        int currentIndex = transform.GetSiblingIndex();
        if (currentIndex < transform.parent.childCount - 1)
        {
            transform.SetSiblingIndex(currentIndex + 1);
        }
    }
}
