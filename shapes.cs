using System;
using System.Collections.Generic;
using System.Linq;

public class Shapes {
	const char FILLER_CHAR = '#';
	//readonly static string[] SUPPORTED_SHAPES = {"Square", "Rectangle", "Parallelogram", "Triangle", "Diamond"};
	readonly static List<Tuple<string, Func<int, string[]>, int>> SUPPORTED_SHAPES = new List<Tuple<string, Func<int, string[]>, int>> 
	{
		Tuple.Create<string, Func<int, string[]>, int>("Square", formSquare, 1),
		Tuple.Create<string, Func<int, string[]>, int>("Rectangle", formRectangle, 1),
		Tuple.Create<string, Func<int, string[]>, int>("Parallelogram", formParallelogram, 2),
		Tuple.Create<string, Func<int, string[]>, int>("Isoceles Triangle", formIsoTriangle, 2),
		Tuple.Create<string, Func<int, string[]>, int>("Equilateral Triangle", formEquTriangle, 2),

		Tuple.Create<string, Func<int, string[]>, int>("Diamond", formDiamond, 3),
		Tuple.Create<string, Func<int, string[]>, int>("Hexagon (odd height) or Octogon (even height)", formHexagon, 3),
	};

	public static string[] nada(int a){
		Console.WriteLine("ain't made this yet");
		return new string[0];
	}

	public static void Main(string[] args){
		int shapeType = 1; 
		int shapeHeight = 4; // desired shape height
		int minHeight; // min height for selected shape
		string label; // label text
		string labelLineMsg = "What row should the label be on? (default: 4): ";
		int labelLine; // line # for label
		bool drawAnotherShape = true; // main program loop control
		string answer; // answers to loop questions

		while (drawAnotherShape) {
			bool addLabels = true; // label loop control
			// get shape type
			shapeType = readInt(intro(), min:1, max:SUPPORTED_SHAPES.Count);

			// get shape height
			minHeight = SUPPORTED_SHAPES[shapeType-1].Item3;
			shapeHeight = readInt("How many lines tall should it be? (min: "+minHeight+"): ", min:minHeight);

			// generate shape
			string[] shape = SUPPORTED_SHAPES[shapeType-1].Item2(shapeHeight);

			while (addLabels) {
				// get display text (warn against chopping for long messages
				Console.Write("What label should be added? (default: LU): ");
				label = Console.ReadLine();
				if (string.IsNullOrEmpty(label)){
					label = "LU";
				}

				labelLine = readInt(labelLineMsg, defaultNum:4, min:1);
				shape = setLabel(shape, label, labelLine);

				//	ask for additional labels
				//	new labels can override old ones
				Console.Write("Add another label? (y/n)" );
				answer = Console.ReadLine().ToLower();
				if (answer != "y" &&  answer != "yes"){
					addLabels = false;
				} else { Console.WriteLine("");	}
			}

			// draw shape
			draw(shape);

			// loop control
			Console.Write("Draw another shape? (y/n) ");
			answer = Console.ReadLine().ToLower();
			if (answer != "y" &&  answer != "yes"){
				drawAnotherShape = false;
			} else { Console.WriteLine("");	}
		
		}
	}

	static string intro(){
		int c = 1;
		string introMsg ="";
		foreach (var shape in SUPPORTED_SHAPES) {
			introMsg+="("+c+") "+shape.Item1+"\n";
			c++;
		}
		introMsg+="\nEnter the number of the shape to draw: ";
		return introMsg;
	}
	

	static int readInt(string message, int min = int.MinValue, int max = int.MaxValue){
		string input;
		int number;
		do {
			Console.Write(message);
			input = Console.ReadLine();
		} while (!validateIntInRange(input, min, max, out number));
		return number;
	}

	static int readInt(string message, int defaultNum, int min = int.MinValue, int max = int.MaxValue){
		string input;
		int number = defaultNum;
		do {
			Console.Write(message);
			input = Console.ReadLine();
			if (string.IsNullOrEmpty(input)){
				break;
			}
		} while (!validateIntInRange(input, min, max, out number));
		return number;
	}

	static bool validateIntInRange (string numberString, int min, int max, out int number){
		if (int.TryParse(numberString.Trim(), out number)){
			if (number >= min && number <= max){
				return true;
			}
		}
		return false;
		
	}

	static string[] formSquare(int height){
		string[] square = new string[height];
		
		for (int c=0; c<height; c++){
			square[c] = generateLine(height);
		}

		return square;
	}

	static string[] formRectangle(int height){
		string[] rectangle = new string[height];

		for (int c=0; c<height; c++){
			rectangle[c] = generateLine(height*2);
		}

		return rectangle;
	}

	static string[] formParallelogram(int height){
		string[] parallelogram = formSquare(height);

		for (int c=0; c<height; c++){
			parallelogram[c] = new string (' ',height-1-c) + parallelogram[c];
		}

		return parallelogram;
	}

	static string[] formIsoTriangle(int height){
		string[] isoTriangle = new string[height];

		for (int c=0; c<height; c++){
			isoTriangle[c] = generateLine(c+1);
		}

		return isoTriangle;
	}

	static string[] formEquTriangle(int height){
		string[] equTriangle = formIsoTriangle(height);

		for (int c=0; c<height; c++){
			equTriangle[c] = new string (' ',height-1-c) + equTriangle[c]; 
		}

		return equTriangle;
	}

	static string[] formDiamond(int height){
		int halfHeight = (int)Math.Ceiling(height/2.0);
		string[] diamond = new string[height];
		string[] triangle = formEquTriangle(halfHeight); 
		
		for (int c=0; c<halfHeight; c++){
			diamond[c] = triangle[c];
			diamond[height-1-c] = triangle[c];
		}

		return diamond;
	}

	static string[] formHexagon(int height){
		string[] hexagon = new string[height];
		string[] diamond = formDiamond(height+4);
		for (int c=2; c<height+2;c++){
			hexagon[c-2] = diamond[c]; 
		}
			
		return hexagon;
	}

	static string generateLine(int length){
		return string.Concat(Enumerable.Repeat(FILLER_CHAR+" ",length)).Trim();	
	}

	static string[] setLabel(string[] shape, string label, int labelLine){
		if (labelLine > shape.Length) return shape; // label is off the shape, no change needed

		string targetLine = shape[labelLine-1];

		int targetLineFillerChars=0;
		foreach (char c in targetLine) {
			if (c == FILLER_CHAR) {
				targetLineFillerChars++;
			}
		}
		
		int targetIndex = 1;
		int labelLength = label.Length;
		if (labelLength < targetLineFillerChars) { // need to calculate the middle and the offset
			int targetMiddle = (int)Math.Ceiling(targetLineFillerChars/2.0);
	 		int offset = labelLength - (int)Math.Ceiling((labelLength + ((labelLength+1) % 2)) / 2.0);
			targetIndex = targetMiddle - offset;
		} else { // truncate label to proper size
			label = label.Substring(0, targetLineFillerChars);	
		}
		
		int count;
		foreach (char c in label) {
			// find targetIndex'th non-space char, replace with c, shift target
			count = 0;
			for (int d = 0; d<targetLine.Length; d++){
				if (targetLine[d] != ' ') {
					count++;
					if (count == targetIndex) {
						targetIndex++;
						char[] chars = targetLine.ToCharArray();
						chars[d] = c;
						targetLine = new string(chars);
						break;
					}
				}
			}
						
		}
		shape[labelLine-1] = targetLine;

		return shape;
	}

	static void draw(string[] shape){
		foreach (string line in shape){
			Console.Write(line + "\n");
		}
		Console.WriteLine("");
	}

}
