using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Note:
 * currently building get within Blender
 * this file is a place holder
 */

public class GunStuff : MonoBehaviour
{
    public Transform gun;
    public float xrot;
    public float yrot;
    public float zrot;
    public float xpos;
    public float ypos;
    public float zpos;
    public float xscale;
    public float yscale;
    public float zscale;
    
    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        xrot = gun.transform.rotation.x;
        yrot = gun.transform.rotation.y;
        zrot = gun.transform.rotation.z;
        xpos = gun.transform.position.x;
        ypos = gun.transform.position.y;
        zpos = gun.transform.position.z;
        xscale = gun.transform.localScale.x;
        yscale = gun.transform.localScale.y;
        zscale = gun.transform.localScale.z;
    }
}
