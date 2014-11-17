using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

// Operates a generic ArtNet Session within this scene for
// sending arbitrary packets.  All IO is done on a separate
// thread and does not block Unity's control-flow.

public class DmxController : MonoBehaviour {

	// act like a singleton
	public static DmxController inst;

	// Designer Parameters
	[Serializable]
	public class Endpoint {
		public string host = "localhost";
		public int port = 11000;
	};
	public Endpoint[] endpoints;
	public bool multithreaded = true;

	// Internal State
	UdpClient client;
	Thread ioThread;
	
	struct DmxPacket {
		public byte[] packet;
		public Endpoint endpoint;
	};
	
	Queue<DmxPacket> packetsToSend = new Queue<DmxPacket>();
	bool finished = false;
	
	byte[] artNetHeader;
	
	Endpoint GetEndpoint(int i) {
		if (i < 0 || i >= endpoints.Length) {
			return endpoints[0];
		} else {
			return endpoints[i];
		}
	}
	
	public void Send(short universe, byte[] data, int len, int endpoint=-1) {
		// Write the packet to a synchronized queue and pulse the
		// consumer thread in case it's waiting for something to arrive.
		// This ensures that we don't block unity while we wait for the
		// socket to be ready to send.
		if (!finished) {
			byte[] packet = new byte[(0x11 + len) + 1];
			Buffer.BlockCopy(artNetHeader, 0, packet, 0, this.artNetHeader.Length);
			packet[8] = LoByte(0x5000);
			packet[9] = HiByte(0x5000);
			packet[10] = 0;          //ProtVerHi
			packet[11] = 14;         //ProtVerLo
			packet[12] = 0;          //Sequence
			packet[13] = 0;          //Physical
			packet[14] = LoByte(universe);
			packet[15] = HiByte(universe);
			packet[0x10] = HiByte(len);
			packet[0x11] = LoByte(len);
			Buffer.BlockCopy(data, 0, packet, 0x12, len);
			if (multithreaded) {
				lock(packetsToSend) {
					packetsToSend.Enqueue(new DmxPacket() {
						packet = packet, endpoint = GetEndpoint(endpoint)
					});				
					Monitor.Pulse(packetsToSend);
				}
			} else if (client != null) {
				var ep = GetEndpoint(endpoint);
				client.Send(packet, packet.Length, ep.host, ep.port);
			}
		}
	}
	
	void Awake() {
		inst = this;
		if (endpoints.Length == 0) {
			Debug.LogWarning("No Endpoints Defined.");
			finished = true;
			return;
		}

		artNetHeader = new byte[] { 0x41, 0x72, 0x74, 0x2d, 0x4e, 0x65, 0x74, 0 };	
		
		// Connect
		try {
			client = new UdpClient();
		} catch {
			Debug.LogError("Could not connect initialize ArtNet Client");
		}
		if (multithreaded && client != null) {
			// Logical "ownership" of the client is passed to the 
			// consumer thread who is responsible for closing it when
			// the session completes.
			ioThread = new Thread(Consumer);
			ioThread.IsBackground = true;
			ioThread.Start();
		} else {
			// Don't queue up packets
			finished = true;
		}
	}

	void OnDestroy() {
		if (inst == this) { inst =  null; }
		
		// C# spec guarentees this write to be atomic
		finished = true;
	}

	void Consumer() {
		var inbox = new Queue<DmxPacket>();

		// Loop until we're done
		while(!finished) {
			// Wait for packets to arrive from Unity
			lock(packetsToSend) {
				while(packetsToSend.Count == 0) {
					// monitor releases the lock until we get
					// Pulse()'d by the the unity thread
					Monitor.Wait(packetsToSend);
				}
				while(packetsToSend.Count > 0) {
					inbox.Enqueue(packetsToSend.Dequeue());
				}
			}
			// Send outside the lock, so we don't block Unity
			while(inbox.Count > 0) {

				var packet = inbox.Dequeue();
				client.Send(packet.packet, packet.packet.Length, packet.endpoint.host, packet.endpoint.port);
				//Debug.Log ("sending packet: " + packet.endpoint.port);
			}
		}

		// Finalize the Session
		client.Close();
		client = null;
	}
	
	
	
	static byte LoByte(int wParam) { return Convert.ToByte(wParam & 0xffL); }
	static byte HiByte(int wParam) { return Convert.ToByte((wParam / 0x100) & 0xffL); }
	
	
}
