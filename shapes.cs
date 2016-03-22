using System;
using System.Collections.Generic;
using System.Linq;

public class Shapes {
	const string FILLER_CHAR = "#";
	//readonly static string[] SUPPORTED_SHAPES = {"Square", "Rectangle", "Parallelogram", "Triangle", "Diamond"};
	readonly static List<Tuple<string, Func<int, string[]>>> SUPPORTED_SHAPES = new List<Tuple<string, Func<int, string[]>>> 
	{
		Tuple.Create<string, Func<int, string[]>>("Square", formSquare),
		Tuple.Create<string, Func<int, string[]>>("*Rectangle", nada),
		Tuple.Create<string, Func<int, string[]>>("Parallelogram", formParallelogram),
		Tuple.Create<string, Func<int, string[]>>("Isoceles Triangle", formIsoTriangle),
		Tuple.Create<string, Func<int, string[]>>("Equilateral Triangle", formEquTriangle),
		Tuple.Create<string, Func<int, string[]>>("Diamond", formDiamond),

	};

	public static string[] nada(int a){
		Console.WriteLine("ain't made this yet");
		return new string[0];
	}

	public static void Main(string[] args){
		int shapeType = 1;
		int shapeHeight = 4;
		bool drawAnotherShape = true;

		while (drawAnotherShape) {
			// get shape type
			shapeType = readInt(intro(), 1, SUPPORTED_SHAPES.Count);

			// get shape height
			shapeHeight = readInt("How many lines tall should it be? (enter a positive integer): ", 1);

			// generate shape
			string[] shape = SUPPORTED_SHAPES[shapeType-1].Item2(shapeHeight);


			// get display text (warn against chopping for long messages
			//	ask for additional labels

			// draw shape
			draw(shape);

			// loop
			Console.WriteLine("Draw another shape? (y/n)");
			string answer = Console.ReadLine().ToLower();
			if (answer != "y" &&  answer != "yes"){
				drawAnotherShape = false;
			} else { Console.WriteLine("");	}
		
		}
		Console.WriteLine("Still not done yet...");
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

	static string[] prepareIsoTriangle(int height){
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

	static string generateLine(int length, string filler=FILLER_CHAR, bool centered=false){
		string line;

		if (centered) {
			line = "NOT IMPLEMENTED";
		} else {
			line = string.Concat(Enumerable.Repeat(FILLER_CHAR+" ",length)).Trim();	

		}

		return line;
	}

	static void draw(string[] shape){
		foreach (string line in shape){
			Console.Write(line + "\n");
		}
		Console.WriteLine("");
	}

}
