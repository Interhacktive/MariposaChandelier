﻿using System;
using System.Collections;
using UnityEngine;

public class LEDSender : MonoBehaviour {

	Cable[] cables;
	int sliceLength;
	byte[] bigBuffer;
	byte[] bigBuffer2;
	int sendCable = 1;


	void Start () {
		cables = GetComponentsInChildren<Cable>();
		Array.Sort(cables, (a,b) => a.universe.CompareTo(b.universe));
		sliceLength = cables[0].workingBuffer.Length;
		bigBuffer = new byte[sliceLength * 3];//max size is	sliceLength * 3	
		bigBuffer2 = new byte[sliceLength * 2];//max size is	sliceLength * 3	

	}
	
	void FixedUpdate() {
		if(sendCable == 1){

		//BOARD #1
		// send cables 1-3 to DMX endpoint 0
		for(int i=0; i<3; ++i) {
			cables[i].WriteWorkingBuffer();
			Buffer.BlockCopy(cables[i].workingBuffer, 0, bigBuffer, i * sliceLength, sliceLength);
		}
		DmxController.inst.Send(1, bigBuffer, 3 * sliceLength, 1);
		//Debug.Log("sending to network");

				
		//BOARD #2
		// send cables 4-6 to DMX endpoint 0
		for(int i=3; i<6; ++i) {
			cables[i].WriteWorkingBuffer();
			Buffer.BlockCopy(cables[i].workingBuffer, 0, bigBuffer, (i-3) * sliceLength, sliceLength);
		}
		DmxController.inst.Send(2, bigBuffer, 3 * sliceLength, 2);


		}
		if(sendCable == 1){
		//BOARD #3

		// send cable 7-9 to DMX endpoint 3
		for(int i=6; i<9; ++i) {
			cables[i].WriteWorkingBuffer();
			Buffer.BlockCopy(cables[i].workingBuffer, 0, bigBuffer, (i-6) * sliceLength, sliceLength);
		}
		DmxController.inst.Send(3, bigBuffer, 3 * sliceLength, 3);





		//BOARD #4
		// send cables 8-10 to DMX endpoint 1
		for(int i=7; i<10; ++i) {
			cables[i].WriteWorkingBuffer();
			Buffer.BlockCopy(cables[i].workingBuffer, 0, bigBuffer, (i-7) * sliceLength, sliceLength);
		}
		DmxController.inst.Send(4, bigBuffer, 3 * sliceLength, 4);

		}
		if(sendCable == 1){


		//BOARD #5
		// send cables 13 and 14 (N & P) to DMX endpoint 5
	
		for(int i=12; i<14; ++i) {
			cables[i].WriteWorkingBuffer();
			Buffer.BlockCopy(cables[i].workingBuffer, 0, bigBuffer2, (i-12) * sliceLength, sliceLength);
		}
		DmxController.inst.Send(5, bigBuffer2, 2 * sliceLength, 5);//board 4 is actually 5, starting at 0
		//Debug.Log ("sending artnet to board #5");
		}



	}
}