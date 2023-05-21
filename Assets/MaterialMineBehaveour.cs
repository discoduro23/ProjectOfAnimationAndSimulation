using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMineBehaveour : MonoBehaviour
{
    public bool CanMine = true;

    public void Mine()
    {
        if (CanMine)
        {
            if (transform.localScale.x <= 0.5f && transform.localScale.y <= 0.5f && transform.localScale.z <= 0.5f)
            {
                Destroy(gameObject);

                // 1 MATERIAL MORE
            }
            else
            {
                CanMine = false;
                StartCoroutine(MineCoroutine());
            }

        }
    }

    IEnumerator MineCoroutine()
    {
        yield return new WaitForSeconds(1f);

        // Scale down the object gradually
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        CanMine = true;
    }
}
