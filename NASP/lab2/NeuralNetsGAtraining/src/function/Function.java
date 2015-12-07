package function;

/**
 * Created by weenkus on 12/7/15.
 */
public class Function {

    private double x;
    private double y;

    public Function() {
        this.x = 0;
        this.y = 0;
    }

    public Function(double x, double y) {
        this.x = x;
        this.y = y;
    }

    public double getX() {
        return x;
    }

    public void setX(double x) {
        this.x = x;
    }

    public double getY() {
        return y;
    }

    public void setY(double y) {
        this.y = y;
    }

    @Override
    public String toString() {
        return "[x=" + this.x + ", y=" + this.y + "]";
    }


}
