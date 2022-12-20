using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugFood : MonoBehaviour
{
    [SerializeField] private Transform bugNest;

    private void OnEnable()
    {
        for (int i = 0; i < bugNest.childCount; i++)
        {
            BugMovement bugMovement;
            if (bugNest.GetChild(i).TryGetComponent(out bugMovement))
            {
                bugMovement.GoingToEat(transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BugMovement bugMovement;
        if(other.TryGetComponent(out bugMovement))
            StartCoroutine(DestroyFood(bugMovement));
    }

    IEnumerator DestroyFood(BugMovement bug)
    {
        yield return new WaitForSeconds(10f);
        bug.FinishEating();
        gameObject.SetActive(false);
    }

}
