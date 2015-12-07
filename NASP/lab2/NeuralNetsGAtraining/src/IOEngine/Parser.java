package IOEngine;

import function.Function;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.text.NumberFormat;
import java.text.ParseException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

/**
 * Created by weenkus on 12/7/15.
 */
public class Parser {

    private static Scanner reader;

    public static List<Function> parseInput(String fileName) throws IOException, NumberFormatException, ParseException {

        BufferedReader br = new BufferedReader(new FileReader(fileName));

        try {
            String line = br.readLine();
            List<Function> functionList = new ArrayList<Function>();

            // Read the input file line by line
            while (line != null) {

                // Create the function with the input/output and add it to the function values
                String[] parts = line.split("\t");
                NumberFormat nf = NumberFormat.getInstance();
                Function function = new Function(nf.parse(parts[0].trim()).doubleValue(), nf.parse(parts[1].trim()).doubleValue());
                functionList.add(function);

                // Continue reading the input file
                line = br.readLine();
            }
            return functionList;
        } finally {
            br.close();
        }
    }

    public static int askUserForIntegerNet() {
        reader = new Scanner(System.in);
        System.out.print("Number of neural nets in mid layer: ");
        return reader.nextInt();
    }

    public static int askUserForIntegerGenetic() {
        reader = new Scanner(System.in);
        System.out.print("Number of neural nets in the in population: ");
        return reader.nextInt();
    }

    public static int askUserForIntegerIteration() {
        reader = new Scanner(System.in);
        System.out.print("Number of iterations in the evolution: ");
        return reader.nextInt();
    }

    public static double askUserForIntegerEror() {
        reader = new Scanner(System.in);
        System.out.print("Error for which the evolution should aim: ");
        return reader.nextDouble();
    }

    public static double askUserForDoubleMutataion() {
        reader = new Scanner(System.in);
        System.out.print("Mutation rate: ");
        double input = reader.nextDouble();
        return input;
    }

    public static String askUserForDoubleEvolutionMethod() {
        reader = new Scanner(System.in);
        System.out.print("Iterative or error (i/e): ");
        String input = reader.nextLine();
        return input.toLowerCase();
    }
}
