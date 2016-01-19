package fer.hr.nasp.lab3;

import java.util.LinkedList;
import java.util.List;

/**
 * Class describes a location on either enterprise or planet. Careful!!!! x is
 * the distance from the top of the map y is the distance from the left side of
 * the map
 * 
 * @author weenkus
 *
 */
public class Point implements Comparable<Point> {

	private int x;
	private int y;
	private int height;
	private CanvasLocation type;
	private Location location;
	private int deviceID;
	private int cost;
	private Point parent;
	private int heuristicCost;

	public Point(int x, int y, int height, CanvasLocation type,
			Location location, int deviceID) {
		this.x = x;
		this.y = y;
		this.height = height;
		this.type = type;
		this.location = location;
		this.deviceID = deviceID;
		this.cost = 0;
	}

	public Point() {

	}

	/**
	 * Calculate the teleportation distance between different teleportation
	 * locations.
	 * 
	 * @param p
	 *            - teleportation destination
	 * @return - distance value as integer
	 */
	public int teleportationDistance(Point p) {
		// Teleportation is only viable if they have the same ID
		if (this.deviceID == p.deviceID) {
			int xDistance = Math.abs(this.x - p.x);
			int yDistance = Math.abs(this.y - p.y);
			return xDistance + yDistance;
		} else
			throw new IllegalArgumentException();
	}

	/**
	 * Calculate the shuttle distance between Enterprise and the Planet.
	 * 
	 * @param p
	 *            - shuttle destination
	 * @return - distance value as integer
	 */
	public int shuttleDistance(Point p) {
		return teleportationDistance(p) * 3;
	}

	/**
	 * Calculate the travel cost between two points on the same location
	 * (planet/enterprise). The location need to be neighbors with the same
	 * location.
	 * 
	 * @param p
	 *            - destination point
	 * @return - travel cost
	 */
	public int travelCost(Point p) {
		// The points need to be neighbors
		if (Math.abs(this.x - p.x) >= 0 || Math.abs(this.y - y) >= 0) {
			throw new IllegalArgumentException();
		}

		// Distance to points that have a special meaning is zero, always.
		if (p.type == CanvasLocation.END
				|| p.type == CanvasLocation.SHUTTLE_LANDING
				|| p.type == CanvasLocation.SHUTTLE_LAUNCH
				|| p.type == CanvasLocation.START
				|| p.type == CanvasLocation.TELEPORTER)
			return 0;
		// And on the same planet/ship
		if (this.location == p.location)
			return Math.abs(this.height - p.height);
		else
			throw new IllegalArgumentException();
	}

	@Override
	public String toString() {
		if (this.deviceID == 0)
			return "(" + (this.x + 1) + "," + (this.y + 1) + "," + this.height
					+ "," + this.type + "," + this.location + "," + this.cost
					+ ")";
		else
			return "(" + (this.x + 1) + "," + (this.y + 1) + "," + this.height
					+ "," + this.type + "_" + this.deviceID + ","
					+ this.location + "," + this.cost + ")";
	}

	/**
	 * Test if the point is final.
	 * 
	 * @return - true is the point is final, otherwise false
	 */
	public boolean goal() {
		if (this.type == CanvasLocation.END)
			return true;
		else
			return false;
	}

	/**
	 * Returns a list of all neighborhood points of the point p.
	 * 
	 * @param canvas
	 *            - surface of all points
	 * @param dimension
	 *            - canvas dimensions
	 * @return - list of all neighborhood points.
	 */
	public List<Point> succ(Point[][] canvas, int dimension, List<Point> link) {
		List<Point> succList = new LinkedList<Point>();

		getSurroundingPoints(canvas, dimension, succList);
		// Find succ for teleportation devices
		if (this.type == CanvasLocation.TELEPORTER) {
			for (Point p : link) {
				if (p.type != CanvasLocation.TELEPORTER)
					continue;
				if (this.deviceID == p.deviceID
						&& ((this.x != p.x) || (this.y != p.y))) {
					succList.add(p);
				}
			}
		}
		if (this.type == CanvasLocation.SHUTTLE_LAUNCH) {
			for (Point p : link) {
				if (p.type != CanvasLocation.SHUTTLE_LANDING)
					continue;
				if (p.type == CanvasLocation.SHUTTLE_LANDING
						&& ((this.x != p.x) || (this.y != p.y))) {
					succList.add(p);
				}
			}
		}

		return succList;
	}

	/**
	 * Get surrounding points.
	 * 
	 * @param canvas
	 *            - surface
	 * @param dimension
	 *            - surfcace dimensions
	 * @param succList
	 *            - list of surrounding points
	 */
	private void getSurroundingPoints(Point[][] canvas, int dimension,
			List<Point> succList) {
		// There is a succ from above
		if (this.x != 0) {
			if (sameLocation(canvas[this.x - 1][this.y]))
				succList.add(canvas[this.x - 1][this.y]);
			// There is a succ underneath
		}
		if (this.x != (dimension - 1)) {
			if (sameLocation(canvas[this.x + 1][this.y]))
				succList.add(canvas[this.x + 1][this.y]);
			// There is a succ to the left
		}
		if (this.y != 0) {
			if (sameLocation(canvas[this.x][this.y - 1]))
				succList.add(canvas[this.x][this.y - 1]);
			// There is a succ to the right
		}
		if (this.y != (dimension - 1)) {
			if (sameLocation(canvas[this.x][this.y + 1]))
				succList.add(canvas[this.x][this.y + 1]);
		}
	}

