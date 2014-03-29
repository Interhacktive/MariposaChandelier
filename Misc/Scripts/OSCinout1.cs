using UnityEngine;
using System.Collections;

public class OSCinout1 : MonoBehaviour {
	public int PlayerNum = 0;
	bool vibratable = false;
	bool vibrate = false;
	bool horizontal = true;
	public bool menuControl = false;
	public string RemoteIP = "127.0.0.1";
	public int SendToPort = 9000;
	public int ListenerPort = 8000;
	private Osc handler;
	private float adjusted = 0.0f;
	public Vector3 XYZ;
	Vector2 aimXY = new Vector2(0,0);
	bool shooting = true;
	public bool phoneControl = false;

	
	// Use this for initialization
	void Start () {

		UDPPacketIO udp = GetComponent<UDPPacketIO>();
		
		udp.init(RemoteIP, SendToPort, ListenerPort);
		handler = GetComponent<Osc>();
		handler.init(udp);

		handler.SetAddressHandler("/accxyz", iPhone);
		handler.SetAddressHandler("/3/xy", iPhoneXY);
		handler.SetAddressHandler("/acc", Android);
		handler.SetAddressHandler("/turbo", turbo);
		handler.SetAddressHandler("/sensor/1", drop);
		
		//PHONE MODE
		if(PlayerPrefs.GetInt("controllerMode") == 1){
			phoneControl = true;
		}
	
		if(PlayerPrefs.GetInt("phoneMode") == 0){
			Debug.Log("Phone Mode Horizontal");
			horizontal = true;
		}
		if(PlayerPrefs.GetInt("phoneMode") == 1){
			Debug.Log("Phone Mode Vertical");
			horizontal = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt("controllerMode") == 1){
			phoneControl = true;
		} else {
		phoneControl = false;	
		}
		
		if(phoneControl){
	//		LevelControls levelControls = GetComponent<LevelControls>();
			
			if(vibrate && vibratable){
				OscMessage oscM = Osc.StringToOscMessage("/vibrate " + 1);
				handler.Send(oscM); 
				vibrate = false;
			}
			 
			if(horizontal){
				adjusted = ( XYZ.z)*-1;
				if(PlayerNum == 0){
		//			levelControls.tilt = new Vector2(-XYZ.y,adjusted);
				}
				if(PlayerNum == 1){
				//	levelControls.aimXY = new Vector2(-XYZ.y,adjusted);
				}
			} else {
				if(PlayerNum == 0){
					//levelControls.tilt = new Vector2(XYZ.x,XYZ.y);
				}
				if(PlayerNum == 1){
					//levelControls.aimXY = new Vector2(XYZ.x,XYZ.y);
				}
			}
		}
	}
	
	
	
	public void iPhone(OscMessage oscMessage){
		this.XYZ = new Vector3((float)oscMessage.Values[0], (float)oscMessage.Values[1], (float)oscMessage.Values[2]);	
	
	} 
	public void iPhoneXY(OscMessage oscMessage){
		//aimXY =  Vector2((oscMessage.Values[0]*2)-1, (oscMessage.Values[1]*2)-1);
		//Debug.Log(aimXY);	
		shooting = true;
	} 

	public void Android(OscMessage oscMessage){
		XYZ = new Vector3((float)oscMessage.Values[0]/-10, (float)oscMessage.Values[1]/-10, (float)oscMessage.Values[2]/10);	
	} 


	public void turbo(OscMessage oscMessage){
		Debug.Log("turbo engaged!!!!");
		//turboMode = true;	
	} 

	public void drop(OscMessage oscMessage){
		Debug.Log("DROPPING!!");
	} 
	
}

