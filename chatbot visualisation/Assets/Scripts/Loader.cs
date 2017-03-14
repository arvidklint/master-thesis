using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class Loader {
	public static string Load(string fileName) {
		string text = "";

		try {
			StreamReader sr = new StreamReader(fileName, Encoding.Default);
			text = sr.ReadToEnd();
			sr.Close();
		} 
		catch (Exception e) {
			Debug.LogError ("Could not read file");
		}
			
		return text;
	}
}
