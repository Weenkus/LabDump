package fer.hr.nasp.lab3;

import java.io.IOException;
import java.util.Comparator;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.PriorityQueue;

public class A {

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
						if (one.getHeuristicCost() > two.getHeuristicCost()) {
							return 1;
						} else if (one.getHeuristicCost() < two
								.getHeuristicCost()) {
							return -1;
						} else {
							return 0;
						}

					}
				});

		// Initialise data structures
		Point startingPoint = findStartingPoint(canvas, canvasDimension);
		Point endPoint = findEndPoint(canvas, canvasDimension);
		startingPoint.setParent(null);
		List<Point> shuttleTreleportationLink = linkEntities(canvas,
				canvasDimension);

		// Start the uniform cost algorithm
		HashSet<Point> closed = new HashSet<>();
		openSortedList.add(startingPoint);

		boolean found = false;
		int numberOfOpenedNodes = 0;
		int numberOfVisitedNodes = 0;
		long minimalCost = 0;

		while (openSortedList.size() > 0) {
			// System.out.println(openSortedList);

			// Visit a node
			Point n = openSortedList.remove();
			++numberOfVisitedNodes;
			closed.add(n);

			if (n.goal() == true) {
				found = true;
				// Path cost
				minimalCost = n.getCost();

				// Print path
				n.printPath();
				break;
			}
			List<Point> succNodes = new LinkedList<Point>();
			succNodes = n.succ(canvas, canvasDimension,
					shuttleTreleportationLink);
			for (Point m : succNodes) {
				// Skip already visited nodes
				if (closed.contains(m)) {
					for (Point p : closed) {
						if (p.equals(m)) {
							if (m.getCost() > p.getCost()) {
								closed.remove(m);
								closed.add(p);
							}
						}
					}
					continue;
				}

				// This part, so much pain
				closed.add(m);

				// Calculate the cost between the n point and the m point before
				// adding it to the list
				// n.calculateCost(m);

				m.setParent(n);

				m.setCost(m.calculatePathCostSoFar());
				m.setHeuristicCost(m.getCost()+m.heuristicOne(endPoint));
//				m.setHeuristicCost(m.getCost()
//						+ m.heuristicTwo(endPoint, shuttleTreleportationLink));

				openSortedList.add(m);
				++numberOfOpenedNodes;
			}
			// Thread.sleep(2000);
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
	 * Find the end (goal) point on the canvas O(n*m) -> horrible
	 * 
	 * @param canvas
	 *            - point map
	 * @param dimension
	 *            - map dimensions
	 * @return - the end point on the canvas
	 */
	public static Point findEndPoint(Point[][] canvas, int dimension) {
		for (int i = 0; i < dimension; i++) {
			for (int j = 0; j < dimension; j++)
				if (canvas[i][j].getType() == CanvasLocation.END)
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
