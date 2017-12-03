using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRT : MonoBehaviour {

	public RenderTexture rt;
	public RawImage rawImage;
	public Material imagemat;

	void Start() {
		rt = new RenderTexture(Screen.width, Screen.height, 32);
		rt.name = "Whatever";
		rt.enableRandomWrite = true;
		rt.Create();
		this.GetComponent<Camera>().targetTexture = rt;
		rawImage.texture = rt;
	}

	// Update is called once per frame
	void Update () {
		RenderTexture.active = rt;
		GL.Clear(true, true, Color.black);
		Graphics.Blit(rt, imagemat);
	}
}
