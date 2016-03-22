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
		int? shapeType = null;
		int? shapeHeight = null;

		

		// display shape options with corresponding numbers
		shapeType = readShapeType();


		// get requested height (validate for int and against minimum)
		shapeHeight = readHeight();

		// generate shape
		string[] shape = prepareShape(shapeType ?? 1, shapeHeight ?? 4);
		shape = test.Item2(5);
		// get display text (warn against chopping for long messages
		//	ask for additional labels

		// draw shape
		draw(shape);

		// loop
		Console.WriteLine("Draw another shape? (y/n)");
		// loop on y, exit otherwise
		
		Console.WriteLine("Still not done yet...");
	}

	static int readShapeType(){
		int shapeType;
		string inputType;
		// (square, diamond, equilateral triangle, isoceles triangle, 
		// rectangle, parallelogram, hexagon, octogon, X, V, line

		do {
			intro(); //replace this with an array/list and some display based on that?
			inputType = Console.ReadLine();
		} while (!validatePositiveInt(inputType, out shapeType) || shapeType > SUPPORTED_SHAPES.Length);

		return shapeType;
	}
	

	static int readHeight(){
		int shapeHeight;
		string inputHeight;

		do {
			Console.WriteLine("How tall should the shape be? (enter a positive integer)");
			inputHeight = Console.ReadLine();
		} while (!validatePositiveInt(inputHeight, out shapeHeight) || shapeHeight < 1);
		// recommend a max value based on console width?

		return shapeHeight;
	}

	static bool validatePositiveInt(string numberString, out int number){
		if (int.TryParse(numberString.Trim(), out number)){
			if (number > 0) {
				return true;
			}
		} 
		return false;
	}

	static string[] prepareShape(int type, int height){
		Console.WriteLine("prepareShape IS STILL USING TESTING ARUGMENTS");
		string[] shape;

		switch (type) {
			default:
				shape = prepareSquare(height);
				break;

		}

		return shape;
	
	}

	static string[] prepareSquare(int height = 3){
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

	static void intro(){
		int c = 1;
		foreach (string shape in SUPPORTED_SHAPES) {
			Console.WriteLine("("+c+") "+shape);
			c++;
		}
		Console.WriteLine("Enter the number of the shape to draw:");
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
