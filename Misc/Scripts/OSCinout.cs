using UnityEngine;
using System.Collections;

public class OSCinout : MonoBehaviour {
	
	public string RemoteIP = "127.0.0.1";
	public int SendToPort = 9000;
	public int ListenerPort = 8000;
	private Osc handler;
	string allColors = "";
	string oldAllColors = "";
	
	public GameObject ballInstance;
	public GameObject boxInstance;
	
	public GameObject soundObject1;
	public GameObject soundObject2;
	public GameObject soundObject3;
	public GameObject soundObject4;
	public GameObject soundObject5;
	public GameObject soundObject6;
	
	public GameObject Lamp;
	
	
	public bool s1Play = false;
	public bool s2Play = false;
	public bool s3Play = false;
	public bool s4Play = false;
	public bool s5Play = false;
	public bool s6Play = false;
	
	// Use this for initialization
	void Start () {

		UDPPacketIO udp = GetComponent<UDPPacketIO>();
		
		udp.init(RemoteIP, SendToPort, ListenerPort);
		handler = GetComponent<Osc>();
		handler.init(udp);
		
		handler.SetAddressHandler("/sensor/1", sensor1);
		handler.SetAddressHandler("/sensor/2", sensor2);
		handler.SetAddressHandler("/sensor/3", sensor3);
		handler.SetAddressHandler("/sensor/4", sensor4);
		handler.SetAddressHandler("/sensor/5", sensor5);
		handler.SetAddressHandler("/sensor/6", sensor6);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(s1Play){
			soundObject1.GetComponent<AudioSource>().Play();
			s1Play = false;
			//Instantiate(boxInstance, transform.position, transform.localRotation);
			GameObject clone;
            clone = Instantiate(boxInstance, soundObject1.transform.position, soundObject1.transform.localRotation) as GameObject;
			Color myColor = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), 0.5f); 
			clone.GetComponent<panelColorer>().myColor = myColor;
			clone.GetComponent<panelColorer>().emisColor = myColor;
         
		}
		if(s2Play){
			soundObject2.GetComponent<AudioSource>().Play();
			s2Play = false;
			GameObject clone;
            clone = Instantiate(boxInstance, soundObject2.transform.position, soundObject2.transform.localRotation) as GameObject;
			Color myColor = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), 0.5f); 
			clone.GetComponent<panelColorer>().myColor = myColor;
			clone.GetComponent<panelColorer>().emisColor = myColor;
		}
		if(s3Play){
			soundObject3.GetComponent<AudioSource>().Play();
			s3Play = false;
			GameObject clone;
            clone = Instantiate(boxInstance, soundObject3.transform.position, soundObject3.transform.localRotation) as GameObject;
			Color myColor = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), 0.5f); 
			clone.GetComponent<panelColorer>().myColor = myColor;
			clone.GetComponent<panelColorer>().emisColor = myColor;
		}
		if(s4Play){
			soundObject4.GetComponent<AudioSource>().Play();
			s4Play = false;
			GameObject clone;
            clone = Instantiate(boxInstance, soundObject4.transform.position, soundObject4.transform.localRotation) as GameObject;
			Color myColor = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), 0.5f); 
			clone.GetComponent<panelColorer>().myColor = myColor;
			clone.GetComponent<panelColorer>().emisColor = myColor;
		}
		if(s5Play){
			soundObject5.GetComponent<AudioSource>().Play();
			s5Play = false;
			GameObject clone;
            clone = Instantiate(boxInstance, soundObject5.transform.position, soundObject5.transform.localRotation) as GameObject;
			Color myColor = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), 0.5f); 
			clone.GetComponent<panelColorer>().myColor = myColor;
			clone.GetComponent<panelColorer>().emisColor = myColor;
		}
		if(s6Play){
			soundObject6.GetComponent<AudioSource>().Play();
			s6Play = false;
			GameObject clone;
            clone = Instantiate(boxInstance, soundObject6.transform.position, soundObject6.transform.localRotation) as GameObject;
			Color myColor = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), 0.5f); 
			clone.GetComponent<panelColorer>().myColor = myColor;
			clone.GetComponent<panelColorer>().emisColor = myColor;
		}
		
		
				/*
				OscMessage oscM = Osc.StringToOscMessage("/vibrate " + 1);
				handler.Send(oscM); 
				*/
				
		//OSC MESSAGE
		allColors = "";
		allColors += Lamp.GetComponent<LEDGatherer>().lampNumber + " ";
		
		for(int i = 0; i < Lamp.GetComponent<LEDGatherer>().LEDAmount;i++){
		//Debug.Log(Lamp.GetComponent<LEDGatherer>().LEDColors[i].r);
			allColors += (int)(Lamp.GetComponent<LEDGatherer>().LEDColors[i].r*255) + " ";
			
			allColors += (int)(Lamp.GetComponent<LEDGatherer>().LEDColors[i].g*255) + " ";
			allColors += (int)(Lamp.GetComponent<LEDGatherer>().LEDColors[i].b*255) + " ";
			
		}
		
		if(oldAllColors != allColors){
		OscMessage oscM = Osc.StringToOscMessage(allColors);
        handler.Send(oscM);  
			//Debug.Log(allColors);
		
		}
		oldAllColors = allColors;
		
	}
	
	
	

	public void sensor1(OscMessage oscMessage){
		s1Play = true;
		Debug.Log("sensor1");	
	}
	public void sensor2(OscMessage oscMessage){
		s2Play = true;
		Debug.Log("sensor2");
	}
	public void sensor3(OscMessage oscMessage){
		s3Play = true;
		Debug.Log("sensor3");
	}
	public void sensor4(OscMessage oscMessage){
		s4Play = true;
		Debug.Log("sensor4");
	}
	public void sensor5(OscMessage oscMessage){
		s5Play = true;
		Debug.Log("sensor5");
	}
	public void sensor6(OscMessage oscMessage){
		s6Play = true;
		Debug.Log("sensor6");
	}
	
	
}

