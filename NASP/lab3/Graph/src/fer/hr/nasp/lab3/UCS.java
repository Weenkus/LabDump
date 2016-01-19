package fer.hr.nasp.lab3;

import java.io.*;
import java.util.*;

public class UCS {

	public static void main(String[] args) throws IOException,
			InterruptedException {
		// Initialise the canvas
		int canvasDimension = CanvasMaker.getDimensions();
		Point[][] canvas = new Point[canvasDimension][canvasDimension];

		// Create the canvas
		canvas = CanvasMaker.readInputFile(canvas, canvasDimension);

		PriorityQueue<Point> openSortedList = new PriorityQueue<>(
				canvasDimension * canvasDimension, new Comparator<Point>() {
					/**
					 * Sort the points by their cost.
					 */
					@Override
					public int compare(Point one, Point two) {
						// return one.getHeight() - two.getHeight(); // 120
						if (one.getCost() > two.getCost()) {
							return 1;
						} else if (one.getCost() < two.getCost()) {
							return -1;
						} else {
							return 0;
						}

					}
				});
		
//		for(int i = 0; i < canvasDimension; i++) {
//			for(int j = 0; j < canvasDimension; j++) {
//				System.out.println(canvas[i][j]);
//			}
//		}

		// Initialise data structures
		Point startingPoint = findStartingPoint(canvas, canvasDimension);
		startingPoint.setParent(null);
		List<Point> shuttleTreleportationLink = linkEntities(canvas,
				canvasDimension);

		// Start the uniform cost algorithm
		HashSet<Point> vistedPointLIst = new HashSet<>();
		openSortedList.add(startingPoint);

		boolean found = false;
		int numberOfOpenedNodes = 0;
		int numberOfVisitedNodes = 0;
		long minimalCost = 0;
		vistedPointLIst.add(startingPoint);
		
		while (openSortedList.size() > 0) {
			//System.out.println(openSortedList);

			// Visit a node
			Point n = openSortedList.remove();
			++numberOfVisitedNodes;
			//vistedPointLIst.add(n);
			
			if (n.goal() == true) {
				found = true;
				// Path cost
				minimalCost = n.getCost();

				// Print path
				n.printPath();
				break;
			}
			List<Point> succNodes = new LinkedList<Point>();
			succNodes = n.succ(canvas, canvasDimension, shuttleTreleportationLink);
			for (Point m : succNodes) {
				// Skip already visited nodes
				if (vistedPointLIst.contains(m))
					continue;
				
				// This part, so much pain 
				vistedPointLIst.add(m);

				// Calculate the cost between the n point and the m point before
				// adding it to the list
				//n.calculateCost(m);
				m.setParent(n);
				m.setCost(m.calculatePathCostSoFar());
				openSortedList.add(m);
				++numberOfOpenedNodes;
			}
			//Thread.sleep(2000);
		}

		System.out.println("\nOpend nodes: " + numberOfOpenedNodes);
		System.out.println("Visited nodes: " + numberOfVisitedNodes);
		System.out.println("Minimal cost: " + minimalCost);
		// Success??
		if (found == false) {
			System.out.println("Picard failed his crew.");
		} else {
			System.out.println("Picard made it in time!");
		}

	}

	/**
	 * Return the starting point in the cavans.
	 * 
	 * @param canvas
	 *            - map
	 * @param dimension
	 *            - map dimensions (rows/columns)
	 * @return - starting point of the map
	 */
	public static Point findStartingPoint(Point[][] canvas, int dimension) {
		for (int i = 0; i < dimension; i++) {
			for (int j = 0; j < dimension; j++)
				if (canvas[i][j].getType() == CanvasLocation.START)
					return canvas[i][j];
		}
		return canvas[0][0];
	}

	/**
	 * Link all the teleporters and shuttles/
	 * 
	 * @param canvas
	 *            - map
	 * @param dimension
	 *            - map dimensions (rows/columns)
	 * @return - map with a link (sourc, dest)
	 */
	public static List<Point> linkEntities(Point[][] canvas, int dimension) {
		List<Point> shuttleTreleportationLink = new LinkedList<Point>();
		for (int i = 0; i < dimension; i++) {
			for (int j = 0; j < dimension; j++)
				if (canvas[i][j].getType() == CanvasLocation.SHUTTLE_LAUNCH
						|| canvas[i][j].getType() == CanvasLocation.SHUTTLE_LANDING
						|| canvas[i][j].getType() == CanvasLocation.TELEPORTER)
					shuttleTreleportationLink.add(canvas[i][j]);
		}
		return shuttleTreleportationLink;
	}

}
