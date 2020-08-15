using UnityEngine;
using System.Collections;

public class itweenTO : MonoBehaviour {
    public Vector3 End;
    public bool isgo = true;
    Hashtable args = new Hashtable();
	// Use this for initialization
    void Start()
    {
        
        args.Add( "time", 3);
        args.Add("islocal",true);
        args.Add("position", End);
        
      
    }
	
	// Update is called once per frame
	void Update () {
        if (isgo==true)
        {
            isgo = false;
            iTween.MoveTo(this.gameObject, args);
        }
	}
}
