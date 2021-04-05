using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboEffect : ParticlesHandler
{
    [SerializeField] private float lifeTime;

    private void SetPosition(Vector3 newPosition)
    {
        this.transform.position = newPosition;
    }

    public void PlayComboEffect(Vector3 finalPosition, Quaternion rotation)
    {
        SetPosition(finalPosition);
        this.transform.rotation = rotation;
        base.Play();

        StartCoroutine(SelfDeactivate());
    }

    private IEnumerator SelfDeactivate()
    {
        yield return new WaitForSeconds(lifeTime);
        this.gameObject.SetActive(false);
    }
}
