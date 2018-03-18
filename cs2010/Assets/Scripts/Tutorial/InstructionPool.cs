using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;

public class InstructionPool
{
	private List<Instruction> instructions;

	private XmlDocument instructionSet;

	public InstructionPool() {
		instructions = new List<Instruction>();
		instructionSet = new XmlDocument ();
	}

	public void AddInstruction(Instruction newInstruction) {
		instructions.Add (newInstruction);
	}

	public Instruction GetInstruction(int index) {
		if (instructions.Count != 0) {
			return instructions[index];
		}

		return null;
	}

	public int InstructionCount() {
		return instructions.Count;
	}

	public void SetupInstructions(string XmlPath) {
		// Load the XML file
		try {
			instructionSet.Load (XmlPath);
		}
		catch (FileNotFoundException e) {
			Debug.LogError ("Reverting back to main menu due to: " + e);
		}

		// Get all the instruction nodes
		XmlNodeList instructionNode = instructionSet.SelectNodes("Instructions/Instruction");

		foreach(XmlNode node in instructionNode) {

			try {
			string body = node["Body"].InnerText;

			// Create new instruction object
			Instruction instruction = new Instruction (body);
			// Add this to the collection of all instructions
			instructions.Add (instruction);

			XmlNode positions = node.SelectSingleNode ("Pos");
			if (positions != null) {
				string pos = positions ["X"].InnerText;

				// Retrieve X and Y call to actions
				int X = int.Parse(positions ["X"].InnerText);
				int Y = int.Parse(positions ["Y"].InnerText);

				instruction.SetX (X);
				instruction.SetY (Y);
			}

			if (node ["VoiceOver"] != null) {
				instruction.SetVoiceOver (node ["VoiceOver"].InnerText);
			}
			
			} 
			catch (NullReferenceException) {
				Debug.LogError ("A required attribute was missing from the tutorial XML file");
				// Revert to main menu
			}
				
		}
	}
		
}