.PHONY: all
all: shapes

.PHONY: shapes
shapes: 
	@mcs shapes.cs
	@mono shapes.exe	
