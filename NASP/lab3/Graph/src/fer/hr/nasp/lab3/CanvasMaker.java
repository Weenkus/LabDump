package fer.hr.nasp.lab3;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

/**
 * Class for creating the canvas for state space search algorithms.
 * 
 * @author weenkus
 *
 */
public final class CanvasMaker {
	

	/**
	 * Return the number of rows (columns) on the canvas.
	 * 
	 * @return - number of rows (columns)
	 * @throws IOException - no file found
	 */
	public static int getDimensions() throws IOException {

		BufferedReader br = new BufferedReader(new FileReader("input.txt"));
		int dimension = 0;
		try {
			String line = br.readLine();
			dimension += line.length() - line.replaceAll(" ", "").length();
		} finally {
			br.close();
		}
		return dimension + 1;
	}

	/**
	 * Read the input file.
	 * 
	 * @throws IOException - the given file was not found!
	 */
	public static Point[][] readInputFile(Point[][] canvas, int dimension) throws IOException {

		BufferedReader br = new BufferedReader(new FileReader("input.txt"));
		try {
			String line = br.readLine();

			int j = -1;
			while (line != null) {
				j++;
				
				
				String[] parts = line.split(" ");
				
				// Constructe the canvas
				int i = -1;
				for(String s : parts) {
				    i++;
				    
				    // Figure the current location of the point
				    Location loc;
				    if( i < dimension / 2 )
				    	loc = Location.ENTERPRISE;
				    else 
				    	loc = Location.PLANET;
				    
				    // Create the point
				    Point p;
				    if( s.contains("T") ) {
				    	p = new Point(j, i, 0, CanvasLocation.TELEPORTER, loc, Character.getNumericValue(s.charAt(1)));
				    } else if (s.contains("S") ) {
				    	if( loc == Location.ENTERPRISE )
				    		p = new Point(j, i, 0, CanvasLocation.SHUTTLE_LAUNCH, loc, 0);
				    	else
				    		p = new Point(j, i, 0, CanvasLocation.SHUTTLE_LANDING, loc, 0);
				    } else if (s.equals("P") ) {
				    	p = new Point(j, i, 0, CanvasLocation.START, loc, 0);
				    } else if (s.equals("C")) {
				    	p = new Point(j, i, 0, CanvasLocation.END, loc, 0);
				    } else {
				    	p = new Point(j, i, Integer.parseInt(s), CanvasLocation.NORMAL, loc, 0);
				    }
				    
				    // Add the point to the canvas
				    canvas[j][i] = p;  
				}
				line = br.readLine();
			}
		} finally {
			br.close();
		}
		
		return canvas;
	}
}
