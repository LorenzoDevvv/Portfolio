using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;

    private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    private List<Material> originalMaterials = new List<Material>();
    private Coroutine flashRoutine;

    private void Awake()
    {
        // Get all MeshRenderer components in the object hierarchy.
        GetMeshRenderers(transform);
    }

    private void GetMeshRenderers(Transform parent)
    {
        // Add MeshRenderers in the current object to the list.
        MeshRenderer[] renderers = parent.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            meshRenderers.Add(renderer);
            originalMaterials.Add(renderer.material);
        }

        // Recursively call GetMeshRenderers on each child object.
        for (int i = 0; i < parent.childCount; i++)
        {
            GetMeshRenderers(parent.GetChild(i));
        }
    }

    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        // Swap to the flashMaterial for each MeshRenderer.
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.material = flashMaterial;
        }

        yield return new WaitForSeconds(duration);

        // Restore the original material for each MeshRenderer.
        for (int i = 0; i < meshRenderers.Count; i++)
        {
            meshRenderers[i].material = originalMaterials[i];
        }

        flashRoutine = null;
    }
}
