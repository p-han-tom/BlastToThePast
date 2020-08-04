using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewinder : MonoBehaviour
{
    // Rewind variables
    protected List<Vector3> rewindPositions = new List<Vector3>();
    protected int rewindIndex = 1;

    // Prefabs
    public GameObject afterimagePrefab;
    protected GameObject afterimage;

    protected void MoveAfterImage()
    {
        rewindPositions.Add(transform.position);

        // After 2 seconds, an afterimage will start tracking the player's previous position
        if (rewindPositions.Count > 120)
        {
            rewindIndex++;

            // Change animations for if player is moving or idle and rotate player
            if (rewindPositions[rewindIndex].x != rewindPositions[rewindIndex - 2].x || rewindPositions[rewindIndex].y != rewindPositions[rewindIndex - 2].y)
            {
                afterimage.GetComponent<Animator>().SetBool("moving", true);
                afterimage.transform.localRotation = (rewindPositions[rewindIndex].x < rewindPositions[rewindIndex - 2].x) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            }
            else
            {
                afterimage.GetComponent<Animator>().SetBool("moving", false);
                if (transform.position == afterimage.transform.position) afterimage.transform.localRotation = transform.localRotation;
            }
            afterimage.transform.position = rewindPositions[rewindIndex];
        }
    }

    public virtual void Rewind()
    {
        transform.position = rewindPositions[rewindIndex];

        // Reset rewind variables and teleport afterimage back to player
        rewindIndex = 1;
        rewindPositions.Clear();
        afterimage.transform.position = transform.position;
        afterimage.GetComponent<Animator>().Rebind();

        
    }

}
