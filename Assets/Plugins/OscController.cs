using MakingThings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The OscController manages a UDP session with a an OSC device.
// By default, the session is closed when a new scene is loaded, 
// however you can use unity's "DontDestroyOnLoad" semantics to
// maintain one across several scenes (but you'll need to do your
// own bookkeeping).

// Events are delegated as idiomatic Unity Messages, so in script
// just implement the given method:
// 
// function OnOscMessage(msg) { ... }
// 
// This message is sent to an gameobject whose path matches the path
// of the message, as well as an optional delegate that can be set
// explicitly.

public class OscController : MonoBehaviour {

	// Designter Parameters
	public string host = "127.0.0.1";
	public int remotePort = 7000;
	public int localPort = 7001;
	public Transform messageDelegate;

	// Osc Session Objects
	UdpPacket io;
	Osc osc;

	// Producer-Consumer Queue
	Queue<OscMessage> producerQueue = new Queue<OscMessage>();
	Queue<OscMessage> consumerQueue = new Queue<OscMessage> ();

	// Getters (in case you want to *send* an OSC message)
	public Osc Client { get { return osc; } }

	// An optional vanilla C# delegate for doing more advanced event handling
	public event OscMessageHandler onMessage;

	// Messages can be made and dispatched locally for testing, as well as
	// a side-effect of the client connection.
	public void Dispatch(OscMessage msg) {
		
		// dispatch as a normal event
		if (onMessage != null) { onMessage(msg); }
		
		// dispatch as a unity message to delegate, using OSC path as a 
		// lookup path, so designers can control routing w/o code changes
		if (messageDelegate != null) { 
			var target = messageDelegate.Find(msg.Address.Substring(1));
			if (target) {
				target.SendMessage("OnOscMessage", msg, SendMessageOptions.RequireReceiver);//added don't require reciever. 
			} else {
				messageDelegate.SendMessage("OnOscMessage", msg); 
			}
		} else {
			var go = GameObject.Find(msg.Address);
			if (go) { go.SendMessage("OnOscMessage", msg); }
		}
	}

	void Awake() {
		Debug.Log(string.Format(
			"{0}: Starting OSC Client Session with {1} on ports {2}/{3}",
			name, host, localPort, remotePort
		));

		// eastablish a new session
		try {
			io = new UdpPacket ();
			io.RemoteHostName = host;
			io.RemotePort = remotePort;
			io.LocalPort = localPort;
			osc = new Osc (io);
			osc.SetAllMessageHandler (OnMessage);
			io.Open();
		} catch {
			io = null;
			osc = null;
		}
	}

	void OnMessage(OscMessage msg) {
		// warning: at this point we are on the IO thread,
		// not the unity thread!  We need to "publish" the message
		// back to unity if we want to call any methods in the
		// UnityEngine package without crashing!
		lock (producerQueue) {
			producerQueue.Enqueue(msg);
		}
	}

	void Update() {

		// Anything in the in-queue?
		if (producerQueue.Count > 0) {

			// Every frame we check the producer queue for new messages,
			// and copy them into a consumer queue.  We have a two-queue
			// structure like this because we want to get-in and get-out 
			// of that lock as quickly as possible to avoid contention.
			lock (producerQueue) {
				while(producerQueue.Count > 0) {
					consumerQueue.Enqueue(producerQueue.Dequeue());
				}
			}

			// now we dump that consumer queue in the form of idiomatic
			// unity messages which are easy to handle in scripts.
			while (consumerQueue.Count > 0) {
				Dispatch(consumerQueue.Dequeue());
			}

		}

	}

	void OnDestroy() {
		// shut down the session immediately rather than waiting
		// for OSC's destructor, which might not be called before
		// the next Osc Controller's Awake() is invoked.
		io.Close ();
	}

}