	/**
	 * Check if the two points are on the same location;
	 * 
	 * @param p
	 *            - point
	 * @return - true if both points are on the same location
	 */
	public boolean sameLocation(Point p) {
		if (this.location != p.location)
			return false;
		else
			return true;
	}

	/**
	 * Caluclate the distance cost between this point and point p.
	 * 
	 * @param p
	 *            - distance to which points we calculate
	 */
	public void calculateCost(Point p) {
		if ((this.type == CanvasLocation.TELEPORTER && p.type == CanvasLocation.TELEPORTER)
				&& this.deviceID == p.deviceID) {
			p.cost = this.teleportationDistance(p) + this.cost;
		} else if (this.type == CanvasLocation.SHUTTLE_LAUNCH
				&& p.type == CanvasLocation.SHUTTLE_LANDING) {
			p.cost = this.shuttleDistance(p) + this.cost;
		} else {
			p.cost = Math.abs(this.height - p.height) + this.cost;
		}
	}

	/**
	 * Print the point path.
	 */
	public void printPath() {
		System.out.println("\nPath: ");
		for (Point p = this; p != null; p = p.parent) {
			System.out.println(p);
		}
	}

	/**
	 * Go from the current node to the start node calculating the total path
	 * cost.
	 * 
	 * @return - current path cost
	 */
	public int calculatePathCostSoFar() {
		int sum = 0;
		if (this.parent == null)
			return 0;
		for (Point p = this; p.parent != null; p = p.parent) {
			if ((p.type == CanvasLocation.TELEPORTER && p.parent.type == CanvasLocation.TELEPORTER)
					&& p.deviceID == p.parent.deviceID)
				sum += p.teleportationDistance(p.parent);
			else if (p.type == CanvasLocation.SHUTTLE_LANDING
					&& p.parent.type == CanvasLocation.SHUTTLE_LAUNCH)
				sum += p.shuttleDistance(p.parent);
			else
				sum += Math.abs(p.height - p.parent.height);
		}
		return sum;
	}

	/**
	 * First heuristic method. Calculates h(state) based on the manhattans
	 * distance to the end point.
	 *
	 * @param p
	 *            - currant state
	 * @param end
	 *            - end state (goal)
	 * @return - menhattans distance between point p and end point (goal)
	 */

	public int heuristicOne(Point end) {
		return Math.abs(this.x - end.x) + Math.abs(this.y - end.y);
	}

	public int heuristicTwo(Point end, List<Point> listOfTeleporters) {
		int distanceEnterprise = 0;
		int resultHolder;
		int min = 200;

		if (this.location == Location.ENTERPRISE) {
			for (Point m : listOfTeleporters) {
				if (m.getType() != CanvasLocation.TELEPORTER)
					continue;
				if (m.getLocation() != Location.PLANET)
					continue;
				resultHolder = Math.abs(m.x - this.x) + Math.abs(m.y - this.y);
				if (resultHolder < min)
					min = resultHolder;
			}
			distanceEnterprise = min;
			min = 200;
			for (Point m : listOfTeleporters) {
				if (m.getType() != CanvasLocation.TELEPORTER)
					continue;
				if (m.getLocation() != Location.ENTERPRISE)
					continue;
				resultHolder = Math.abs(m.x - end.x) + Math.abs(m.y - end.y);
				if (resultHolder < min)
					min = resultHolder;
			}

			return min + distanceEnterprise;

		} else if (this.location == Location.PLANET) {
			return Math.abs(this.x - end.x) + Math.abs(this.y - end.y);
		}
		return 0;
	}

	public int getHeight() {
		return this.height;
	}

	public CanvasLocation getType() {
		return this.type;
	}

	public int getX() {
		return this.x;
	}

	public int getY() {
		return this.y;
	}

	public Location getLocation() {
		return this.location;
	}

	public int getCost() {
		return this.cost;
	}

	public void setParent(Point p) {
		this.parent = p;
	}

	public void setCost(int cost) {
		this.cost = cost;
	}

	public void setHeuristicCost(int heuristicCost) {
		this.heuristicCost = heuristicCost;
	}

	public int getHeuristicCost() {
		return heuristicCost;
	}

	@Override
	public int compareTo(Point arg0) {
		// return one.getHeight() - two.getHeight(); // 120
		if (this.cost > arg0.getCost()) {
			return 1;
		} else if (this.cost < arg0.getCost()) {
			return -1;
		} else {
			return 0;
		}
	}

}
